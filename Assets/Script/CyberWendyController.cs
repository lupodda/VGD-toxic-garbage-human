using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyberWendyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Transform enemy;
    private Animator anim;
    public int wendyMaxHealth; // Punti vita massimi di Wendy
    public int wendyHealth; // Punti vita correnti di Wendy
    public HealthBar wendyHealthBar; // Barra della vita di Wendy
    public float enemyCooldown; // Tempo fra un attacco e l'altro
    private float cooldown = 0f;
    public int mantainDistanceFromPlayer = 2;
    public int startMovingWhenDistanceGreaterThan = 3;
    private bool vicino = false;
    public bool isAttacking = false;
    public bool isMoving = false;
    public int tempoCooldown=100;
    public int tipoAttacco = 1;
    public bool deathOK=false;
    int nAttacks = 6;
    int timer=0;
    int tempoAnimazioneMorte=200;
    bool justDead = false;

    public GameObject versoWendy;
    private bool staFacendoIlVerso = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        target = GameObject.Find("Giocatore").transform;
        enemy = gameObject.transform;//Debug.Log(gameObject.transform.position);
        anim = GetComponentInChildren<Animator>();

        wendyMaxHealth = 5000;
        wendyHealth = wendyMaxHealth;
        wendyHealthBar.SetMaxHealth(wendyMaxHealth);
    }


    private void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
        if (timer > 0)
        {
            timer--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wendyHealth > 0)
        {
            agent.SetDestination(target.position);
            //anim.SetBool("isWalking", true);
            //anim.SetBool("isNear", false);

            wendyHealthBar.SetHealth(wendyHealth);



            if (!isAttacking)
            {
                Move();
            }
            else if (isAttacking && !staFacendoIlVerso)
            {
                StartCoroutine(VersoWendy());
            } else
            {
                staFacendoIlVerso = false;
                Destroy(GameObject.FindGameObjectWithTag("VersoWendy"));
            }


            Attack();
        }
        else
        {
            if (justDead == false)
            {
                justDead = true;
                timer = tempoAnimazioneMorte;
            }
            if (timer == 0)
            {
                deathOK = true;
                //Destroy(gameObject);
            }

          

        }
        AnimatorSetter();

    }

    IEnumerator VersoWendy()
    {
        staFacendoIlVerso = true;
        Instantiate(versoWendy);
        yield return new WaitForSeconds(4f);
        staFacendoIlVerso = false;
        Destroy(GameObject.FindGameObjectWithTag("VersoWendy"));
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

    private void Attack()
    {
        if (!isMoving)
        {
            if (cooldown == 0)
            {
                isAttacking = true;
                tipoAttacco = Random.Range(1, nAttacks);  // todo danno diverso per ciascun attacco
                cooldown = tempoCooldown;
            }
            else
            {
                isAttacking = false;
            }
        }

        //transform.LookAt(target); // Punta al giocatore
        // Se non è stato effettuato l'attacco
        //Rigidbody rb = Instantiate(bullet, gun.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

    }



    private void AnimatorSetter()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("tipoAttacco", tipoAttacco);
        anim.SetInteger("timer", timer);

        if (wendyHealth <= 0)
        {
            anim.SetBool("Dead", true);
        }
    }


    private void Move()
    {
        var rotation = Quaternion.LookRotation(target.position - transform.position);


        if (Distanza(target.position, enemy.position) > mantainDistanceFromPlayer && !vicino) //Altrimenti lo insegue mantenendo una certa distanza
        {
            agent.SetDestination(target.position);

            isMoving = true;

        }
        else if (Distanza(target.position, enemy.position) > startMovingWhenDistanceGreaterThan && vicino)
        {
            vicino = false;
            agent.SetDestination(target.position);

            isMoving = true;
        }
        else
        {
            vicino = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            agent.SetDestination(enemy.position);

            isMoving = false;
        }
    }

}
