using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controlls the states of AI behavior during its turn sequence.
//Written by Tapani Kronkvist

public class AIStateMachine : MonoBehaviour
{
    public bool aiTurn = false; //Ai turn to play.
    bool firstTurn;
    //Enum
    [SerializeField] enum States { init, pickNewCard, cardToHand, playCard, cardToTable, moveToTable, useCards, useSkill, endTurn, wait};
    [SerializeField] enum Behaviour { passive, agressive, defensive}
    [SerializeField] States AI; 
    [SerializeField] Behaviour Strategy;

    //Lists
    public List<GameObject> deck; //Ai deck of cards.
    [SerializeField] List<GameObject> hand;//Cards drawn form deck.
    [SerializeField] List<GameObject> table;

    //Referens objects
    [SerializeField] GameObject activeCard; //Card picked from deck and used for actions.
    [SerializeField] GameObject cloneCard;

    //Variables
    [SerializeField] int cardOrder = 0;//What card been picked in order.
    [SerializeField] int lastCard;//If deck is on last card in cycle.
    
    public float cardSpeed;//How fast card moves on table.
    public int hp = 30;
    public int mana;
    //Positions offsets
    public Transform deckOffset;//Were to spawn cards
    public Transform aIHandOffset;//Were ai hand is located.
    public Transform tableOffset;

    GameObject turnButton;
     

    string[] deckData;

    // Start is called before the first frame update
    void Start()
    {
        AI = States.init; //Starting state for ai.
        Strategy = Behaviour.passive; //Starting behaviour for ai.
        firstTurn = true;
        turnButton = GameObject.Find("TurnButton");

        deckData = txtToString.Convert("AIDeck", "AI");
        for (int i = 0; i < deckData.Length; i++)
        {
            Object loadCard = Resources.Load("Cards/CardPrefab");
            GameObject newCard;
            newCard = Instantiate((GameObject)loadCard, transform.GetChild(0).transform);
            newCard.GetComponent<Card>().cardName = deckData[i];
            deck.Add(newCard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aiTurn)
        {
            switch (AI)
            {
                case States.init:
                    //Initialize and wakes played sleeping cards. takes three cards if it's Ai first turn.
                    Init();
                    break;
                case States.pickNewCard: //Picks a card from deck.
                    PickNewCard();
                    break;
                case States.cardToHand: //Moves card to hand
                    CardToHand();
                    break;
                case States.playCard: //Checks cards and plays possible card 
                    PlayCard();
                    //Invoke("PlayCard", 2); 
                    break;
                case States.cardToTable:
                    CardToTable();
                    break;
                case States.useCards:
                    //print("Ai uses any cards that are voke");
                    UseCards();
                    break;
                case States.moveToTable: //Moves card to table. FLIP CARD NEEDED!
                    MoveToTable();
                    break;
                case States.useSkill: //NOT DONE! Attack sekvens needs to be tied to ´player cards.
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
    void Init() //AI Initializing, visual feedback to player and determines strategy(random, aggresive, defensive", adds mana), takes three card if its first turn.
    {
        mana++;//AI Gets one mana/turn
        Strategy = (Behaviour)Random.Range(0, 3); //Sets ai behavior.
        if (table != null)//Wakes any card that is played in previous sound
        {
            foreach (GameObject item in table)//Search for any sleeping cards and wakes them.
            {
                GameObject sleepingCard;
                sleepingCard = item;
                if (sleepingCard.GetComponent<Card>().sleep == true)
                {
                    sleepingCard.GetComponent<Card>().sleep = false;
                }
            }
        }
        if (firstTurn)//First turn Ai gets three cards to play with
        {
            for (int i = 0; i < deck.Count; i++)
            {
                if (i > 2) //Three cards been played
                {
                    break;
                }
                
                cloneCard = deck[i];
                hand.Add(cloneCard); //Add card to hand
                cloneCard.transform.position = aIHandOffset.transform.position; //Moves to hand !!THIS NEEDS ANIMATION!!
            }
        }
        print("Init done");
        AI = States.pickNewCard; //Next state.
    }
    
    void PickNewCard()//This method needs AI getting hurt if it runs out of cards
    {
        lastCard = deck.Count;//Checks how many cards in deck. 
        if (cardOrder == lastCard)//If AI on last card it starts from the beginning of deck again.This solution until the right cards are in play. REMOVES LATER!
        {
            cardOrder = 0;
        }
        activeCard = deck[cardOrder]; //Draws card next in order.
        cloneCard = Instantiate(activeCard); //Creates a clone of prefab
        hand.Add(cloneCard);//Adds the clone of card to hand list
        cardOrder++; //Increments the card order for next round.
        cloneCard.transform.position = deckOffset.transform.position; //Sets position of card
        AI = States.cardToHand; //Next state
    }
    void CardToHand()
    {
        if (cloneCard.transform.position != aIHandOffset.transform.position)//Checks if card is in hand if not moves it.
        {
            cloneCard.transform.position = Vector3.MoveTowards(cloneCard.transform.position, aIHandOffset.transform.position, cardSpeed * Time.deltaTime);
        }
        else //Movement complete
        {
            activeCard = null; //Resets the card AI is using.
            cloneCard = null; //Erase the cloneCard from memory.
            AI = States.playCard; //Next state
        }
    }
   
    void PlayCard()
    {
        print("Playing card");
        for (int i = 0; i < hand.Count; i++)
        {
            activeCard = hand[i];//Ai start going thru cards
            int manaCost = activeCard.GetComponent<Card>().manaCost; //Checking cost
            if (manaCost <= mana) //Ai can play card.
            {
                table.Add(hand[i]);//Add to list of cards AI is about to play.'
                hand.RemoveAt(0);
                mana -= manaCost; //draws cost.
            }
            if (mana == 0 || i == hand.Count) //Switch state if mana i spent or ai cycled thru all cards. 
            {
                print("Playing to table");
                AI = States.cardToTable;//Switch state.
            }
        }
        //AI = States.useCards;
    }
    void CardToTable()
    {
        if (table.Count != 0)//Checking if list contains cards and deals them.
        {
            
            activeCard = table[0];//Store first card in list.
             //Remove so next item in list gets place [0]
            print("Moving Card to Table");
            AI = States.moveToTable; //Enter state to move stored card to table.
        }
        else  //if ai didn't afford any cards it tries to use a skill. !!Ai should go to use skill after this step but this will be implemented later!!
        {
            //AI = States.useSkill;
            AI = States.useCards;
        }
        

    }
    void MoveToTable() //!!This needs have funktion to set new offsets on card so they don't build ontop of eachother!! 
    {
        //FLIP CARD NEEDED
        if (activeCard.transform.position != tableOffset.transform.position)//Checks if card is in hand if not moves it.
        {
            activeCard.transform.position = Vector3.MoveTowards(activeCard.transform.position, tableOffset.transform.position, cardSpeed * Time.deltaTime);
        }
        else //Movement complete.
        {
            //Clear card from stack.
            AI = States.cardToTable; //Goes back to state to pick next card in stack.
        }
    }

    void UseCards()
    {
        print("Checking if cards are voke and attacking Ai Attacking");
        //Use card method needed!!
        AI = States.endTurn;
    }
    void EndTurn() //Ai is out of options and ends turn.
    {
        //Set cards on the table to voke.
        turnButton.GetComponent<TurnButton>().ResetTurn();
        aiTurn = false;
        AI = States.init;
        //TrunButton call needed
        print(aiTurn);
    }

}
