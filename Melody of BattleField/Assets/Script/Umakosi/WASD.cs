using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    public float moveSpeed = 1;
    public float rotSpped = 1;
    private int doubleClick;
    private int frame=400;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotSpped * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotSpped * Time.deltaTime, 0);
        }

        frame--;

        if (frame <= 0)
        {
            frame = 400;
            if (moveSpeed > 10)
            {
                moveSpeed /= 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            if (moveSpeed <= 10)
            {
                doubleClick++;
                if (doubleClick >= 2)
                {
                    moveSpeed *= 3;
                    doubleClick = 0;
                    Debug.Log("doubleClick");
                }
            }
            Debug.Log(doubleClick);
        }

    }
}