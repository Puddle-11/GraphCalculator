using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    private bool[,] MatrixValues;
    [SerializeField] private GameObject matrixCell;
    [SerializeField] private Vector2 Origin;
    [SerializeField] private float CellSize;
    private GameObject[,] MatrixObjects = new GameObject[0,0];
    [SerializeField] private float marginBreak;
    public static Matrix MatrixRef;
    private void Awake()
    {
        if(MatrixRef == null)
        {
            MatrixRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Origin = transform.position;
    }
    private void Start()
    {

        UpdateMatrix(0);
    }
    public void UpdateMatrix(int _size, Vector2Int[] _truthValue)
    {
        for (int x = 0; x < MatrixObjects.GetLength(0); x++)
        {
            for (int y = 0; y < MatrixObjects.GetLength(1); y++)
            {
                Destroy(MatrixObjects[x, y]);
            }
        }
        _size = _size + 1;
        MatrixValues = new bool[_size - 1, _size - 1];
        MatrixObjects = new GameObject[_size, _size];
        for (int x = 0; x < _size; x++)
        {
            for (int y = 0; y < _size; y++)
            {
                Vector2 pos = Vector2.zero;
                pos = Origin + new Vector2(x, -y) * CellSize;
                if(x != 0)
                {
                    pos.x += marginBreak;

                }
                if(y != 0)
                {
                    pos.y -= marginBreak;
                }
                MatrixObjects[x,y] = Instantiate(matrixCell, pos, Quaternion.identity, gameObject.transform);

            }
        }
        for (int ix = 1; ix < _size; ix++)
        {
            MatrixObjects[ix, 0].GetComponent<MatrixCellValues>().UpdateText((ix - 1).ToString());
        }
        for (int iy = 0; iy < _size; iy++)
        {
            MatrixObjects[0, iy].GetComponent<MatrixCellValues>().UpdateText((iy - 1).ToString());
        }

        for (int i = 0; i < _truthValue.Length; i++)
        {
            MatrixObjects[_truthValue[i].x + 1, _truthValue[i].y + 1].GetComponent<MatrixCellValues>().UpdateText(1.ToString());
        }
        MatrixObjects[0, 0].GetComponent<MatrixCellValues>().UpdateText("");
    }
    public void UpdateMatrix(int _size)
    {
        Vector2Int[] temp = new Vector2Int[] {};
        UpdateMatrix(_size, temp);
    }
}
