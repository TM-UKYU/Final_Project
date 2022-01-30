using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = (GameObject)Resources.Load("HitEffect");

        // プレハブを元にオブジェクトを生成する
        GameObject instance = (GameObject)Instantiate(obj,new Vector3(5.0f, 0.0f, 0.0f),Quaternion.identity);

        if (collision.gameObject.GetComponent<PlayerStatus>())
        {
            collision.gameObject.GetComponent<PlayerStatus>().Hp -= 10;
        }
    }
}
