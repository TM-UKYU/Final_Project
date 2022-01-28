using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathBall : MonoBehaviour
{
    //�ړ����x
    [SerializeField]
    private float MoveSpeed = 5;
    //��������
    [SerializeField]
    private float AliveTime = 50;
    //�o�ߎ���
    private float ElapsedTime = 0;

    //�U����L���ɂ��邩�ǂ���
    private bool enableAttack;
    //�r�̃R���C�_�Q
    private Collider[] SpikeCollider;
    //�U�������CharacterControlle
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        //�R���C�_���擾
        SpikeCollider = GetComponents<Collider>();
        //�e�X�e�[�^�X������
        ElapsedTime = 0;
        Destroy(gameObject, AliveTime);


    }

    // Update is called once per frame
    void Update()
    {
        //�����Ă���ΑO�����ɑO�i
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        ElapsedTime += Time.deltaTime;
        effect.GetComponent<SetEffects>().EffectUpdate(this.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�U�����L���Ȕ͈͂̃A�j���[�V�����łȂ���Ή������Ȃ�
        if (!enableAttack)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("�U�����q�b�g");

            var praticShockwaveChara = collision.gameObject.GetComponent<ParticleShockwaveChara>();
            //�L�������_���[�W��ԂłȂ���΃_���[�W��^����
            if (praticShockwaveChara.GetState() != ParticleShockwaveChara.State.damage)
            {
                praticShockwaveChara.Damage();
                //�L�������U�����󂯂��Ƃ��ɘr�Ƃ̏Փ˂𖳌��ɂ���
                IgnoreCollision(true);
            }
        }
    }

    public void ChangeEnableAttack(bool Flag)
    {
        //�U���J�n�ɂ̓L�����Ƙr�̏Փ˂�L���ɂ��Ă���
        if (Flag)
        {
            IgnoreCollision(false);
        }
    }

    //�_���[�W���̃L�����Ƙr�̏Փ˂�؂�ւ������\�b�h
    public void IgnoreCollision(bool Flag)
    {
        foreach (var item in SpikeCollider)
        {
            Physics.IgnoreCollision(item, characterController, Flag);
        }
    }
}
