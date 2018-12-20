using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour {
    [SerializeField] List<GameObject> myCards = new List<GameObject>();
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    [SerializeField] float rotationZOffset;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rotationZOffset = -100 / (myCards.Count - 1);
        for (int i = 0; i < myCards.Count; i++)
        {
            myCards[i].GetComponent<SpriteRenderer>().sortingOrder = i;
            myCards[i].transform.position = new Vector3(transform.position.x + xOffset * i,transform.position.y, transform.position.z + zOffset * i);
            myCards[i].transform.localRotation = Quaternion.Euler(90 ,0, rotationZOffset * i);
            


        }
        transform.rotation = Quaternion.Euler(0, (rotationZOffset / 2) * (myCards.Count - 1), 0);
    }

    public void AddCardToHand(GameObject newCard)
    {
        newCard.transform.SetParent(transform);
        myCards.Add(newCard);
        
    }
}
