using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    //攻撃を有効にするかどうか
    private bool enableAttack;
    //腕のコライダ群
    private Collider[] SpikeCollider;
    //攻撃相手のCharacterControlle
    [SerializeField]
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        //腕に使っているコライダを取得
        SpikeCollider = GetComponents<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //攻撃が有効な範囲のアニメーションでなければ何もしない
        if (!enableAttack)
        {
            return;
        }

        if(collision.gameObject.tag=="Player")
        {
            Debug.Log("攻撃がヒット");

            var praticShockwaveChara = collision.gameObject.GetComponent<ParticleShockwaveChara>();
            //キャラがダメージ状態でなければダメージを与える
            if(praticShockwaveChara.GetState()!=ParticleShockwaveChara.State.damage)
            {
                praticShockwaveChara.Damage();
                //キャラが攻撃を受けたときに腕との衝突を無効にする
                IgnoreCollision(true);
            }
        }
    }

    public void ChangeEnableAttack(bool Flag)
    {
        //攻撃開始にはキャラと腕の衝突を有効にしておく
        if(Flag)
        {
            IgnoreCollision(false);
        }
    }

    //ダメージ時のキャラと腕の衝突を切り替えすメソッド
    public void IgnoreCollision(bool Flag)
    {
        foreach(var item in SpikeCollider)
        {
            Physics.IgnoreCollision(item, characterController,Flag);
        }
    }
}
