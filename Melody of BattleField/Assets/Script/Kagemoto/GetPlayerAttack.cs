using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerAttack : MonoBehaviour
{
    [SerializeField] private int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life<=0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDamage(int damege)
    {
        life -= damege;
    }
}
