using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool hover;
    private Vector2 Offset;
    private float timer;
    private bool runningTimer;
    public int index;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mouse")
        {

        hover = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Mouse")
        {

        hover = false;
        }
    }
 

    // Update is called once per frame
    void Update()
    {
        if (runningTimer)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && hover)
        {
            runningTimer = true;
            PointManager.ChangeSelected(gameObject);
            Offset = transform.position - CameraManager.Camref.Mouse.transform.position;

        }
        if (Input.GetMouseButtonUp(0))
        {
            runningTimer = false;
            if (PointManager.currentlySelectedPoint == gameObject)
            {


                if (!PointManager.PMRef.drawingLine)
                {
                    if (timer < PointManager.PMRef.clickTime)
                    {
                        Debug.Log("Begining Line");

                        PointManager.PMRef.BeginLine(index);
                    }

                }
      

                PointManager.ChangeSelected(null);
            }

            timer = 0;
        }
        
        if (PointManager.currentlySelectedPoint == gameObject)
        {
            transform.position = CameraManager.mouseWorldPos + Offset;
        }

    }
}
