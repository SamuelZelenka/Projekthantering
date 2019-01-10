using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaTextScript : MonoBehaviour
{

    TextMesh myText;
    PlayerScript stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Player1").GetComponent<PlayerScript>();
        myText = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    { 
        myText.text = $"{stats.mana}/{stats.maxMana}";
    }
}
