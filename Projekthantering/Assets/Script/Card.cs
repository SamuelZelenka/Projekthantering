using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour
{   //This is a prefab for all the cards in the game. Includes variables that effects card in play.
    //Written by Tapani Kronvkist

    public string cardName, cardType, cardText;
    public int hp, attack, manaCost;
    Text showHp, showAttack, showCost;
    
    // Start is called before the first frame update
    void Start()
    {
        showHp = GameObject.Find("DisplayHp").GetComponent<Text>();
        showHp.text = ""+hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
