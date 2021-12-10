using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public GameObject player;



    private Vector3 offset;

    private Vector3 setPosition;





    //ここをfloatに変えました

    private float moveOffsetY;





    //半径

    private float r = 5;

    //ラジアン

    private float deg = 0;

    //横移動量

    float horizontal;

    //縦移動量

    float vertical;





    void Start()
    {



        offset = transform.position + player.transform.position;



    }





    void LateUpdate()
    {

        //＊３fのところを変数にしたら感動設定できる

        horizontal = Input.GetAxis("Mouse X") * 3f;

        vertical = Input.GetAxis("Mouse Y");



        //プラマイ変更で移動反転

        if (horizontal != 0)
        {

            deg -= horizontal;

        }



        if (vertical != 0)
        {

            moveOffsetY -= vertical;

        }



        setPosition.x = player.transform.position.x - r * Mathf.Cos(Mathf.Deg2Rad * deg);

        setPosition.y = player.transform.position.y - moveOffsetY;

        setPosition.z = player.transform.position.z - r * Mathf.Sin(Mathf.Deg2Rad * deg);

        transform.position = setPosition;

    }
}
