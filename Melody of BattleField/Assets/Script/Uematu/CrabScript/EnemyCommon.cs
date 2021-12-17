using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    //敵のHP
    [SerializeField]
    private float HitPoint = 50;
    //怯み値
    [SerializeField]
    private float FrightenedNum = 10;
    //現在の怯み値
    private float NowFrightened = 0;
    //当たり判定のコライダ―
    public Collider HitCollider;

    void Start()
    {
        HitCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            //ココマジックナンバーだけど後でプレイヤーが管理している攻撃力に変更
            DecHP(10);
            //上に同じ
            DecFrightened(1);
        }
    }

    //ダメージを食らった時HPの計算
    public void DecHP(float Damage)
    {
        //HPを減らす
        HitPoint -= Damage;
    }

    public bool CheckDeath()
    {
        //HPが0以下になったらTrueを返す
        if (HitPoint <= 0)
        {
            return true;
        }

        return false;
    }

    //ダメージを食らった時の怯み値の計算
    private void DecFrightened(float DecNum)
    {
        //怯み値を増加させる
        NowFrightened += DecNum;
    }

    public bool CheckFrightened()
    {
        //怯み値が一定値を超えたらTrueを返す
        if (FrightenedNum <= NowFrightened)
        {
            NowFrightened = 0;
            return true;
        }

        return false;
    }
}
