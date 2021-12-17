using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wasd;
    public float futtobi;
    private bool isHit;

    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            Invoke("Call", 3f);
        }
    }
    private void Call()
    {
        isHit = false;
        /////////////////////////////////////////////////////
        wasd.GetComponent<PlayerTransVelocity>().enabled = true;////////////
        /////////////////////////////////////////////////////

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "FireBall")
        {

            futtobi = 10.0f;
            isHit = true;

            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            //Destroy(other.gameObject);
            Vector3 direction = other.transform.forward;
            direction.y = 1.0f;

            this.GetComponent<Rigidbody>().AddForce(direction * futtobi, ForceMode.Impulse);


            /////////////////////////////////////////////////////
            wasd.GetComponent<PlayerTransVelocity>().enabled = false;///////////
            /////////////////////////////////////////////////////

        }
    }
}


