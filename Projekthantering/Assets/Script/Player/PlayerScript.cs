using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int health;
    public int mana;
    public int attack;
    public int armor;

    TextMesh healthText, attackText, armorText;
    GameObject attackIcon, armorIcon;

    public int maxHealth = 30;
    public int maxMana;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        maxMana = 1;
        healthText = GameObject.Find($"{name}/PlayerVisual/Health/HealthText").GetComponent<TextMesh>();
        attackText = GameObject.Find($"{name}/PlayerVisual/Attack/AttackText").GetComponent<TextMesh>();
        armorText = GameObject.Find($"{name}/PlayerVisual/Armor/ArmorText").GetComponent<TextMesh>();

        attackIcon = GameObject.Find($"{name}/PlayerVisual/Attack");
        armorIcon = GameObject.Find($"{name}/PlayerVisual/Armor");

        transform.Find($"{gameObject.name}/deck");
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + health;
        attackText.text = "" + attack;
        armorText.text = "" + armor;

        if (attack <= 0)
        {
            attackIcon.SetActive(false);
        }
        else
        {
            attackIcon.SetActive(true);
        }
        if (armor <= 0)
        {
            armorIcon.SetActive(false);
        }
        else
        {
            armorIcon.SetActive(true);
        }
    }


    public void TakeDamage(int damage)
    {
        //remove armor + health if damage > armor, else remove armor
        if (armor <= damage)
        {
            RemoveHealth(damage - armor);
            RemoveArmor(armor);
        }
        else
        {
            RemoveArmor(damage);
        }
    }



    public void RemoveHealth(int value)
    {
        health -= value;
    }

    public void RemoveMana(int value)
    {
        mana -= value;
    }

    public void RemoveArmor(int value)
    {
        armor -= value;
    }
    public void RemoveAttack(int value)
    {
        attack -= value;
    }
    public void AddHealth(int value)
    {
        health += value;
    }
    public void AddMana(int value)
    {
        mana += value;
    }
    public void AddArmor(int value)
    {
        armor += value;
    }
    public void AddAttack(int value)
    {
        attack += value;
    }

}
