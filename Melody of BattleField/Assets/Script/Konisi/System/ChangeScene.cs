using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // �J�ڐ�̃V�[���̖��O
    public string nextSceneName;

    // �V�[���J��
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
