using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health;
    public int mana;
    public int armor;

    int maxHealth = 30;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
