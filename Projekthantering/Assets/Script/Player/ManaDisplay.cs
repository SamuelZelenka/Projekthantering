using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManaDisplay : MonoBehaviour
{
    [SerializeField] Sprite emptyMana;
    [SerializeField] Sprite fullMana;

    PlayerScript stats;


    [SerializeField] List<GameObject> manacrystals = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Player1").GetComponent<PlayerScript>();
        foreach (Transform child in transform)
        {
            manacrystals.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < manacrystals.Count; i++)
        {
            if(i < stats.maxMana)
            {
                manacrystals[i].SetActive(true);
            }
            else
            {
                manacrystals[i].SetActive(false);
            }
        }


        for (int i = 0; i < stats.maxMana; i++)
        {
            if(i < stats.mana)
            {
                manacrystals[i].GetComponent<SpriteRenderer>().sprite = fullMana;
            }
            else
            {
                manacrystals[i].GetComponent<SpriteRenderer>().sprite = emptyMana;
            }
        }
    }
}
