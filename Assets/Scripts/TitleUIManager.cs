using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleUIManager : MonoBehaviour
{
    private int mainSceneNumber = 1;
    private string playerName;
    public TMP_InputField inputName;
    private string topTextPath = "/Canvas/TopText";
    private TextMeshProUGUI topText;

    private void Start()
    {
        DataPersistence.Instance.LoadScore();
        inputName.onValueChanged.AddListener(delegate { SetPlayerName(); });
        topText = GameObject.Find(topTextPath).GetComponent<TextMeshProUGUI>();
        if (DataPersistence.Instance.bestScore > 0)
        {
            topText.text = $"Top: {DataPersistence.Instance.bestPlayer} - {DataPersistence.Instance.bestScore}";
        }
        Debug.Log(DataPersistence.Instance.bestPlayer + DataPersistence.Instance.bestScore);

    }
    

    public void Play()
    {
        DataPersistence.Instance.playerName = playerName;
        if (string.IsNullOrEmpty(DataPersistence.Instance.playerName)) { DataPersistence.Instance.playerName = "???"; }
        SceneManager.LoadScene(mainSceneNumber);
        
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetPlayerName()
    {
        playerName = inputName.text;
    }
}
