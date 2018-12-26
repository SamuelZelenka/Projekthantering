using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health { get { return health;} set { health += value;} }
    public int mana { get { return mana; } set { mana += value; } }
    public int armor { get { return armor; } set { armor += value; } }

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
}
