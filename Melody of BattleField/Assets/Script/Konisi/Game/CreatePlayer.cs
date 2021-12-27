using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(StageInformation.Weapon == StageInformation.WEAPON_ID.KEYBOARD)
        {
            GameObject player_prefab = Resources.Load<GameObject>("Keyboard");
            GameObject player = Instantiate(player_prefab);
            player.transform.position = new Vector3(0, -1.80f, 0);
            player.name = "Keyboard";
        }
        else if(StageInformation.Weapon == StageInformation.WEAPON_ID.GUITAR)
        {
            GameObject player_prefab = Resources.Load<GameObject>("Guitar");
            GameObject player = Instantiate(player_prefab);
            player.transform.position = new Vector3(0, -1.80f, 0);
            player.name = "Guitar";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
