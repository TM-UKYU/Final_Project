using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
   //�Z�[�u�f�[�^
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

        // �Z�[�u�f�[�^������������
        //for(int i=1; i < 4; i++)
        //{
        //    if (!File.Exists(datastr + i + ".json"))
        //    {
        //        Debug.Log(datastr + i + ".json" + "�쐬");
        //        DefaultSave(i);
        //    }
        //}
    }

    // �Z�[�u
    public void Save(int fileNum)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(saveData);

        writer = new StreamWriter(Application.dataPath + "/save" + fileNum + ".json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    // �Q�[�����n�߂��Ƃ��ɃZ�[�u�f�[�^�����݂��Ȃ��Ƃ��Ƀf�t�H���g�̃Z�[�u�f�[�^1���쐬
    public void DefaultSave(int fileNum)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(saveData);

        writer = new StreamWriter(Application.dataPath + "/save"+ fileNum +".json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    // ���[�h
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

            // ���[�h�����f�[�^�ŏ㏑��
            saveData = JsonUtility.FromJson<SaveData>(datastr);

            // �f�o�b�O�\��
            Debug.Log(saveData.playerHP + "�̃f�[�^�����[�h���܂���");
            Debug.Log(saveData.playerLevel + "�̃f�[�^�����[�h���܂���");
            Debug.Log(saveData.playerPower + "�̃f�[�^�����[�h���܂���");
        }
    }

    public void DeleteSaveData(int fileNum)
    {
        string datastr = "";

        datastr = Application.dataPath + "/save" + fileNum + ".json";

        if (File.Exists(datastr))
        {
            File.Delete(datastr);

            Debug.Log("�폜");
        }
    }
}
