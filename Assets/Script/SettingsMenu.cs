using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject mirino;

    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && settingsMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            mirino.SetActive(false);
            Time.timeScale = 0f;
            PauseMenu.GameIsPaused = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;


        }
    }

    public void ChangeMouseSensitivity(float sliderValue)
    {

        GameController.mouseSensitivity = sliderValue;



    }

    public void ChangeVolume(float sliderValue)
    {

        GameObject.Find("GameController").GetComponents<AudioSource>()[0].volume = sliderValue;
        GameObject.Find("GameController").GetComponents<AudioSource>()[1].volume = sliderValue;
        GameObject.Find("Giocatore").GetComponent<AudioSource>().volume = sliderValue;
        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < GameObject.Find("Spawner").GetComponent<Spawner>().numberOfEnemies; i++)
            {
                enemies[i].GetComponent<AudioSource>().volume = sliderValue;
            }

        } else if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < GameObject.Find("Spawner").GetComponent<SpawnerLevel3>().numberOfEnemies; i++)
            {
                enemies[i].GetComponent<AudioSource>().volume = sliderValue;
            }
        }

        if (sliderValue<0.1f && sliderValue>=0)
        {
            GameObject.Find("ost").GetComponent<AudioSource>().volume = sliderValue;
        }
        else
        {
            GameObject.Find("ost").GetComponent<AudioSource>().volume = 0.1f;
        }


    }
}
