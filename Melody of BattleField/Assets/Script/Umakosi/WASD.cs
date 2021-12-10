using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    public float moveSpeed = 1;
    public float rotSpped = 1;
    private int doubleClick;
    private int frame = 400;
    private GameObject mainCamera;
    private void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
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
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
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