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

        Debug.Log("�Փ˂����I�u�W�F�N�g�F" + gameObject.name);
        Debug.Log("�Փ˂��ꂽ�I�u�W�F�N�g�F" + other.gameObject.name);

        if (other.gameObject.tag == "EnemyAttackCol")
        {
            Debug.Log("�_���[�W");
            ps.Hp--;
            return;
        }
    }
}
