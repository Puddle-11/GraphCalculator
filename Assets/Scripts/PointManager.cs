using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static GameObject currentlySelectedPoint;
    public static PointManager PMRef;
    public List<GameObject> allPoints = new List<GameObject>();
    public List<GameObject> allLines = new List<GameObject>();
    public List<Vector2Int> lineMatrix = new List<Vector2Int>();
    public bool drawingLine;
    public GameObject currentTempLine;
    public GameObject linePrefab;
    public GameObject pointPrefab;
    public float clickTime;
    public GameObject CurrentSelect;
    public void Awake()
    {
        if(PMRef == null)
        {
            PMRef = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void Clear()
    {
        for (int i = 0; i < allPoints.Count; i++)
        {
            Destroy(allPoints[i]);
        }
        allPoints.Clear();
        for (int i = 0; i < allLines.Count; i++)
        {
            Destroy(allLines[i]);
        }
        allLines.Clear();
        lineMatrix.Clear();
    }
    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && drawingLine == true)
        {
            EndLine(currentlySelectedPoint);
        }
        CurrentSelect = currentlySelectedPoint;
    }
    public void BeginLine(int _index)
    {
        GameObject x = Instantiate(linePrefab, allPoints[_index].transform.position, Quaternion.identity);
        x.GetComponent<Line>().L_beginingIndex = _index;
        x.GetComponent<Line>().L_startTransform = allPoints[_index].transform;
        x.GetComponent<Line>().L_endTransform = CameraManager.Camref.Mouse.transform;
        currentTempLine = x;
        drawingLine = true;
    }
    public void CancelLine()
    {
        StartCoroutine(DrawLineDelay());        
        Destroy(currentTempLine);
        currentTempLine = null;
    }
    public IEnumerator DrawLineDelay()
    {
        yield return 0;
        drawingLine = false;
    }
    public void EndLine(int _endingIndex)
    {
        _endingIndex = Mathf.Clamp(_endingIndex, 0, allPoints.Count);
        Vector2Int Value = new Vector2Int(currentTempLine.GetComponent<Line>().L_beginingIndex, _endingIndex);
        bool clean = true;
        foreach (GameObject L in allLines)
        {
            Vector2Int currentval = new Vector2Int(L.GetComponent<Line>().L_beginingIndex, L.GetComponent<Line>().L_endingIndex);   
            if(currentval == Value)
            {
                clean = false;
            }
        }
        if (clean)
        {
            currentTempLine.GetComponent<Line>().L_endingIndex = _endingIndex;
            currentTempLine.GetComponent<Line>().L_endTransform = allPoints[_endingIndex].transform;
            
            allLines.Add(currentTempLine);
            currentTempLine = null;
            lineMatrix.Add(Value);
        }
        else
        {
            CancelLine();
        }
        StartCoroutine(DrawLineDelay());

    }
    public void EndLine(GameObject _currentPoint)
    {
        if (_currentPoint == null) 
        { 
            CancelLine();
            return;
        }

        EndLine(_currentPoint.GetComponent<Point>().index);
    }
    public void AddPoint()
    {
       GameObject temp = Instantiate(pointPrefab, Vector2.zero, Quaternion.identity, gameObject.transform);
        allPoints.Add(temp);
        temp.GetComponent<Point>().index = allPoints.Count - 1;
    }
    public void RemovePoint(GameObject _point)
    {
        allPoints.Remove(_point);
    }

    public static void ChangeSelected(GameObject _newPoint)
    {

        currentlySelectedPoint = _newPoint;
    }

}
