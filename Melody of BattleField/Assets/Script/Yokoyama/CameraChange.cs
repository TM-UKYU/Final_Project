using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    private GameObject mainCamera;      //メインカメラ格納用
    private GameObject subCamera;       //サブカメラ格納用 


    //呼び出し時に実行される関数
    void Start()
    {
        //メインカメラとサブカメラをそれぞれ取得
        mainCamera = GameObject.Find("TpsCamera");
        subCamera = GameObject.Find("MovieCamera");

        //サブカメラを非アクティブにする
       //subCamera.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //サブカメラをアクティブに設定
            mainCamera.SetActive(true);
            subCamera.SetActive(false);
        }
     
    }
}
