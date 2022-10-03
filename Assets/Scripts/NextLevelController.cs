using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelController : MonoBehaviour



{
    public Button Replay;
    public Button LoadLevel;    
    public Button Quit;    

    
    private void Awake() 
    {
        Replay.onClick.AddListener(ReloadLevel);
        LoadLevel.onClick.AddListener(LoadNext);
        Quit.onClick.AddListener(QuitGame);
    }
    public void LevelComplete()
    {
        gameObject.SetActive(true); 
    }
    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadNext()
    {
        SceneManager.LoadScene(2);
    }
    private void QuitGame()
    {
        Debug.Log("Quit Game");
    }
}