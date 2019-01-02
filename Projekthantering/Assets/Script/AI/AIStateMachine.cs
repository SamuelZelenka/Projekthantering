using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controlls the states of AI behavior during its turn sequence.
//Written by Tapani Kronkvist

public class AIStateMachine : MonoBehaviour
{
    bool aiTurn = true; //Ai turn to play.
    //Enum
    [SerializeField] enum States { init, pickNewCard, checkCards, playCard, useCards, useSkill, endTurn};
    [SerializeField] enum Behaviour { passive, agressive, defensive}
    [SerializeField] States AI; 
    [SerializeField] Behaviour Strategy;
    //Lists
    public List<GameObject> deck; //Ai deck of cards.
    [SerializeField] List<GameObject> hand;//Cards drawn form deck.
    [SerializeField] List<GameObject> playable;
    [SerializeField] List<GameObject> table;

    //Referens objects
    [SerializeField] GameObject cardDrawn; //Card picked from deck.

    //Variables
    [SerializeField] int cardOrder = 0;//What card been picked in order.
    [SerializeField] int lastCard;//If deck is on last card in cycle.
    [SerializeField] int mana;
    //Positions
    public Transform deckOffset;//Were to spawn cards

    // Start is called before the first frame update
    void Start()
    {
        AI = States.init; //Starting state for ai.
        Strategy = Behaviour.passive; //Starting behaviour for ai.
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiTurn)
        {
            switch (AI)
            {
                case States.init:
                    Init();
                    print("Init done");
                    break;
                case States.pickNewCard:
                    print("AI picking new card");
                    print(Strategy);
                    PickNewCard();
                    break;
                case States.checkCards:
                    //print("AI is checking cards and determined what to do");
                    print("cardDraw");
                    CheckCards();
                    break;
                case States.playCard:
                    //print("AI plays cards from hand to table with choosen strategy");
                    PlayCard();
                    break;
                case States.useCards:
                    print("Ai uses any cards that are voke");
                    UseCards();
                    break;
                case States.useSkill:
                    print("Ai uses skill");
                    //UseSkill();
                    break;
                case States.endTurn:
                    print("AI ends its turn");
                    EndTurn();
                    break;
            }
        }
    }
    void Init() 
    {
        //print("AI Initializing, visual feedback to player and determines strategy(random, aggresive, defensive", adds mana);
        mana++;//Gets one mana/turn
        Strategy = (Behaviour)Random.Range(0, 3); //Sets ai behavior.
        if (table != null)//Wakes any card that is played in previous sound
        {
            foreach (var item in table)
            {

            }
        }
        AI = States.pickNewCard; //Next state.
    }
    
    void PickNewCard()
    {
        lastCard = deck.Count;//Checks how many cards in deck.
        if (cardOrder == lastCard)//If AI on last card it starts from the beginning of deck again.
        {
            cardOrder = 0;
        }
        cardDrawn = deck[cardOrder]; //Draws card next in order.
        hand.Add(cardDrawn); //Adds card to hand
        GameObject cloneCard;
        cloneCard = Instantiate(cardDrawn);
        cloneCard.transform.position = deckOffset.transform.position;
        //cardDrawn = null; //Reset
        
        AI = States.checkCards; //Next state
    }
    void CheckCards()
    {
        cardOrder++;//Increments to pick next card in deck
        
        foreach (var card in hand)
        {
            GameObject checkCard;//Store current card
            checkCard = card;
            int checkManaCost;//Store mana cost of current card.
            checkManaCost = checkCard.GetComponent<AICard>().manaCost;
            print(checkManaCost);
            if (mana <= checkManaCost)//Checks if Ai has enough mana to play this card.
            {
                playable.Add(checkCard);
            }
        }
        AI = States.playCard;
    }
    void PlayCard()
    {
        if (mana <= 0)//out of mana and Ai ends turn.
        {
            AI = States.endTurn;
        }
        //AI plays a card without Advanced algorithm (This will change in next scrum)
        for (int i = 0; i < playable.Count; i++)
        {
            if (mana > 0)
            {
                GameObject pickFromHand = playable[i];//Saves the choice from list.
                table.Add(pickFromHand);
                pickFromHand.GetComponent<AICard>().played = true;
                mana -= pickFromHand.GetComponent<AICard>().manaCost;//AI spends mana to play card
                playable[i] = null;
            }
        }
        AI = States.useCards;
    }
    void UseCards()
    {
        print(table);
    }
    void EndTurn() //Ai is out of options and ends turn.
    {
        //Set cards on the table to voke.
        aiTurn = false;
    }
}
