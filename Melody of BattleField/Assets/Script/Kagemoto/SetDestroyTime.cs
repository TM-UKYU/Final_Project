using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestroyTime : MonoBehaviour
{
    [SerializeField] private float DestroyTime = 3.0f; // 生存時間
    [SerializeField] private float aliveTime = 0.0f;

    void Start()
    {
        DestroyTime = 5.0f;
    }


    void Update()
    {
        // 1秒に+1カウント
        aliveTime += Time.deltaTime;

        if (aliveTime >= DestroyTime) { Destroy(this.gameObject); }
    }
}
