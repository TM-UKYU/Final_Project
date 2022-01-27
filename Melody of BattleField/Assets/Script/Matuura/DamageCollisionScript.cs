using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollisionScript : MonoBehaviour
{
    private PlayerStatus ps;

    private void Start()
    {
        ps = this.gameObject.GetComponent<PlayerStatus>();
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("衝突したオブジェクト：" + gameObject.name);
        Debug.Log("衝突されたオブジェクト：" + other.gameObject.name);

        if (other.gameObject.tag == "EnemyAttackCol")
        {
            Debug.Log("ダメージ");
            ps.Hp--;
            return;
        }
    }
}
