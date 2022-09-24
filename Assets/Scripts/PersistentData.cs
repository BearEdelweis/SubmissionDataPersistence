using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class PersistentData : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public int serScore;
        public string serName;
    }

    public void SaveRecord()
    {
        //�������� ���������� �������������� ������ SaveData/create new SaveData
        SaveData data = new SaveData();
        //������ � ���� ��������� ��������, ������� ����� ���������/set variables in data from app
        data.serScore = PersistentData.Instance.liderScore;
        data.serName = PersistentData.Instance.liderName;
        //������������ � string ���������� SaveData � ����������� �������/serialize to string
        string json = JsonUtility.ToJson(data);
        //���������� ����������� �������� string � ���� (using System.IO)/save to file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadRecord()
    {
        //���������� string �� ������������� json ����� ����� ������� ����/extract string from json file:
        //c������ ������� ����, ����� ��������� ����� if ���������� �� �� ����� ���� ����/for the first get path
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)) //���� ���� �� ���� path ����������, �����:/check if path exists, then
        {
            //���������� string �� ������������� json �����//extract string from json file
            string json = File.ReadAllText(path);
            //�������������� ���������� SaveData �� string (���� ������� <���>)/deserialize SaveData from strin (it needs <template argument>)
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //���������� ���������� � ������� ������ ��������, ����������� �����//set variables in app from data
            PersistentData.Instance.liderScore = data.serScore;
            PersistentData.Instance.liderName = data.serName;
        }
    }

    //my singleton
    public static PersistentData Instance;
    public string liderName;
    public int liderScore;

    public string playerName;

    void Awake()
    {
        if(PersistentData.Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        LoadRecord();
    }
}
