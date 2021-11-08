using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //親のオブジェクト
    private GameObject ParentObject;
    public int DeleteTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Rootを親オブジェクトに指定
        ParentObject = GameObject.Find("Root");
        this.transform.parent = ParentObject.transform;

        //1秒後に削除
        Destroy(gameObject, DeleteTime);
    }
}
