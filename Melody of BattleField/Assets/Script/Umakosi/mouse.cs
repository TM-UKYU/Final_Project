using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    public GameObject player;

    public GameObject aimObject;



    private Vector3 offset;

    private Vector3 setPosition;



    //”¼Œa

    private float r = 5;

    //ƒ‰ƒWƒAƒ“

    private float deg = 0;



    float horizontal;



    float vertical;





    void Start()
    {

        offset = transform.position - player.transform.position;



        transform.position += offset;

    }





    void LateUpdate()
    {

        //–‚Rf‚Ì‚Æ‚±‚ë‚ğ•Ï”‚É‚µ‚½‚çŠ´“®İ’è‚Å‚«‚é

        //*AimController‚Æˆê‚Ì’l‚É‚·‚é‚±‚Æ

        horizontal = Input.GetAxis("Mouse X") * 3f;

        vertical = Input.GetAxis("Mouse Y");



        if (horizontal != 0)
        {

            deg -= horizontal;

        }





        setPosition.x = player.transform.position.x + r * Mathf.Cos(Mathf.Deg2Rad * deg);

        setPosition.y = player.transform.position.y + offset.y;

        setPosition.z = player.transform.position.z + r * Mathf.Sin(Mathf.Deg2Rad * deg);

        transform.position = setPosition;

        transform.LookAt(aimObject.transform);



    }
}
