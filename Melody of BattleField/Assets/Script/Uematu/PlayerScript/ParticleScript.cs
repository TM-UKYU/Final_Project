using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    //private CrabScript crabScript;
    private GameObject grandObject;
    //パーティクルシステム
    private ParticleSystem ps;
    //ScaleUp用の経過時間
    private float elapsedScaleUpTime = 0f;
    //Scaleを大きくする間隔時間
    [SerializeField]
    private float scaleUpTime = 0.03f;
    //ScaleUpする割合
    [SerializeField]
    private float scaleUpParam = 0.1f;
    //パーティクル削除用の経過時間
    private float elapsedDeleteTime = 0f;
    //パーティクルを削除するまでの時間
    [SerializeField]
    private float deleteTime = 5f;
    //元のパーティクルの透明度
    private float alphaValue = 1f;
    //パーティクルの当たった時のダメージ
    [SerializeField]
    private float Damage = 30;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        //crabScript = GameObject.Find("CrabBoss").GetComponent<CrabScript>();
        //ps.trigger.SetCollider(0, crabScript.transform);
        //ps.trigger.SetCollider(0, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedDeleteTime += Time.deltaTime;
        elapsedScaleUpTime += Time.deltaTime;

        if (elapsedDeleteTime >= deleteTime)
        {
            Destroy(gameObject);
        }

        if (elapsedScaleUpTime > scaleUpTime)
        {
            transform.localScale += new Vector3(scaleUpParam, scaleUpParam, scaleUpParam);
            elapsedScaleUpTime = 0f;

            if (ps != null)
            {
                //パーティクルを段々と透けさせる処理
                List<ParticleSystem.Particle> outside = new List<ParticleSystem.Particle>();

                int numOutside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);

                alphaValue -= (1f / deleteTime) * Time.deltaTime;

                alphaValue = (alphaValue <= 0f) ? 0f : alphaValue;

                for (int i = 0; i < numOutside; i++)
                {
                    ParticleSystem.Particle p = outside[i];
                    p.startColor = new Color(1f, 1f, 1f, alphaValue);
                    outside[i] = p;
                }

                //パーティクルデータの設定
                ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);
            }

        }
    }

    //パーティクルの当たり判定
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            //敵にパーティクルが当たった時の処理
            //今なら当たったらダメージ
            other.gameObject.GetComponent<EnemyCommon>().DecHP(Damage);
        }
    }

    private void OnParticleTrigger()
    {
        if (ps != null)
        {
            //Particle型のインスタンス
            List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
            List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

            //Inside,Enterのパーティクルを取得
            int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

            //データがあればキャラクターに接触した
            if (numInside != 0 || numEnter != 0)
            {
                Debug.Log("パーティクル接触");
                //if(crabScript.GetStatus()!=CrabScript.CrabState.Frightened)
                //{

                //}
            }

            //分かりやすく接触したパーティクルを赤色に変更
            for (int i = 0; i < numInside; i++)
            {
                ParticleSystem.Particle p = inside[i];
                p.startColor = new Color32(255, 0, 0, 255);
                inside[i] = p;
            }

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = new Color32(255, 0, 0, 255);
                enter[i] = p;
            }

            //パーティクルデータの設定
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        }
    }
}
