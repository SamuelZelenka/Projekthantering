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
        showHp = GameObject.Find($"{gameObject.name}/Canvas/DisplayHp").GetComponent<Text>();
        showHp.text = "" + gameController.GetComponent<ReadCardData>().GetCardData(cardName).health;
        //attack
        showAttack = GameObject.Find($"{gameObject.name}/Canvas/DisplayAttack").GetComponent<Text>();
        showAttack.text = "" + gameController.GetComponent<ReadCardData>().GetCardData(cardName).attack;
        //name
        showCardName = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardName").GetComponent<Text>();
        showCardName.text = gameController.GetComponent<ReadCardData>().GetCardData(cardName).name;
        //Description text
        showCardText = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardText").GetComponent<Text>();
        showCardText.text = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardText;
        //manacost
        showManaCost = GameObject.Find($"{gameObject.name}/Canvas/DisplayManaCost").GetComponent<Text>();
        showManaCost.text = "" + gameController.GetComponent<ReadCardData>().GetCardData(cardName).manacost;
        //cardtype
        showCardType = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardType").GetComponent<Text>();
        showCardType.text = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardType;
        //card frame
        showFrame = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardFrame").GetComponent<Image>();
        showFrame.sprite = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardFrame;
        //card portrait
        showFrame = GameObject.Find($"{gameObject.name}/Canvas/DisplayPortrait").GetComponent<Image>();
        showFrame.sprite = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardPortrait; ;
    }

    // Update is called once per frame
    void Update()
    {
        showHp = GameObject.Find("DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;
    }
}
