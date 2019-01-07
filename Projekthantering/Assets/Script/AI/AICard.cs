using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class controlled the movement of cards Instantiated by the ai onto the board.

public class AICard : MonoBehaviour
{
    //[SerializeField]bool isDrawn = true; //Ai draws a card from deck and moves it to hand.
    //public bool played = false; //Ai plays card and moves it out on the table.
    public bool sleep = true;//Card has just been played.

    //public Transform AIHandOffset;//Were the card travels after been drawn
    //public Transform TableOffset;

    float speed = 5.0f;
    public int manaCost;

    Renderer rend;
    public Texture faceDown;
    public Texture faceUp;
    

    void Start()
    {
        //rend = GetComponent<Renderer>();
        //played = false;
        
    }
    /*/ Update is called once per frame
    void Update()
    {
        if (isDrawn)//Starts moving to hand from deck
        {
            float step = speed * Time.deltaTime;
            transform.position =  Vector3.MoveTowards(transform.position, AIHandOffset.transform.position, step);
        }
        if (transform.position == AIHandOffset.transform.position)//Stopes movement
        {
            isDrawn = false;
            
        }
        if (played)//Starts moving to Table from hand;
        {
            rend.material.mainTexture = faceUp;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TableOffset.transform.position, step);
        }
        if (transform.position == TableOffset.transform.position)//Stopes movement
        {
            played = false;
        }

    }*/

}
