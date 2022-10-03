using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;
    private void Awake() 
    {
        PlayButton.onClick.AddListener(PlayGame);
        QuitButton.onClick.AddListener(QuitGame);
    }
    private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    private void QuitGame()
    {
        Debug.Log("Quit");
    }

}
