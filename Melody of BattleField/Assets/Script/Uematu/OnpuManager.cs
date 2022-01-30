using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnpuManager : MonoBehaviour
{
    enum PreviousButton
    {
        Nome,
        Button1,
        Button2,
        Button3
    }

    public KeyCode Button1 = KeyCode.C;
    public KeyCode Button2 = KeyCode.V;
    public KeyCode Button3 = KeyCode.B;

    public Image Onpu1;
    public Image Onpu2;
    public Image Onpu3;

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
                Onpu3.enabled = true;
                previousButton = PreviousButton.Button3;
            }
            else
            {
                DestroyOnpu();
            }
        }
    }

    void DestroyOnpu()
    {
        Onpu1.enabled = false;
        Onpu2.enabled = false;
        Onpu3.enabled = false;
    }
}
