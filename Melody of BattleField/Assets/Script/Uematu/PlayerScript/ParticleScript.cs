using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    //private CrabScript crabScript;
    private GameObject grandObject;
    //�p�[�e�B�N���V�X�e��
    private ParticleSystem ps;
    //ScaleUp�p�̌o�ߎ���
    private float elapsedScaleUpTime = 0f;
    //Scale��傫������Ԋu����
    [SerializeField]
    private float scaleUpTime = 0.03f;
    //ScaleUp���銄��
    [SerializeField]
    private float scaleUpParam = 0.1f;
    //�p�[�e�B�N���폜�p�̌o�ߎ���
    private float elapsedDeleteTime = 0f;
    //�p�[�e�B�N�����폜����܂ł̎���
    [SerializeField]
    private float deleteTime = 5f;
    //���̃p�[�e�B�N���̓����x
    private float alphaValue = 1f;
    //�p�[�e�B�N���̓����������̃_���[�W
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
                //�p�[�e�B�N����i�X�Ɠ��������鏈��
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

                //�p�[�e�B�N���f�[�^�̐ݒ�
                ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);
            }

        }
    }

    //�p�[�e�B�N���̓����蔻��
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            //�G�Ƀp�[�e�B�N���������������̏���
            //���Ȃ瓖��������_���[�W
            other.gameObject.GetComponent<EnemyCommon>().DecHP(Damage);
        }
    }

    private void OnParticleTrigger()
    {
        if (ps != null)
        {
            //Particle�^�̃C���X�^���X
            List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
            List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

            //Inside,Enter�̃p�[�e�B�N�����擾
            int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

            //�f�[�^������΃L�����N�^�[�ɐڐG����
            if (numInside != 0 || numEnter != 0)
            {
                Debug.Log("�p�[�e�B�N���ڐG");
                //if(crabScript.GetStatus()!=CrabScript.CrabState.Frightened)
                //{

                //}
            }

            //������₷���ڐG�����p�[�e�B�N����ԐF�ɕύX
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

            //�p�[�e�B�N���f�[�^�̐ݒ�
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        }
    }
}
