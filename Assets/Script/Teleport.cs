using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Includo il gestore delle scene

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Quando il player collide con il portale entra in questa funzione e cambia scena
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Se il portale collide con il player
        {

            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);

        }
    }
}
