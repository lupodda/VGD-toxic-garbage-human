using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * HealthText serve per gestire la scritta che indica numericamente i punti vita (gli HP)
 */
public class HealthText : MonoBehaviour
{
    public Text healthText;

    public void Start() 
    {
        healthText = GameObject.Find("GameInterface/Healthtext").GetComponent<Text>();
    }

    public void SetText(int health, int maxHealth) 
    {
        healthText.text = health.ToString() + "/" + maxHealth.ToString() + " HP";
    }
}
