using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public List<GameObject> cardHolders = new List<GameObject>();
    public List<GameObject> cardsOnTable = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!cardHolders.Contains(transform.GetChild(i).gameObject))
            {
                cardHolders.Add(transform.GetChild(i).gameObject);
            }
        }
    }

}
