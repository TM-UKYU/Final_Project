using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioSource audioSource;

    void Start()
    {
        //�I�[�f�B�I�\�[�X���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlaySE(AudioClip audiodata)
    {
        audioSource.PlayOneShot(audiodata);
    }
}
