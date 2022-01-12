using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    private GameObject mainCamera;      //���C���J�����i�[�p
    private GameObject subCamera;       //�T�u�J�����i�[�p 


    //�Ăяo�����Ɏ��s�����֐�
    void Start()
    {
        //���C���J�����ƃT�u�J���������ꂼ��擾
        mainCamera = GameObject.Find("TpsCamera");
        subCamera = GameObject.Find("MovieCamera");

        //�T�u�J�������A�N�e�B�u�ɂ���
       //subCamera.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //�T�u�J�������A�N�e�B�u�ɐݒ�
            mainCamera.SetActive(true);
            subCamera.SetActive(false);
        }
     
    }
}
