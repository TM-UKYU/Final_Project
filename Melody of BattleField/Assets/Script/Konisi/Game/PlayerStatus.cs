using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int hp = 100;
    private int maxHp = 100;
    private int stamina = 100;
    private int maxStamina = 100;
    public StageInformation.WEAPON_ID Weapon = StageInformation.WEAPON_ID.KEYBOARD;

    public int Hp
    {
        set //値をhpに代入する
        {
            this.hp = value;
            if (this.hp < 0)
            {
                this.hp = 0;
            }
        }
        get //値を返す
        {
            return this.hp;
        }
    }

    public int MaxHp
    {
        set //値をMaxHpに代入する
        {
            this.maxHp = value;
            if (this.maxHp < 0)
            {
                this.maxHp = 0;
            }
        }
        get //値を返す
        {
            return this.maxHp;
        }
    }

    public int Stamina
    {
        set //値をstaminaに代入する
        {
            this.stamina = value;
            if (this.stamina < 0)
            {
                this.stamina = 0;
            }
        }
        get //値を返す
        {
            return this.stamina;
        }
    }

    public int MaxStamina
    {
        set //値をmaxStaminaに代入する
        {
            this.maxStamina = value;
            if (this.maxStamina < 0)
            {
                this.maxStamina = 0;
            }
        }
        get //値を返す
        {
            return this.maxStamina;
        }
    }

    //public int GetHp() { return Hp; }
    //public int GetMaxHp() { return MaxHp; }
    //public int GetStamina() { return Stamina; }
    //public int GetMaxStamina() { return MaxStamina; }

    //public void SetHp(int hp) { Hp = hp; }
    //public void SetMaxHp(int maxHp) { MaxHp = maxHp; }
    //public void SetStamina(int stamina) { Stamina = stamina; }
    //public void SetMaxStamina(int maxStamina) { MaxStamina = maxStamina; }
}
