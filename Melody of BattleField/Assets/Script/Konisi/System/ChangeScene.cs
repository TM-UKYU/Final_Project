using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // �J�ڐ�̃V�[���̖��O
    public string nextSceneName;

    GameObject manageObject;

    SceneFadeManager fadeManager;

    private void Start()
    {
        //SceneFadeManager���A�^�b�`����Ă���I�u�W�F�N�g���擾
        manageObject = GameObject.Find("FadeManageObject");
        
        //�I�u�W�F�N�g�̒���SceneFadeManager���擾
        fadeManager = manageObject.GetComponent<SceneFadeManager>();
    }

    // �V�[���J��
    public void LoadNextScene()
    {
        //SceneFadeManager�̒��̃t�F�[�h�A�E�g�J�n�֐����Ăяo��
        fadeManager.fadeOutStart(0, 0, 0, 0, nextSceneName);
    }
}
