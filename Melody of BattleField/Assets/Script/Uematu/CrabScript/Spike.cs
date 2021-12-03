using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    //�U����L���ɂ��邩�ǂ���
    private bool enableAttack;
    //�r�̃R���C�_�Q
    private Collider[] SpikeCollider;
    //�U�������CharacterControlle
    [SerializeField]
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        //�r�Ɏg���Ă���R���C�_���擾
        SpikeCollider = GetComponents<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�U�����L���Ȕ͈͂̃A�j���[�V�����łȂ���Ή������Ȃ�
        if (!enableAttack)
        {
            return;
        }

        if(collision.gameObject.tag=="Player")
        {
            Debug.Log("�U�����q�b�g");

            var praticShockwaveChara = collision.gameObject.GetComponent<ParticleShockwaveChara>();
            //�L�������_���[�W��ԂłȂ���΃_���[�W��^����
            if(praticShockwaveChara.GetState()!=ParticleShockwaveChara.State.damage)
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
        if(Flag)
        {
            IgnoreCollision(false);
        }
    }

    //�_���[�W���̃L�����Ƙr�̏Փ˂�؂�ւ������\�b�h
    public void IgnoreCollision(bool Flag)
    {
        foreach(var item in SpikeCollider)
        {
            Physics.IgnoreCollision(item, characterController,Flag);
        }
    }
}
