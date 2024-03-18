using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Line : MonoBehaviour
{

    public int L_beginingIndex;
    public int L_endingIndex;
    public Transform L_startTransform;
    public Transform L_endTransform;
    public LineRenderer LN;
    public float Distance;
    public GameObject Arrowhead;
    private Vector2 Direction;
    
    public void OnEnable()
    {
        LN.positionCount = 2;
    }

    public void Update()
    {
        UpdatePos();
    }
    public void UpdatePos()
    {
        Vector2 pos0 = L_startTransform.position - (L_startTransform.position - L_endTransform.position).normalized * Distance;
        Vector2 pos1 = L_endTransform.position - (L_endTransform.position - L_startTransform.position).normalized * Distance;

        LN.SetPosition(0, pos0);
        LN.SetPosition(1, pos1);
        Arrowhead.transform.position = pos1;
        Direction =  L_endTransform.position - L_startTransform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Arrowhead.transform.rotation = rotation;
    }
}
