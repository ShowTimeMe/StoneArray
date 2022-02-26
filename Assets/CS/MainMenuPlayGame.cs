using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuPlayGame : MonoBehaviour
{
    public Button btnStart;
    public Button btnQuit;
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(delegate { PlayGame("Game"); });
        btnQuit.onClick.AddListener(GameQuit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame(string s)
    {
        SceneManager.LoadScene(s);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
}
