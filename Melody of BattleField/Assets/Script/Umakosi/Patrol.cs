using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] targets;
    private int num = 0;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
    }

    void Update()
    {
      
    }

    public void Prowling()
    {

        // �w�肳�ꂽ�`�F�b�N�|�C���g�Ɍ�����ς���B
        Vector3 vector3 = targets[num].transform.position - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        transform.rotation=Quaternion.Slerp(transform.rotation,quaternion,Time.deltaTime*1);

        // �x�N�g���̌v�Z
        moveDirection = targets[num].transform.position - transform.position;

        float speed = 3 * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targets[num].position, speed);


        // �ړI�n�ɋ߂Â�����A���̃`�F�b�N�|�C���g�ɐ؂�ւ���B
        if (Vector3.Distance(transform.position, targets[num].transform.position) < 10.0f)
        {
            // ������̃e�N�j�b�N
            num = (num + 1) % targets.Length;
            Debug.Log("�`�F�b�N�|�C���g�ʉ߁I");
        }
    }
}
