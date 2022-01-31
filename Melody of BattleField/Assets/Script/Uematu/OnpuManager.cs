using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnpuManager : MonoBehaviour
{
    //前に何を押したか
    enum PreviousButton
    {
        Nome,
        Button1,
        Button2,
        Button3
    }

    //キーコード
    public KeyCode Button1 = KeyCode.C;
    public KeyCode Button2 = KeyCode.V;
    public KeyCode Button3 = KeyCode.B;

    public KeyCode EndButton = KeyCode.Escape;

    //音符のイメージ
    public Image Onpu1;
    public Image Onpu2;
    public Image Onpu3;

    public GameObject Dagger;

    private PreviousButton previousButton = PreviousButton.Nome;

    private void Start()
    {
        DestroyOnpu();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(Button1))
        {
            Onpu1.enabled = true;
            previousButton = PreviousButton.Button1;
        }

        if (Input.GetKeyDown(Button2))
        {
            if (previousButton == PreviousButton.Button1)
            {
                Onpu2.enabled = true;
                previousButton = PreviousButton.Button2;
            }
            else
            {
                DestroyOnpu();
            }
        }

        if (Input.GetKeyDown(Button3))
        {
            if (previousButton == PreviousButton.Button2)
            {
                DestroyOnpu();

                //Onpu3.enabled = true;
                previousButton = PreviousButton.Button3;

                GameObject obj = GameObject.FindGameObjectWithTag("Player");

                Instantiate(Dagger, obj.gameObject.transform.position + Dagger.transform.position,
                Quaternion.Euler(0, (this.transform.rotation.y * 100) + 180, 0));
            }
            else
            {
                DestroyOnpu();
            }
        }

        if (Input.GetKeyDown(EndButton))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    void DestroyOnpu()
    {
        Onpu1.enabled = false;
        Onpu2.enabled = false;
        Onpu3.enabled = false;
    }
}
