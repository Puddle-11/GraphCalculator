using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static Vector2 mouseWorldPos;
    public Vector2 rightclickOffset;
    public static CameraManager Camref;
    [SerializeField] private Camera mainCam;
    public GameObject Mouse;
    public GameObject rightClickMenu;
    private bool menuActive;
    private void Awake()
    {
        if(Camref == null)
        {
            Camref = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && PointManager.currentlySelectedPoint == null)
        {
            menuActive = true;
            rightClickMenu.SetActive(true);
            
            rightClickMenu.transform.position = (Vector2)Input.mousePosition + rightclickOffset;

        }

        if (Input.GetMouseButtonUp(0))
        {
            rightClickMenu.SetActive(false);
            menuActive = false;
        }

        mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Mouse.transform.position = mouseWorldPos;
    }
}
