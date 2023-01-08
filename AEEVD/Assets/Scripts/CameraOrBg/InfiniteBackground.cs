using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    private GameObject cam;

    private float lengthX;
    private float lengthY;
    private float startX;
    private float startY;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        startX = transform.position.x;
        startY = transform.position.y;
        lengthX = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        //if(Mathf.Abs(Vector3.Distance(cam.transform.position, transform.position)) > lengthX * 2)
        //{
            transform.position = new Vector3(startX, startY, 0);
        //}
        if(cam.transform.position.x > (startX + lengthX))
        {
            startX += lengthX;
        }
        else if(cam.transform.position.x < (startX - lengthX))
        {
            startX -= lengthX;
        }

        if(cam.transform.position.y > (startY + lengthY))
        {
            startY += lengthY;
        }
        else if(cam.transform.position.y < (startY - lengthY))
        {
            startY -= lengthY;
        }
    }
}
