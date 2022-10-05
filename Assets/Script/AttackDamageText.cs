using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDamageText : MonoBehaviour
{
    public Text adText;

    public void Start()
    {
        adText = GameObject.Find("GameInterface/AttackDamage").GetComponent<Text>();
    }

    public void SetAdText(int damage)
    {
        adText.text = "Attack Damage: " + damage.ToString();
    }
}
