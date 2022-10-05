using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public int speed = 2;
    public GameObject eyes; // La camera, perchè è come se fosse gli occhi o la testa
    private float verticalVelocity;
    private float gravity = 0.5f;
    private float jumpForce = 0.3f;

    public float vertical = 0.0f;
    public float horizontal = 0.0f;
    public bool jump = false;
    public bool saltoDaFare = false;
    public bool run = false;
    public bool fluttuare = false;
    public bool staFluttuando = false;
    public bool attacco = false;
    public bool ultimate = false;
    public bool executingUltimate = false;
    public int dannoAttacco = 10;
    public bool isGrounded = true;
    public bool isFalling = false;
    public int framesFalling = 0;
    public int framesGrounded = 0;
    public bool morto = false;
    public int ritardoSalto=0;
    public bool isJumping = false;
    public int accelerazioneDiscesaUltimate=3; //Rispetto alla gravità
    public int tempoRicaricaSalto = 30;
    public int tempoRipresaCaduta = 90;
    public bool gameOverOk = false;  //indica quando far apparire il gameover
    public int tempoMorte=5;  //Tempo dalla morte alla visuale gameover
    public float potenzaFluttuata = 20.0f; //Di quanto viene dimezzatta la gravità per la fluttuata
    public float limiteVelocityFluttuata = 6.0f;
    public int tempoRicaricaUltimate = 500;
    public int timer = 0;
    public int timerUltimate = 0;
    public GameObject spazzatura;
    public bool vittoria = false;
    private Vector3 recoil = Vector3.zero;

    public ParticleSystem muzzleFlash;


    public AttackDamageText attackDmgText;
   
    public Text scrittaHeadshot;

    public int maxHealth = 150; // Punti vita massimi del giocatore
    public int health; // Punti vita correnti del giocatore

    public HealthBar healthBar; // Barra della vita del giocatore
    public HealthText healthText; // Testo che mostra la quantità di punti vita massimi e correnti
    public CooldownBar cooldownBar; // Barra del cooldown della ultimate
    public AudioSource attackSound;


    /* Attributi per la schermata di vittoria */
    public GameObject mirino;
    public Text scrittaVittoria;
    public GameObject riniziaLivello;
    public GameObject mainMenu;
    public GameObject quit;

    public int headshot = 25;


    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        dannoAttacco = 10;
        maxHealth = 150;
        health = maxHealth; // All'inizio il giocatore ha il massimo dei punti vita
        healthBar.SetMaxHealth(maxHealth); // La barra segna il massimo dei punti vita
        healthText.SetText(health, maxHealth); // Testo iniziale in cui massimo punti vita e punti vita correnti coincidono
        cooldownBar.SetMaxCooldown(tempoRicaricaUltimate);
        
    }


    void FixedUpdate()
    {

        if (timer != 0)
        {
            timer--;
        }
        if(timerUltimate != 0)
        {
            timerUltimate--;
            cooldownBar.SetCooldown(timerUltimate, tempoRicaricaUltimate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        attackDmgText.SetAdText(dannoAttacco);


        if (isGrounded==true || fluttuare==true) //In questo modo se salta ma non fluttua non può cambiare la direzione dello spostamento
        {
            vertical = Input.GetAxis("Vertical"); // Asse verticale
            horizontal = Input.GetAxis("Horizontal"); // Asse orizzontale
        }

        isGrounded = controller.isGrounded;

        if(isFalling==true && isGrounded == true) {   //Quando atterra timer in cui non si muove perche si riprende dalla caduta
            timer = tempoRipresaCaduta;
               }


        if (health<=0&&morto==false)
        {
            timer = tempoMorte;
            morto = true;
        }

        if (morto == true && timer == 0)
        {
            gameOverOk = true;
        }



        fallingCheck();  //Controlla se il personaggio sta cadendo
        ultimateCheck(); //Controlla se è stata attivata la ultimate, mettere qui controlli se ha abbastanza abilità per fare la ultimate


        

        if (framesGrounded > tempoRicaricaSalto)   //Salta solo se sta a terra da abbastanza
        {
            jump = Input.GetButtonDown("Jump");
            isJumping = true;
        }
        else { jump = false; }


        if (timer == 0)
        {
            run = Input.GetButton("Run") && vertical > 0 && horizontal == 0;
            fluttuare = Input.GetButton("Fluttuare");
            attacco = Input.GetButtonDown("Fire1");

            if (timerUltimate == 0)
            {
                ultimate = Input.GetKeyDown(KeyCode.U);
            }

        }

        if (PauseMenu.GameIsPaused == true)
        {
            attacco = false;
        }




        if (PauseMenu.GameIsPaused == false && morto == false && !vittoria)
        {
            RotateCharacter();
        }

        MoveCharacter(vertical, horizontal);


        if (health < 0) { health = 0; }
        healthBar.SetHealth(health); // Aggiorna la barra a seconda delle modifiche dei punti vita
        healthText.SetText(health, maxHealth); // Aggiorna il testo a seconda delle modifiche dei punti vita
    }




    private void RotateCharacter()
    {

        float x = Input.GetAxis("Mouse X") * GameController.mouseSensitivity; // da qui si può fare pure il porting tra console
        float y = Input.GetAxis("Mouse Y") * GameController.mouseSensitivity;


       
        // Prendo la rotazione locale del character (bisogna considerarla in base alla gerarchia) e bisogna passare da quaternioni (supportati da unity) a euler angles
        Vector3 oldCharacterRotation = transform.localRotation.eulerAngles; // Mi dà la rotazione espressa in quaternioni espressa in euler angles

        // .eulerAngles da fuori è una variabile ma si comporta da getter e setter

        Vector3 newRotation = new Vector3(0f, x + oldCharacterRotation.y, 0f); // Espesso in euler angles e poi lo trasformo in quaternioni, esprime la nuova rotazione

        if (attacco)
        {
            transform.localRotation = Quaternion.Euler(newRotation + recoil); // Trasformo da euler angles a quaternioni e riassegno
        }
        else
        {
            transform.localRotation = Quaternion.Euler(newRotation ); // Trasformo da euler angles a quaternioni e riassegno
        }
        
        // Rotazione della camera/occhi/testa, ruota intorno all'asse x
        Vector3 oldEyesRotation = eyes.transform.localRotation.eulerAngles;


       
        Vector3 newEyesRotation = new Vector3(-y + oldEyesRotation.x, 0f, 0f); // Il - perchè il verso della rotazione va invertito in questo caso

        if (attacco)
        {
            eyes.transform.localRotation = Quaternion.Euler(newEyesRotation + recoil);
        }
        else
        {
            eyes.transform.localRotation = Quaternion.Euler(newEyesRotation);
        }


        // Euler angles vincolano già la rotazione verticale
    }

    private void MoveCharacter(float vertical, float horizontal)
    {



        if (ultimate)
        {
            verticalVelocity = jumpForce*3f;

        }

        if (executingUltimate && verticalVelocity < 0)
        {
            verticalVelocity -= gravity* accelerazioneDiscesaUltimate* Time.deltaTime;
        }


        if (controller.isGrounded)
        {
            staFluttuando = false;

            if (jump)
            {

                    verticalVelocity = jumpForce;



                return;
            }



            if (run)
            {
                speed = 5;
            }
            else
            {
                speed = 2;
            }

        }
        else
        {


            if (fluttuare)
            {
                if (verticalVelocity < 0)  //Se la velocity è negativa
                {

                    verticalVelocity -= gravity / potenzaFluttuata * Time.deltaTime; //La continua a decrementare con un accelerazione che è solo una frazione della gravità
                    if (verticalVelocity > limiteVelocityFluttuata)
                    {
                        verticalVelocity = limiteVelocityFluttuata; //Senza scendere sotto un certo limite
                    }

                }
                else  //Se la velocity è positiva la decrementa normalmente solo con la forza di gravità
                {
                    verticalVelocity -= gravity * Time.deltaTime;
                }

            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;

            }
        }

        
        RaycastHit hit;

        if (attacco) // Spara solo se clicki con il tasto sx del mouse, Fire1 rappresenta tra gli assi virtuali il tasto sx del mouse
        {
            attackSound.Play();
            if(!GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Aim>().isZoomedFPS)
            {
                muzzleFlash.Play();
            }
            
            
            if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit))
            {
                if (SceneManager.GetActiveScene().buildIndex < 4)
                {
                    if (hit.transform.tag == "Enemy")
                    {

                        int danno = dannoAttacco;

                        if (hit.collider is BoxCollider)
                        {
                            danno += headshot;
                            StartCoroutine(WaitScrittaHeadshot());
                        }


                        hit.collider.GetComponent<Enemy>().enemyHealth -= danno;


                        if (hit.collider.GetComponent<Enemy>().enemyHealth <= 0)
                        {
                            Instantiate(spazzatura, hit.transform.position, Quaternion.identity);//Viene creata la "spazzatura" e viene assegnata la stessa posizione del nemico
                            spazzatura.tag = "Gabarage";
                            spazzatura.GetComponent<SphereCollider>().isTrigger = true;//Viene messo il flag di isTrigger a true

                            Destroy(hit.transform.gameObject); // il nemico muore

                            if (SceneManager.GetActiveScene().buildIndex == 3)
                            {
                                GameObject.Find("Spawner").GetComponent<SpawnerLevel3>().deadEnemies++;
                            }
                            else
                            {
                                GameObject.Find("Spawner").GetComponent<Spawner>().deadEnemies++;
                            }

                        }
                    }
                } else
                {
                    if (hit.transform.name == "CyberWendy")
                    {
                        int danno = dannoAttacco;

                        if (hit.collider is BoxCollider)
                        {
                            danno += headshot;
                            StartCoroutine(WaitScrittaHeadshot());
                        }


                        hit.collider.GetComponent<CyberWendyController>().wendyHealth -= danno;
                        //hit.collider.GetComponent<CyberWendyController>().deathOK 
                        if (hit.collider.GetComponent<CyberWendyController>().wendyHealth <=0)
                        {
                           
                           // Destroy(hit.transform.gameObject); // il nemico muore

                            // attivata animazione morte di Wendy
                            // attivata la schermata di vittoria


                            vittoria = true;
                            scrittaVittoria.enabled = true;
                            
                            StartCoroutine(WaitForAnimazioneVittoria());

                            
                        }
                    }
                }
                
            }
        }

        Vector3 movement = (transform.forward * vertical * Time.deltaTime * speed) + (transform.right * horizontal * Time.deltaTime * speed);

        movement.y = verticalVelocity;



        if (timer == 0)
        {
            controller.Move(movement);
        }

        if (verticalVelocity < 0)
        {
            isJumping = false;
        }


    }


    void fallingCheck()
    {
        if (controller.isGrounded == false)
        {
            framesFalling++;
            framesGrounded = 0;
        }
        else
        {
            framesFalling = 0;
            framesGrounded++;
        }

        if (framesFalling > 30)
        {
            isFalling = true;
        }
        else { isFalling = false; }
    }

    void ultimateCheck()
    {
        if (ultimate)
        {
            executingUltimate = true;
        }

        if (isGrounded&&executingUltimate==true)
        {
            timerUltimate = tempoRicaricaUltimate;
            executingUltimate = false;
            StartCoroutine(eyes.GetComponent<CameraShake>().Shake(.15f, .4f));

            Vector3 p1 = transform.position + GetComponent<CharacterController>().center;

            RaycastHit[] colls = Physics.SphereCastAll(p1, 30.0f, transform.forward);
            foreach (RaycastHit col in colls)
            {

                if (SceneManager.GetActiveScene().buildIndex < 4)
                {
                    if (col.collider.CompareTag("Enemy"))
                    {

                        int damage = 100;

                        col.collider.GetComponent<Enemy>().enemyHealth -= damage;

                        if (col.collider.GetComponent<Enemy>().enemyHealth <= 0)
                        {
                            Instantiate(spazzatura, col.transform.position, Quaternion.identity);//Viene creata la "spazzatura" e viene assegnata la stessa posizione del nemico
                            spazzatura.tag = "Gabarage";
                            spazzatura.GetComponent<SphereCollider>().isTrigger = true;//Viene messo il flag di isTrigger a true

                            Destroy(col.collider.transform.gameObject); // il nemico muore
                            if (SceneManager.GetActiveScene().buildIndex == 3)
                            {
                                GameObject.Find("Spawner").GetComponent<SpawnerLevel3>().deadEnemies++;
                            }
                            else
                            {
                                GameObject.Find("Spawner").GetComponent<Spawner>().deadEnemies++;
                            }

                        }
                    }
                }
                
            }


        }






    }

    IEnumerator WaitForAnimazioneVittoria()
    {
        yield return new WaitForSeconds(3.0f);

        Cursor.lockState = CursorLockMode.Confined;
        mirino.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;

        Time.timeScale = 0f;
        vittoria = true;
        riniziaLivello.SetActive(true);
        mainMenu.SetActive(true);
        quit.SetActive(true);
    }




    IEnumerator WaitScrittaHeadshot()
    {
        scrittaHeadshot.enabled = true;

        yield return new WaitForSeconds(0.5f);
        scrittaHeadshot.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.isTrigger = true;
        if (other.tag == "Gabarage")
        {
            Destroy(other.gameObject);
            GameObject.Find("GameController").GetComponent<GameController>().IncreaseScore();
        }
    }







}
