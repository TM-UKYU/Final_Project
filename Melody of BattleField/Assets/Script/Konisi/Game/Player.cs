using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStatus Status;
    
    // Start is called before the first frame update
    void Start()
    {
        Status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
