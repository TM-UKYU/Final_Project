using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Camera subCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.gameObject.SetActive(false);
        subCamera.gameObject.SetActive(true);
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            mainCamera.gameObject.SetActive(true);
            subCamera.gameObject.SetActive(false);
        }
        
    }
}
