using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�e�̔��ˏꏊ")]
    private GameObject enemy;

    [SerializeField]
    [Tooltip("�e")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("�e�̑���")]
    private float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("FireBallPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Bullet();
        }

    }

    public void Bullet()
    {

        Debug.Log("����");
        Vector3 bulletPosition = enemy.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);
        // �o���������{�[����forward(z������)
        Vector3 direction = newBall.transform.forward;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        // �o���������{�[���̖��O��"bullet"�ɕύX
        newBall.name = bullet.name;
        // �o���������{�[����8�b��ɏ���
        Destroy(newBall, 8.0f);
    }
}
