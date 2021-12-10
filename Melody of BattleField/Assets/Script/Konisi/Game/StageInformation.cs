using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInformation : MonoBehaviour
{
    public enum STAGE_ID
    {
        CRAB,
        DRAGON,
        HARD
    };

    public enum WEAPON_ID
    {
        KeyBoard
    }

    public static STAGE_ID Stage;
    public static WEAPON_ID Weapon;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
