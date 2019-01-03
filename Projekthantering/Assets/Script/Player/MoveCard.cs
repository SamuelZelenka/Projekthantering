using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : MonoBehaviour
{
    Camera myCamera;
    RaycastHit hitCard;

    GameObject myPlayer;
    GameObject myGameBoard;

    // Start is called before the first frame update
    void Start()
    {
        myGameBoard = transform.parent.transform.Find($"{myPlayer.name}GameBoard").gameObject;
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(myCamera.ScreenPointToRay(Input.mousePosition), out hitCard, LayerMask.GetMask("Card"));

        if(hitCard.collider != null && hitCard.collider.tag == "PlayerOneCard" && Input.GetAxis("Mouse 1") == 1)
        {
            new Vector3();

            hitCard.collider.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z); 
        }
    }
}
