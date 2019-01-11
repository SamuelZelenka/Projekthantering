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
    [SerializeField] enum Behaviour { passive, agressive, defensive}//Strategy states (Not Implemented yet)
    [SerializeField] States AI; 
    [SerializeField] Behaviour Strategy; //Ai behaviour and strategy (Not implemented yet)!

    //Lists
    public List<GameObject> deck; //Ai deck of cards.
    [SerializeField] List<GameObject> hand;//Cards drawn form deck.
    [SerializeField] List<GameObject> table;//Cards AI plays to the table.
    [SerializeField] List<GameObject> pickedCardsToPlay; //Cards AI affords to play during turn.
    public List<GameObject> cardHolderOffset;//Places to play cards on table.

    //Referens objects
    [SerializeField] GameObject activeCard; //Card picked from deck and used for actions.
    [SerializeField] GameObject cloneCard; //Used in the Instantiate process of cards and when moved from deck to hand.
    [SerializeField] GameObject freeSpace; //Free space for ai to play on board.

    //Variables
    [SerializeField] int cardOrder = 0;//What card been picked in order.
    [SerializeField] int lastCard;//If deck is on last card in cycle.
    
    public float cardSpeed;//How fast card moves on table.
    public int hp = 30;
    public int mana;
    [SerializeField] int currentMana = 0;//This is the amount of cards the Ai picks from hand to put on table.
    //Positions offsets
    public Transform deckOffset;//Were to spawn cards
    public Transform aIHandOffset;//Were ai hand is located.
    public Transform tableOffset;//Where Ai places cards.
    

    GameObject turnButton;//To get calls from button switch AI trun,
     
    string[] deckData; //Bring in cards from .txt

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
            newCard = Instantiate((GameObject)loadCard,transform.parent);// transform.GetChild(0).transform
            newCard.transform.position = deckOffset.transform.position;
            newCard.transform.localScale = new Vector3(2, 2, 2); //Places cards in deck holder.
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
                    firstTurn = false;
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
        print(aiTurn);
        mana++;//AI Gets one mana/turn and adds it to pool.
        currentMana = mana;//stores amount of mana AI got.
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
        cloneCard = null;
        AI = States.pickNewCard; //Next state.
    }
    
    void PickNewCard()//This method needs AI getting hurt if it runs out of cards
    {
        if (deck == null) //No cards in ceck and Ai takes Damage
        {
            hp -= 1;
            AI = States.playCard;
        }
        //lastCard = deck.Count;//Checks how many cards in deck. 
        //if (cardOrder == lastCard)//If AI on last card it starts from the beginning of deck again.This solution until the right cards are in play. REMOVES LATER!
        //{
        //    cardOrder = 0;
        //}
        activeCard = deck[0]; //Draws card next in order.
        deck.RemoveAt(0);
        //cloneCard = Instantiate(activeCard); //Creates a clone of prefab
        hand.Add(activeCard);//Adds the clone of card to hand list
        //cardOrder++; //Increments the card order for next round.
        activeCard.transform.position = deckOffset.transform.position; //Sets position of card
        AI = States.cardToHand; //Next state
    }
    void CardToHand()
    {
        if (activeCard.transform.position != aIHandOffset.transform.position)//Checks if card is in hand if not moves it.
        {
            activeCard.transform.position = Vector3.MoveTowards(activeCard.transform.position, aIHandOffset.transform.position, cardSpeed * Time.deltaTime);
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
        if (hand.Count != 0) { //Ai has cards in hand.
            
            for (int i = 0; i < hand.Count; i++)
            {
                activeCard = hand[i];//Ai start going thru cards
                int cardManaCost = activeCard.GetComponent<Card>().manaCost; //Checking cost

                if (cardManaCost <= currentMana) //Ai can play card.
                {
                    pickedCardsToPlay.Add(hand[i]); //Add to list of card to play.
                    hand.RemoveAt(i); //Removes from hand
                    currentMana -= cardManaCost;//Spends mana
                }
            }
            print("Done choosing cards");
            AI = States.cardToTable;
        }
        else//Ai has not picke any cards so it switches to use cards on player.
        {
            print("AI have no cards");
            AI = States.cardToTable; 
        }
        //AI = States.useCards;
    }
    void CardToTable()//Ai moves any card it is about to play to the table.
    {
        if (pickedCardsToPlay.Count != 0)//Checking if ai picked a card and plays it.
        {
            activeCard = pickedCardsToPlay[0];//Picks first card of choice.
            table.Add(activeCard);//Add to table list;
            pickedCardsToPlay.RemoveAt(0);//Removes it from card choice list.
            foreach (GameObject item in cardHolderOffset)
            {
                if (item.GetComponent<CardHolderOffset>().noCard == true)
                {
                    print("Found a free spot");
                    freeSpace = item;
                    AI = States.moveToTable;
                    break;
                }
            }
            /*for (int i = 0; i < cardHolderOffset.Count; i++)//Ai checks table for free spot to play card
            {
                freeSpace = cardHolderOffset[i];
                if (freeSpace.GetComponent<CardHolderOffset>().noCard == true)
                {
                    print("Found a free spot");
                    
                    AI = States.moveToTable; //Moves Specifik card to table.
                    
                }
            }*/

        }
        else  //Done playing any choicen cards to table.
        {
            //AI = States.useSkill;
            AI = States.useCards;
        }

    }
    void MoveToTable() //!!This needs have funktion to set new offsets on card so they don't build ontop of eachother!! 
    {
        //FLIP CARD NEEDED
        activeCard.transform.rotation =  Quaternion.Euler(90, 0, 0);
        if (activeCard.transform.position != freeSpace.transform.position)//Checks if card is in hand if not moves it.
        {
            activeCard.transform.position = Vector3.MoveTowards(activeCard.transform.position, freeSpace.transform.position, cardSpeed * Time.deltaTime);
        }
        else //Movement complete.
        {
            //Clear card from stack.
            activeCard = null;
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
        
    }

}
