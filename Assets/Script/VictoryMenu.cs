using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryMenu : MonoBehaviour
{
    public GameObject mirino;

    public GameObject victoryMenu;
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

    public void RiniziaLivello()
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
        victoryMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("ScenaMenu");
    }
}
