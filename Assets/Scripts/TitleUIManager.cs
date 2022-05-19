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

    private void Start()
    {
        inputName.onValueChanged.AddListener(delegate { SetPlayerName(); });
    }
    

    public void Play()
    {
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
        Debug.Log(playerName);
    }
}
