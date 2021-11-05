using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseMove : MonoBehaviour
{
    // �X�^�[�g�n�_
    public Vector3 start;
    // �S�[���n�_
    public Vector3 goal;
    // �ړ��X�s�[�h
    public float speed = 0.005f;
    // �i���x
    public float progress = 0.0f;
    // �i�ނ��߂邩
    public bool moveFlg = true;
    // �g�p����C�[�W���O�֐�
    public Easing easing = Easing.easeInSin;

    public enum Easing
    {
        none,
        easeInSin,
        easeOutSin,
        easeInOutSin
    }

    public float easeInSin(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
    
    public float easeOutSin(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }

    public float easeInOutSin(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }

    public void Update()
    {
		if (moveFlg)
		{
            // transform���擾
            Transform myTransform = this.transform;

            // ���W���擾
            Vector3 pos = myTransform.position;

            Vector3 vStart = start;
            Vector3 vGoal = goal;

            //�ڕW�n�_�܂ł̃x�N�g��
            Vector3 vTo = vGoal - vStart;

            Vector3 vNow = new Vector3 { };

            //�i�s����������Č��݂̒n�_������o��
            switch (easing)
            {
                case Easing.none:
                    vNow = vStart + vTo;
                    break;
                case Easing.easeInSin:
                    vNow = vStart + vTo * easeInSin(progress);
                    break;
                case Easing.easeOutSin:
                    vNow = vStart + vTo * easeOutSin(progress);
                    break;
                case Easing.easeInOutSin:
                    vNow = vStart + vTo * easeInOutSin(progress);
                    break;
                default: 
                    break;
            }

            //�n�_�𒆊Ԃɍ��킹��
            pos = vNow;

            //�i�s��̍X�V
            progress += speed;
            if (progress >= 1.0f)
            {
                progress = 1.0f;
                moveFlg = false;
            }
            else
            {
                moveFlg = true;
            }

            myTransform.position = pos;  // ���W��ݒ�
        }
		else
		{
            // transform���擾
            Transform myTransform = this.transform;

            // ���W���擾
            Vector3 pos = myTransform.position;

            Vector3 vStart = start;
            Vector3 vGoal = goal;

            //�ڕW�n�_�܂ł̃x�N�g��
            Vector3 vTo = vGoal - vStart;

            Vector3 vNow = new Vector3 { };
            
            //�i�s����������Č��݂̒n�_������o��
            switch (easing)
            {
                case Easing.none:
                    vNow = vStart + vTo;
                    break;
                case Easing.easeInSin:
                    vNow = vStart + vTo * easeInSin(progress);
                    break;
                case Easing.easeOutSin:
                    vNow = vStart + vTo * easeOutSin(progress);
                    break;
                case Easing.easeInOutSin:
                    vNow = vStart + vTo * easeInOutSin(progress);
                    break;
                default:
                    break;
            }

            //�n�_�𒆊Ԃɍ��킹��
            pos = vNow;

            //�i�s��̍X�V
            progress -= speed;
            if (progress <= 0.0f)
            {
                progress = 0.0f;
                moveFlg = true;
            }
            else
            {
                moveFlg = false;
            }

            myTransform.position = pos;  // ���W��ݒ�
        }
	}
}
