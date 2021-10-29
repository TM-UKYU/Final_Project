using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // 遷移先のシーンの名前
    public string nextSceneName;

    // シーン遷移
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
