using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAccessor : MonoBehaviour
{
    public void SetCrabStage()
    {
        StageInformation.Stage = StageInformation.STAGE_ID.CRAB;
        Debug.Log(StageInformation.Stage);
    }

    public void SetNormalStage()
    {
        StageInformation.Stage = StageInformation.STAGE_ID.NORMAL;
    }

    public void SetHardStage()
    {
        StageInformation.Stage = StageInformation.STAGE_ID.HARD;
    }

    public void SetKeyBoardWeapon()
    {
        StageInformation.Weapon = StageInformation.WEAPON_ID.KeyBoard;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
