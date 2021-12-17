using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Tooltip("弾の発射場所")]
    private GameObject enemy;
    private GameObject player;

    [SerializeField]
    [Tooltip("弾")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("弾の速さ")]
    private float speed = 100f;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("FireBallPoint");
        explosion = GameObject.Find("enemy");

        //ギターをプレイヤーとして追従
        player = GameObject.Find("Guitar");

        //ギターがインスタンス化されていなければキーボードに追従
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

        Debug.Log("発射");
        Vector3 bulletPosition = enemy.transform.position;
        // 上で取得した場所に、"bullet"のPrefabを出現させる
        GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);
        newBall.transform.LookAt(player.transform);
        // 出現させたボールのforward(z軸方向)
        Vector3 direction = newBall.transform.forward;
        // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
        newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        // 出現させたボールの名前を"bullet"に変更
        newBall.name = bullet.name;
        // 出現させたボールを8秒後に消す
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
