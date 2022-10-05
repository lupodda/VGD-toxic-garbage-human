using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Includo il gestore input-output
using System.Runtime.Serialization.Formatters.Binary; // Includo il binary
using UnityEngine.SceneManagement; // Includo il gestore delle scene

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject modalitaMenu;
    public GameObject loadLevelMenu;
    public GameObject trama;

    public void NewGame()
    {
        mainMenu.SetActive(false);
        modalitaMenu.SetActive(true);

        
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate); // Riga uguale al salvataggio
        GameData loadedData = (GameData)bf.Deserialize(file); // Deserialize restituisce un oggetto generico, bisogna castarlo a GameData
        file.Close(); // Chiude il file
        Debug.Log("file loaded successfully"); // Per verificare che il caricamento sia andato a buon fine
        SceneManager.LoadScene(loadedData.getSceneName()); // Carica la scena salvata

    }


    public void LoadLevelMenu()
    {
        mainMenu.SetActive(false);
        loadLevelMenu.SetActive(true);


    }

    public void QuitGame()
    {
    #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif



    }


    public void DifficoltaFacile()
    {
        PlayerPrefs.SetInt("Difficolta", 0); // 0 = facile
        modalitaMenu.SetActive(false);
        trama.SetActive(true);

    }

    public void DifficoltaIntermedia()
    {
        PlayerPrefs.SetInt("Difficolta", 1); // 1 = intermedia
        modalitaMenu.SetActive(false);
        trama.SetActive(true);

    }

    public void DifficoltaDifficile()
    {
        PlayerPrefs.SetInt("Difficolta", 2); // 2 = difficile
        modalitaMenu.SetActive(false);
        trama.SetActive(true);


    }

    public void IniziaGioco()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }


    public void LoadLevel1()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(2);
    }

    public void LoadLevel3()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(3);
    }

    public void LoadLevel4()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(4);
    }
}
