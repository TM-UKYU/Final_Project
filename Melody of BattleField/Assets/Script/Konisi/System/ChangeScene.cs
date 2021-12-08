using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // 遷移先のシーンの名前
    public string nextSceneName;

    GameObject manageObject;

    SceneFadeManager fadeManager;

    private void Start()
    {
        //SceneFadeManagerがアタッチされているオブジェクトを取得
        manageObject = GameObject.Find("FadeManageObject");
        
        //オブジェクトの中のSceneFadeManagerを取得
        fadeManager = manageObject.GetComponent<SceneFadeManager>();
    }

    // 通常シーン遷移
    public void LoadNextScene()
    {
        //SceneFadeManagerの中のフェードアウト開始関数を呼び出し
        fadeManager.fadeOutStart(0, 0, 0, 0, nextSceneName);
    }

    // 分岐シーン遷移
    public void LoadBattleScene()
    {
        switch(StageInformation.Stage)
        {
            case StageInformation.STAGE_ID.CRAB: nextSceneName = "CrabBossScene";Debug.Log("CrabBossScene"); break;
            case StageInformation.STAGE_ID.DRAGON: nextSceneName = "DragonScene";break;
            default: break;
        }

        //SceneFadeManagerの中のフェードアウト開始関数を呼び出し
        fadeManager.fadeOutStart(0, 0, 0, 0, nextSceneName);
    }
}
