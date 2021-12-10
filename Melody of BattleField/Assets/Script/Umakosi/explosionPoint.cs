using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject effect;
    private GameObject enemy;

    void Start()
    {
        enemy = GameObject.Find("enemy");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void explosion(Vector3 hitPos)
    {
        Vector3 bulletPosition = hitPos;
        GameObject newBall = Instantiate(effect, bulletPosition, transform.rotation);
        Destroy(newBall, 2.0f);
    }
}
