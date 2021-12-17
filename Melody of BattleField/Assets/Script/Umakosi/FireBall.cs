using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Tooltip("�e�̔��ˏꏊ")]
    private GameObject enemy;
    private GameObject player;

    [SerializeField]
    [Tooltip("�e")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("�e�̑���")]
    private float speed = 100f;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("FireBallPoint");
        explosion = GameObject.Find("enemy");

        //�M�^�[���v���C���[�Ƃ��ĒǏ]
        player = GameObject.Find("Guitar");

        //�M�^�[���C���X�^���X������Ă��Ȃ���΃L�[�{�[�h�ɒǏ]
        if (player == null)
        {
            player = GameObject.Find("Keyboard");
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Bullet()
    {

        Debug.Log("����");
        Vector3 bulletPosition = enemy.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);
        newBall.transform.LookAt(player.transform);
        // �o���������{�[����forward(z������)
        Vector3 direction = newBall.transform.forward;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        // �o���������{�[���̖��O��"bullet"�ɕύX
        newBall.name = bullet.name;
        // �o���������{�[����8�b��ɏ���
        //Destroy(newBall, 8.0f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Player")
        {           
            explosion.GetComponent<explosionPoint>().explosion(other.ClosestPointOnBounds(this.transform.position));
            Destroy(this);
        }
    }
}
