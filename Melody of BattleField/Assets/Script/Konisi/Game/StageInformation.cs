using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInformation : MonoBehaviour
{
    // �X�e�[�W
    public enum STAGE_ID
    {
        CRAB,
        DRAGON,
        HARD
    };

    // ����
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
        // �V�[�����J�ڂ�����������Ȃ��悤��
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
