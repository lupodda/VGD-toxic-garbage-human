using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandiMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject comandiMenu;
    public GameObject mirino;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && comandiMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
            comandiMenu.SetActive(false);
            pauseMenu.SetActive(true);
            mirino.SetActive(false);
            Time.timeScale = 0f;
            PauseMenu.GameIsPaused = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        }
    }
}
