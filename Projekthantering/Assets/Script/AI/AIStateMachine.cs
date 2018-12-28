using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controlls the states of AI behavior during its turn sequence.
//Written by Tapani Kronkvist

public class AIStateMachine : MonoBehaviour
{
    bool aiTurn = true; //Ai turn to play.
    [SerializeField] enum States { init, pickNewCard, checkCards, playCard, useCards, useSkill, endTurn};
    [SerializeField] enum Behaviour { passive, agressive, defensive}
    [SerializeField] States AI; 
    [SerializeField] Behaviour Strategy;
    public List<GameObject> deck; //Ai deck of cards.
    [SerializeField] GameObject cardDrawn; //Card picked from deck.
    [SerializeField] int cardOrder = 0;//What card been picked in order.

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
            StateMachine();
        }
    }
    void StateMachine()
    {
            switch (AI)
            {
                case States.init:
                    Init();
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
                    print("AI plays cards from hand to table with choosen strategy");
                    //PlayCard();
                    break;
                case States.useCards:
                    print("Ai uses any cards that are voke");
                    //UseCards();
                    break;
                case States.useSkill:
                    print("Ai uses skill");
                    //UseSkill();
                    break;
                case States.endTurn:
                    print("AI ends its turn");
                    //EndTurn();
                    break;
            }

    }
    void Init() 
    {
        //print("AI Initializing, visual feedback to player and determines strategy(random, aggresive, defensive");
        StartCoroutine(AIStartUp());
    }
    IEnumerator AIStartUp()
    {
        yield return new WaitForSeconds(3.0f);
        Strategy = (Behaviour)Random.Range(0, 3);
        AI = States.pickNewCard;
    }
    void PickNewCard()
    {
        int lastCard = deck.Count;//Checks how many cards in deck.
        if (cardOrder == lastCard)//If AI on last card it starts from the beginning of deck again.
        {
            cardOrder = 0;
        }
        else
        {
            cardDrawn = deck[cardOrder];
            
        }
        AI = States.checkCards;
    }
    void CheckCards()
    {
        cardOrder++;
        AI = States.playCard;
    }
}
