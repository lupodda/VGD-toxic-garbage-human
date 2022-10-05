using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Includo il gestore input-output
using System.Runtime.Serialization.Formatters.Binary; // Includo il binary
using UnityEngine.SceneManagement; // Includo il gestore delle scene
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{

    public GameObject mirino;

    public GameObject gameOverMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif




    }

    public void RetryGame()
    {

        //Cursor.lockState = CursorLockMode.Locked;
        //PauseMenu.GameIsPaused = false;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        Time.timeScale = 1f;
        mirino.SetActive(true);

        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);



    }

    public void LoadMenu()
    {
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("ScenaMenu");
    }



}
