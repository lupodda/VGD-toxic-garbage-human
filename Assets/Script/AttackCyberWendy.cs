using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackCyberWendy : MonoBehaviour
{
    public int damage = 10; // Danni che effettua il nemico
    public int bonusDamage = 0; // Danni in più a seconda dell'attacco
    GameObject wendy;
    CyberWendyController wendyController;
    // Start is called before the first frame update
    void Start()
    {
        wendy = GameObject.Find("CyberWendy");
        wendyController = wendy.GetComponent<CyberWendyController>();
        bonusDamage = BonusDamage(wendyController.tipoAttacco);

    }

    // Update is called once per frame
    void Update()
    {
        bonusDamage = BonusDamage(wendyController.tipoAttacco);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().health -= damage + bonusDamage;
        }
    }

    int BonusDamage(int attackType) {
        int bonus = 0;
        switch (attackType)
        {
            case 1:
                {
                    bonus = 5;
                }
                break;
            case 2:
                {
                    bonus = 5;
                }
                break;
            case 3:
                {
                    bonus = 10;
                }
                break;
            case 4:
                {
                    bonus = 15;
                }
                break;
            case 5:
                {
                    bonus = 20;
                }
                break;
            case 6:
                {
                    bonus = 20;
                }
                break;
        }

        return bonus;
    }
}
