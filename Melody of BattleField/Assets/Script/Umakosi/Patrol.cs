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

        // 指定されたチェックポイントに向きを変える。
        Vector3 vector3 = targets[num].transform.position - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        transform.rotation=Quaternion.Slerp(transform.rotation,quaternion,Time.deltaTime*1);

        // ベクトルの計算
        moveDirection = targets[num].transform.position - transform.position;

        float speed = 3 * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targets[num].position, speed);


        // 目的地に近づいたら、次のチェックポイントに切り替える。
        if (Vector3.Distance(transform.position, targets[num].transform.position) < 10.0f)
        {
            // 順送りのテクニック
            num = (num + 1) % targets.Length;
            Debug.Log("チェックポイント通過！");
        }
    }
}
