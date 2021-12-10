using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAccessor : MonoBehaviour
{
    // �X�e�[�W�̊e����StageInformation�ɓn��
    public void SetCrabStage()
    {
        StageInformation.Stage = StageInformation.STAGE_ID.CRAB;
        Debug.Log(StageInformation.Stage);
    }

    public void SetDragonStage()
    {
        StageInformation.Stage = StageInformation.STAGE_ID.DRAGON;
    }

    public void SetHardStage()
    {
        StageInformation.Stage = StageInformation.STAGE_ID.HARD;
    }

    public void SetKeyBoardWeapon()
    {
        StageInformation.Weapon = StageInformation.WEAPON_ID.KEYBOARD;
    }

    public void SetGuitar()
    {
        StageInformation.Weapon = StageInformation.WEAPON_ID.GUITAR;
    }
}
