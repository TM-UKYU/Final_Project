using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInformation : MonoBehaviour
{
    // ステージ
    public enum STAGE_ID
    {
        CRAB,
        DRAGON,
        HARD
    };

    // 武器
    public enum WEAPON_ID
    {
        KEYBOARD,
        GUITAR
    }

    public static STAGE_ID Stage;
    public static WEAPON_ID Weapon;

    // Start is called before the first frame update
    void Start()
    {
        // シーンが遷移した後も消えないように
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
