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
    public Sprite frame, portrait;
    Text showHp, showAttack, showCardName, showCardText, showManaCost, showCardType;
    Image showFrame, showPortrait;
    GameObject gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        //health
        hp = gameController.GetComponent<ReadCardData>().GetCardData(cardName).health;
        showHp = GameObject.Find($"{gameObject.name}/Canvas/DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;

        //attack
        attack = gameController.GetComponent<ReadCardData>().GetCardData(cardName).attack;
        showAttack = GameObject.Find($"{gameObject.name}/Canvas/DisplayAttack").GetComponent<Text>();
        showAttack.text = "" + attack;
        
        //name
        showCardName = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardName").GetComponent<Text>();
        showCardName.text = cardName;

        //Description text
        cardText = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardText;
        showCardText = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardText").GetComponent<Text>();
        showCardText.text = cardText;

        //manacost
        manaCost = gameController.GetComponent<ReadCardData>().GetCardData(cardName).manacost;
        showManaCost = GameObject.Find($"{gameObject.name}/Canvas/DisplayManaCost").GetComponent<Text>();
        showManaCost.text = "" + manaCost;

        //cardtype
        cardType = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardType;
        showCardType = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardType").GetComponent<Text>();
        showCardType.text = cardType;

        //card frame
        frame = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardFrame;
        showFrame = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardFrame").GetComponent<Image>();
        showFrame.sprite = frame;

        //card portrait
        portrait = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardPortrait;
        showPortrait = GameObject.Find($"{gameObject.name}/Canvas/DisplayPortrait").GetComponent<Image>();
        showPortrait.sprite = portrait;
    }

    // Update is called once per frame
    void Update()
    {
        showHp = GameObject.Find("DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;
    }
}
