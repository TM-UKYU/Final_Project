using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    private GameObject enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        enemyAttack = GameObject.Find("enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            float damage;
            damage=enemyAttack.GetComponent<EnemyMove>().power;
            Debug.Log("Ç¢ÇƒÇ¡ÅI"+damage);
        }
    }
}
