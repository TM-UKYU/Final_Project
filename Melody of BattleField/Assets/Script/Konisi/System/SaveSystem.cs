using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
   //セーブデータ
    [System.Serializable]
    public class SaveData
    {
        public int playerHP = 100;
        public int playerLevel = 1;
        public int playerPower = 10;
    }

    SaveData saveData = new SaveData();

    private void Start()
    {
        string datastr = "";

        //saveData.DroneSaveDatas = new List<DroneData>();

        datastr = Application.dataPath + "/save";

        // セーブデータが無かったら
        //for(int i=1; i < 4; i++)
        //{
        //    if (!File.Exists(datastr + i + ".json"))
        //    {
        //        Debug.Log(datastr + i + ".json" + "作成");
        //        DefaultSave(i);
        //    }
        //}
    }

    // セーブ
    public void Save(int fileNum)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(saveData);

        writer = new StreamWriter(Application.dataPath + "/save" + fileNum + ".json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    // ゲームを始めたときにセーブデータが存在しないときにデフォルトのセーブデータ1を作成
    public void DefaultSave(int fileNum)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(saveData);

        writer = new StreamWriter(Application.dataPath + "/save"+ fileNum +".json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    // ロード
    public void Load(int fileNum)
    {
        string datastr = "";
        StreamReader reader;

        datastr = Application.dataPath + "/save" + fileNum + ".json";

        if (File.Exists(datastr))
        {
            reader = new StreamReader(datastr);

            datastr = reader.ReadToEnd();
            reader.Close();

            // ロードしたデータで上書き
            saveData = JsonUtility.FromJson<SaveData>(datastr);

            // デバッグ表示
            Debug.Log(saveData.playerHP + "のデータをロードしました");
            Debug.Log(saveData.playerLevel + "のデータをロードしました");
            Debug.Log(saveData.playerPower + "のデータをロードしました");
        }
    }

    public void DeleteSaveData(int fileNum)
    {
        string datastr = "";

        datastr = Application.dataPath + "/save" + fileNum + ".json";

        if (File.Exists(datastr))
        {
            File.Delete(datastr);

            Debug.Log("削除");
        }
    }
}
