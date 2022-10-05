using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator; // Conterrà l'animator
    private float velocityX = 0.0f;// velocitaX
    private float velocityY = 0.0f; // velocitaY
    private PlayerController playerController;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Ricava l'animator
        playerController = GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {


        animator.SetFloat("walkingAsseX", playerController.vertical);
        animator.SetFloat("walkingAsseY", playerController.horizontal);
        animator.SetBool("jump", playerController.isJumping);
        animator.SetBool("run", playerController.run);
        animator.SetBool("fluttuare", playerController.fluttuare);
        animator.SetBool("isGrounded", playerController.isGrounded);
        animator.SetBool("isFalling", playerController.isFalling);
        animator.SetInteger("Falling", playerController.framesFalling);
        animator.SetInteger("TimeGrounded", playerController.framesGrounded);
        animator.SetBool("executingUltimate", playerController.executingUltimate);
        animator.SetBool("morto", playerController.morto);
        animator.SetInteger("timer", playerController.timer);
        animator.SetInteger("timerUltimate", playerController.timerUltimate);
        animator.SetBool("vittoria", playerController.vittoria);



        if (playerController.vertical == 0)   // Animazione camminata avanti/indietro
        {
            if (velocityX > 0)
            {
                velocityX -= 5f * Time.deltaTime;
            }
            if (velocityX < 0)
            {
                velocityX += 5f * Time.deltaTime;
            }

        }
        else
        {
            if (playerController.vertical > 0)
            {
                velocityX += 3f * Time.deltaTime;
            }
            else
            {
                velocityX -= 3f * Time.deltaTime;
            }
        }

        velocityX = Mathf.Clamp(velocityX, -1.0f, 1.0f);




        if (playerController.horizontal == 0)   // Animazione camminata avanti/indietro
        {

            if (velocityY < 0)
            {
                velocityY += 10f * Time.deltaTime;
            }
            if (velocityY > 0)
            {
                velocityY -= 10f * Time.deltaTime;
            }
        }
        else
        {
            if (playerController.horizontal > 0)
            {
                velocityY += 3f * Time.deltaTime;
            }
            else
            {
                velocityY -= 3f * Time.deltaTime;
            }
        }

        velocityY = Mathf.Clamp(velocityY, -1.0f, 1.0f);



        
    }



    
}
