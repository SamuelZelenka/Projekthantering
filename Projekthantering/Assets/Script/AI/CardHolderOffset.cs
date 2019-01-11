using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderOffset : MonoBehaviour
{
    public bool noCard;

    // Start is called before the first frame update
    void Start()
    {
        noCard = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        noCard = false;
    }
    private void OnTriggerExit(Collider other)
    {
        noCard = true;
    }
}
