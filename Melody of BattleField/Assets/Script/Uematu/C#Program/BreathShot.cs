using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathShot : MonoBehaviour
{
    //弾のオブジェクト
    public BreathBall breathBall;
    //蟹のクラス
    public CrabScript crabScript;

    public void BreathFiring()
    {
        Debug.Log("弾発射");

        //弾の生成
        BreathBall obj = (BreathBall)Instantiate(breathBall, transform);
        //弾を発射した事をCrabScriotにも伝える
        crabScript.SetIsBreath(true);
    }
}
