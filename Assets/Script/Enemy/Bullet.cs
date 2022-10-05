using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public int damage = 3; // Danni che effettua il nemico
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            damage = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            damage = 4;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().health -= damage;
        }

        if ((collider.gameObject.name != "Gun") && (collider.gameObject.tag != "Enemy"))
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, 10f);
    }
}
