using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Includo il gestore input-output
using System.Runtime.Serialization.Formatters.Binary; // Includo il binary
using UnityEngine.SceneManagement; // Includo il gestore delle scene
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private GameObject mirino;
    public GameObject gameOverMenu;

    public Text scrittaVittoria;
    public GameObject riniziaLivello;
    public GameObject mainMenu;
    public GameObject quit;


    // Start is called before the first frame update
    void Start()
    {
        mirino = GameObject.FindGameObjectWithTag("Mirino");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverMenu.activeSelf && ((SceneManager.GetActiveScene().buildIndex < 4) || (SceneManager.GetActiveScene().buildIndex == 4 && !scrittaVittoria.enabled && !riniziaLivello.activeSelf && !mainMenu.activeSelf && !quit.activeSelf)))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        mirino.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuUI.SetActive(true);
        mirino.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }


    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("ScenaMenu");
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif



    }

    

  


}
