using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class UIManager : MonoBehaviour
{
    public TMP_InputField playerName;
    public TextMeshProUGUI liderScoreText;
    public TextMeshProUGUI liderNameText;

    void Start()
    {
        playerName.text = PersistentData.Instance.playerName;
        SetRecordText();
    }

    public void SetPlayerName()
    {
        PersistentData.Instance.playerName = playerName.text;
    }
    public void NewStart()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        PersistentData.Instance.SaveRecord();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void SetRecordText()
    {
        liderScoreText.text = "Highscore: "+PersistentData.Instance.liderScore;
        liderNameText.text = "Lider: "+PersistentData.Instance.liderName;
    }
}
