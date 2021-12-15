using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int Hp = 80;
    private int MaxHp = 100;
    private int Stamina = 30;
    private int MaxStamina = 100;
    public StageInformation.WEAPON_ID Weapon = StageInformation.WEAPON_ID.KEYBOARD;

    public int GetHp() { return Hp; }
    public int GetMaxHp() { return MaxHp; }
    public int GetStamina() { return Stamina; }
    public int GetMaxStamina() { return MaxStamina; }
}
