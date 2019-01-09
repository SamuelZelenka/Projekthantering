using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCreation : MonoBehaviour
{
    [SerializeField] List<CardData> Cards;

    bool editDeck = true;

    RaycastHit hit;

    [SerializeField]public GameObject CardLibrary;
    [SerializeField]public GameObject DeckList;

    // Start is called before the first frame update
    void Start()
    {
        Cards = new List<CardData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeckLimit (int x)
    {
        if(x == 30)
        {
            editDeck = false;
        }
    }

    void SortDeck ()
    {

    }

    void OnMouseDown()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, Mathf.Infinity) == hit.collider.gameObject.CompareTag("Card")) ;
    }
}
