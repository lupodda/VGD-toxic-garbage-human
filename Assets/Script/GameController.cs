using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Includo il gestore delle scene
using UnityEngine.UI;
using System.IO; // Includo il gestore input-output
using System.Runtime.Serialization.Formatters.Binary; // Includo il binary



public class GameController : MonoBehaviour
{
    
    public int puntiAmbiente = 0; // Variabile che mantiene lo score
    public ScoreText scoreText;
    public Teleport portale;
    private bool portalSpawned = false;

    public GameObject gameOver;

    public Text mostraNomeLivello;
    public Text salvataggioAutomaticoOk;
    
    public AudioSource bonusAttackSound;
    public AudioSource bonusHealSound;
    
    public Text attaccoBonus;
    public Text saluteBonus;

    public GameObject mirino;

    public static float mouseSensitivity = 1.0f; // Sensibilità del mouse, si può anche mettere diversa tra x e y
    public float mouseSensitivityCheck;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1f;
        mirino.SetActive(true);

        StartCoroutine(WaitScrittaSalvataggioAutomatico());

        StartCoroutine(WaitScrittaLivello());
        
        StartCoroutine(WaitScrittaSalvataggioAutomatico());

    }



    // IncreaseScore incrementa lo score di 1 quando viene chiamata
    public void IncreaseScore()
    {
        puntiAmbiente += 20;
    }

    
    
    // Update is called once per frame
    void Update()
    {


        mouseSensitivityCheck = mouseSensitivity;

        if (puntiAmbiente >= 200)
        {
            if (puntiAmbiente % 600 == 0)
            {
                GameObject.Find("Giocatore").GetComponent<PlayerController>().dannoAttacco += 10;
                puntiAmbiente += 20;
                print("Bonus: +20 danno! +20 punti ambiente!"); // Sostituire con scritta nell'interfaccia

                GameObject.Find("Giocatore").GetComponent<PlayerController>().health += 20;
                GameObject.Find("Giocatore").GetComponent<PlayerController>().maxHealth += 20;
                puntiAmbiente += 20;
                print("Bonus: +20 hp! +20 punti ambiente!"); // Sostituire con scritta nell'interfaccia

                StartCoroutine(WaitBetweenBonus());
            }
            else if (puntiAmbiente % 200 == 0)
            {
                GameObject.Find("Giocatore").GetComponent<PlayerController>().dannoAttacco += 10;
                puntiAmbiente += 20;
                print("Bonus: +20 danno! +20 punti ambiente!"); // Sostituire con scritta nell'interfaccia
                StartCoroutine(WaitScrittaAttaccoBonus());
                bonusAttackSound.Play();
            }

            else if (puntiAmbiente % 300 == 0)
            {
                GameObject.Find("Giocatore").GetComponent<PlayerController>().health += 20;
                GameObject.Find("Giocatore").GetComponent<PlayerController>().maxHealth += 20;
                puntiAmbiente += 20;
                print("Bonus: +20 hp! +20 punti ambiente!"); // Sostituire con scritta nell'interfaccia
                StartCoroutine(WaitScrittaSaluteBonus());
                bonusHealSound.Play();
            }

        }
        
        
        scoreText.SetScoreText(puntiAmbiente);

        if(SceneManager.GetActiveScene().buildIndex < 4)
        {
            int puntiSbloccoPortale;

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                puntiSbloccoPortale = GameObject.Find("Spawner").GetComponent<SpawnerLevel3>().numberOfEnemies * 20 * 3 / 4;
            }
            else
            {
                puntiSbloccoPortale = GameObject.Find("Spawner").GetComponent<Spawner>().numberOfEnemies * 20 * 3 / 4;
            }



            if (puntiAmbiente >= puntiSbloccoPortale && !portalSpawned)
            {
                Vector3 portalPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                portalPosition.x += 2;
                portalPosition.y += 1;

                Teleport portal = Instantiate(portale, portalPosition, Quaternion.identity) as Teleport;
                GameObject.Find("PortalText").GetComponent<Text>().text = "Livello " + (SceneManager.GetActiveScene().buildIndex + 1);


                portalSpawned = true;

            }
        }
        



        if(GameObject.Find("Giocatore").GetComponent<PlayerController>().health <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().healthText.SetText(0, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().maxHealth);

            if (GameObject.Find("Giocatore").GetComponent<PlayerController>().gameOverOk)
            {
                Cursor.lockState = CursorLockMode.Confined;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                Time.timeScale = 0f;
                mirino.SetActive(false);
                gameOver.SetActive(true);
            }

        }

    }

    IEnumerator WaitScrittaLivello()
    {

        mostraNomeLivello.text = "Livello " + SceneManager.GetActiveScene().buildIndex;
        mostraNomeLivello.enabled = true;

        yield return new WaitForSeconds(3);
        mostraNomeLivello.enabled = false;
    }
    
    
    IEnumerator WaitScrittaAttaccoBonus()
    {
        attaccoBonus.enabled = true;

        yield return new WaitForSeconds(4);
        attaccoBonus.enabled = false;
    }
    
    
    IEnumerator WaitScrittaSaluteBonus()
    {
        saluteBonus.enabled = true;

        yield return new WaitForSeconds(4);
        saluteBonus.enabled = false;
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/save.dat")) // Controllo se il file da usare esiste nel path specificato
        {
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate); // Creiamo il file vero e proprio; la Open prende come parametro il path del file di salvataggio (trovato in automatico con persistentDataPath concatenato al nome del file) e la modalità in cui esso si vuole aprire
            GameData dataToSave = new GameData(SceneManager.GetActiveScene().name); // Qui verranno contenute le informazioni da salvare
            bf.Serialize(file, dataToSave); // Serializza i dati da salvare, li salva nel file
            file.Close(); // Chiude il file
            Debug.Log("file saved successfully"); // Per verificare che il salvataggio sia andato a buon fine
        }

    }

    IEnumerator WaitScrittaSalvataggioAutomatico()
    {

        SaveGame();
        salvataggioAutomaticoOk.enabled = true;

        yield return new WaitForSeconds(3);
        salvataggioAutomaticoOk.enabled = false;
    }

    /**
    IEnumerator WaitForSeconds(int time)
    {
        yield return new WaitForSeconds(time);
    }
    */

    IEnumerator WaitBetweenBonus()
    {
        StartCoroutine(WaitScrittaAttaccoBonus());
        bonusAttackSound.Play();
        yield return new WaitForSeconds(6);
        StartCoroutine(WaitScrittaSaluteBonus());
        bonusHealSound.Play();
    }
}
