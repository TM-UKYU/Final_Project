using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // �ʏ�V�[���J��
    public void LoadNextScene()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }

        //SceneFadeManager�̒��̃t�F�[�h�A�E�g�J�n�֐����Ăяo��
        fadeManager.fadeOutStart(0, 0, 0, 0, nextSceneName);
    }

    // ����V�[���J��
    public void LoadBattleScene()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }

        switch(StageInformation.Stage)
        {
            case StageInformation.STAGE_ID.CRAB: nextSceneName = "CrabBossScene"; break;
            case StageInformation.STAGE_ID.DRAGON: nextSceneName = "DragonScene";break;
            default: break;
        }

        //SceneFadeManager�̒��̃t�F�[�h�A�E�g�J�n�֐����Ăяo��
        fadeManager.fadeOutStart(0, 0, 0, 0, nextSceneName);
    }
}
