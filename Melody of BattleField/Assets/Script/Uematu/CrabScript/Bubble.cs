using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //�e�̃I�u�W�F�N�g
    private GameObject ParentObject;
    public int DeleteTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Root��e�I�u�W�F�N�g�Ɏw��
        ParentObject = GameObject.Find("Root");
        this.transform.parent = ParentObject.transform;

        //1�b��ɍ폜
        Destroy(gameObject, DeleteTime);
    }
}
