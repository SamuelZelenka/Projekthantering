using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    bool ifPlaced;
    
    //public Transform otherCard;
    private Vector3 mOffset;

    private float mZCoord;

    public List<int> tableSlots = new List<int>();

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();
            
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    void Update()
    {

        GameObject[] cardsOnTable = GameObject.FindGameObjectsWithTag("Card");

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Table" && Input.GetMouseButton(0) == false && ifPlaced == false)
            {
                gameObject.transform.position = new Vector3(0.16f, 2.7f, 4.08f);
                
                //ifPlaced = true;
            }
            /*else if (hit.collider.gameObject.tag == "Table" && ifPlaced == true)
            {
                if (otherCard.transform.position.z < this.gameObject.transform.position.z && Input.GetMouseButton(0) == false)
                {
                    gameObject.transform.position = new Vector3(0.16f, 2.7f, 4.08f + 1.50f);
                }
            }*/
            /*else if (hit.collider.gameObject.tag != "Table" && Input.GetMouseButton(0) == false && ifPlaced == true)
            {
                ifPlaced = false;
            }*/
        }
    }
    
    void cardsOnTable()
    {
        while (ifPlaced == true)
        {
            GameObject[] cardsPlaced = GameObject.FindGameObjectsWithTag("Card");

            foreach (GameObject card in cardsPlaced)
            {
                //Cards available on the table
            }
        }
    }
}
