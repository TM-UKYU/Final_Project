using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathShot : MonoBehaviour
{
    //弾のオブジェクト
    public BreathBall breathBall;
    //蟹のクラス
    public CrabScript crabScript;
    //攻撃音
    public AudioClip attackSound;
    //ブレスの発射音
    public AudioClip FireSound;
    //音を管理するスクリプト
    public SoundManager soundManager;

    public void BreathFiring()
    {
        Debug.Log("弾発射");

        //弾の生成
        BreathBall obj = (BreathBall)Instantiate(breathBall, transform);
        //弾を発射した事をCrabScriotにも伝える
        crabScript.SetIsBreath(true);
        //弾を発射したときにも音を出す
        soundManager.SoundPlayOne(FireSound);
    }

    //蟹が攻撃したときに呼ばれる処理
    public void AttackEvent()
    {
        soundManager.SoundPlayOne(attackSound);
    }
}
