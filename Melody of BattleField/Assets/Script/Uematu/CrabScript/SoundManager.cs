using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //音を鳴らすためのコンポーネント
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundPlayOne(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
