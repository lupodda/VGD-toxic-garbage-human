using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Transform enemy;
    private float mantainDistanceFromPlayer;
    private float ignorePlayerDistance;

    public int enemyMaxHealth; // Punti vita massimi del nemico
    public int enemyHealth; // Punti vita correnti del nemico
    public HealthBar enemyHealthBar; // Barra della vita del nemico
    private Animator anim;

    public Transform headTarget;
    public GameObject bullet; // Proiettile
    public GameObject gun; // Pistola
    public float enemyCooldown; // Tempo fra un attacco e l'altro
    private float cooldown = 0f;
    public float attackRange = 10f; // Distanza da cui il nemico inizia a sparare
    public Vector3 direzione;
    public float force = 100f;

    public float minDistanceFromPlayer = 3f;
    public float maxDistanceFromPlayer = 25f;

    public float minIgnorePlayerDist = 30f;
    public float maxIgnorePlayerDist = 40f;

    private int timer=0;


    public ParticleSystem muzzleFlash;

    public AudioSource bulletSound;

    public bool generateHumans;

    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        target = GameObject.Find("Giocatore").transform;
        headTarget = GameObject.Find("HeadTarget").transform;
        //enemy = GameObject.Find("Enemy").transform;
        enemy = gameObject.transform;//Debug.Log(gameObject.transform.position);
        anim = GetComponentInChildren<Animator>();

        mantainDistanceFromPlayer= Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        ignorePlayerDistance = Random.Range(minIgnorePlayerDist, maxIgnorePlayerDist);
        agent.speed = Random.Range(2, 4);



        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            enemyMaxHealth = 100;

        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            enemyMaxHealth = 300;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if(generateHumans)
            {
                enemyMaxHealth = 100;
            } else
            {
                enemyMaxHealth = 300;
            }
        }

        enemyHealth = enemyMaxHealth; // All'inizio il nemico ha il massimo dei punti vita
        enemyHealthBar.SetMaxHealth(enemyMaxHealth); // La barra segna il massimo dei punti vita

    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer--;
        }
    }

    void Update()
    {


        if (enemyHealth < enemyMaxHealth)
        {
            ignorePlayerDistance = 10000;
        }


        if (timer == 0)
        {
            mantainDistanceFromPlayer = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            timer = 250;
        }

        var rotation = Quaternion.LookRotation(target.position - transform.position);
        
        if (Distanza(target.position, enemy.position) > ignorePlayerDistance) //Se il giocatore è abbstanza lontano il nemico lo ignora
        {
            agent.SetDestination(enemy.position);
            anim.SetBool("isWalking", false);
            anim.SetBool("isNear", false);

        }
        else
        {

            if (Distanza(target.position, enemy.position) > mantainDistanceFromPlayer) //Altrimenti lo insegue mantenendo una certa distanza
            {
                agent.SetDestination(target.position);
                anim.SetBool("isWalking", true);
                anim.SetBool("isNear", false);

            }
            else
            {
         
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime );
                agent.SetDestination(enemy.position);
                anim.SetBool("isWalking", false);
                anim.SetBool("isNear", true);

            }
        }

        cooldown -= Time.deltaTime;
        if ((Vector3.Distance(target.position, enemy.position) < attackRange) && (cooldown <= 0))
        {
            Attack(); // Attacco solo se il nemico è nel range
        }
        enemyHealthBar.SetHealth(enemyHealth); // Aggiorna la barra a seconda delle modifiche dei punti vita

    }

    float Distanza(Vector3 a, Vector3 b)
    {
        float x = a.x - b.x;
        float y = a.y - b.y;
        float z = a.z - b.z;

        x = x * x;
        y = y * y;
        z = z * z;
        return Mathf.Sqrt(x + y + z);
    }
    
    /**
     * Attacco con proiettile
     */
    private void Attack()
    {
        enemyCooldown = Random.Range(2f, 4f); // L'attacco può essere effettuato tra i 4 e gli 8 secondi
        cooldown = enemyCooldown;
        //transform.LookAt(target); // Punta al giocatore
        // Se non è stato effettuato l'attacco
        //Rigidbody rb = Instantiate(bullet, gun.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        
        Rigidbody rb = Instantiate(bullet, gun.transform.position, gun.transform.rotation).GetComponent<Rigidbody>();
        bulletSound.Play();
        muzzleFlash.Play();
        direzione = new Vector3(headTarget.transform.position.x + Random.Range(-0.1f, 0.1f), headTarget.transform.position.y + Random.Range(-0.1f, 0.1f), headTarget.transform.position.z + Random.Range(-0.1f, 0.1f)) - gun.transform.position;
        direzione.Normalize();
        rb.AddForce(direzione*force, ForceMode.Impulse);
    }


    

}