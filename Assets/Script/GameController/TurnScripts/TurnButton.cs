using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{

    float maxTurnTime;
    [SerializeField] float turnTimer;
    bool myTurn;
    Animation rotateAnim;
    GameObject myPlayer;

    Animation uiTurnSplashAnim;
    Text uiTimer;
    GameObject gameController;
    

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");
        myPlayer = transform.parent.gameObject;
        maxTurnTime = 30;
        turnTimer = maxTurnTime;
        myTurn = true;
        rotateAnim = GetComponent<Animation>();
        uiTimer = GameObject.Find("UITimer").GetComponent<Text>();
        uiTurnSplashAnim = GameObject.Find("YourTurnSplash").GetComponent<Animation>();
        uiTurnSplashAnim.clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animation/Splash/YourTurnSplash.anim");


    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
        if (turnTimer <= 0)
        {
            if(myTurn)
            {
                EndTurn(); //if my turn and timer is 0, force end turn.
            }
            else
            {
                turnTimer = 0; //if not my turn and timer is <= 0. keep timer at 0
            }
        }
        else
        {
            turnTimer -= Time.deltaTime; // countdown turntimer
        }

        if(Input.GetKeyUp(KeyCode.LeftControl)) //!PLACEHOLDER! for other players turn !PLACEHOLDER!
        {
            ResetTurn();
        }
    }

    private void OnMouseOver()
    {
        if (myTurn)
        {
            if (Input.GetAxis("Mouse 1") == 1) //if button is clicked on
            {
                EndTurn();
            }
        }
    }
    void EndTurn()
    {
        rotateAnim.Play("EndTurn");
        myTurn = false;
        
        gameController.GetComponent<GameController>().newTurn();
    }

    void ResetTurn()
    {
        rotateAnim.Play("ResetButton");
        turnTimer = maxTurnTime;
        myTurn = true;
        uiTurnSplashAnim.Play();
        myPlayer.GetComponentInChildren<CardHand>().AddCardFromDeck();

    }
    void UIUpdate()
    {
        uiTimer.text = $"{(int)turnTimer}";
        if(turnTimer < 10)
        {
            uiTimer.color = new Color(255, 0, 0);
        }
        else
        {
            uiTimer.color = new Color(255, 255, 255);
        }
    }

}
