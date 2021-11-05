using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseMove : MonoBehaviour
{
    // スタート地点
    public Vector3 start;
    // ゴール地点
    public Vector3 goal;
    // 移動スピード
    public float speed = 0.005f;
    // 進捗度
    public float progress = 0.0f;
    // 進むか戻るか
    public bool moveFlg = true;
    // 使用するイージング関数
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
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            Vector3 vStart = start;
            Vector3 vGoal = goal;

            //目標地点までのベクトル
            Vector3 vTo = vGoal - vStart;

            Vector3 vNow = new Vector3 { };

            //進行具合を加味して現在の地点を割り出す
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

            //地点を中間に合わせる
            pos = vNow;

            //進行具合の更新
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

            myTransform.position = pos;  // 座標を設定
        }
		else
		{
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;

            Vector3 vStart = start;
            Vector3 vGoal = goal;

            //目標地点までのベクトル
            Vector3 vTo = vGoal - vStart;

            Vector3 vNow = new Vector3 { };
            
            //進行具合を加味して現在の地点を割り出す
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

            //地点を中間に合わせる
            pos = vNow;

            //進行具合の更新
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

            myTransform.position = pos;  // 座標を設定
        }
	}
}
