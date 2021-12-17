using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDagger : MonoBehaviour
{
    private GameObject daggerScript1;
    private GameObject daggerScript2;
    private GameObject daggerScript3;

    public GameObject Player;

    public AudioClip audioClip;

    private SoundManager soundManager;

    //攻撃をするキー
    public KeyCode AttackKey_1 = KeyCode.C;
    public KeyCode AttackKey_2 = KeyCode.X;
    public KeyCode AttackKey_3 = KeyCode.Z;

    private void Start()
    {
        GameObject obj = GameObject.Find("SoundManager");
        soundManager = obj.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Create();
    }

    //ダガーを子オブジェクトとして生成
    void Create()
    {
        Transform DaggerPos = gameObject.transform;
        //GameObject obj;

        if (Input.GetKeyUp(AttackKey_1))
        {
            daggerScript1 = (GameObject)Resources.Load("Dagger1");

            //obj = Instantiate(daggerScript1, daggerScript1.transform.position, Quaternion.Euler(0, 90, 0));
            //obj.transform.parent = gameObject.transform;

            //DaggerPos.position + daggerScript1.transform.position;
            Debug.Log(Player.transform.rotation.y * 100);
            Instantiate(daggerScript1, DaggerPos.position + daggerScript1.transform.position,
                Quaternion.Euler(0, (this.transform.rotation.y * 100) + 180, 0));
            soundManager.SoundPlayOne(audioClip);
        }
        else if (Input.GetKeyUp(AttackKey_2))
        {
            daggerScript2 = (GameObject)Resources.Load("Dagger2");

            Instantiate(daggerScript2, DaggerPos.position + daggerScript2.transform.position,
                Quaternion.Euler(0, (this.transform.rotation.y * 100) + 180, 0));
        }
        else if (Input.GetKeyUp(AttackKey_3))
        {
            daggerScript3 = (GameObject)Resources.Load("Dagger3");

            Instantiate(daggerScript3, DaggerPos.position + daggerScript3.transform.position,
                Quaternion.Euler(0, (this.transform.rotation.y * 100) + 180, 0));
        }
    }
}
