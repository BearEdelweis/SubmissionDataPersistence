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
        //создание экземпл€р€ сериализуемого класса SaveData/create new SaveData
        SaveData data = new SaveData();
        //запись в этот экземпл€р значений, которые нужно сохранить/set variables in data from app
        data.serScore = PersistentData.Instance.liderScore;
        data.serName = PersistentData.Instance.liderName;
        //сериализаци€ в string экземпл€ра SaveData с записанными данными/serialize to string
        string json = JsonUtility.ToJson(data);
        //сохранение полученного значени€ string в файл (using System.IO)/save to file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadRecord()
    {
        //извлечение string из существующего json файла путем указан€ пути/extract string from json file:
        //cначала указать путь, потом проверить через if существует ли по этому пути файл/for the first get path
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)) //если файл по пути path существует, тогда:/check if path exists, then
        {
            //извлечение string из существующего json файла//extract string from json file
            string json = File.ReadAllText(path);
            //десериализаци€ экземпл€р€ SaveData из string (€вно указать <тип>)/deserialize SaveData from strin (it needs <template argument>)
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //присвоение переменным в текущей сессии значений, сохраненных ранее//set variables in app from data
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
