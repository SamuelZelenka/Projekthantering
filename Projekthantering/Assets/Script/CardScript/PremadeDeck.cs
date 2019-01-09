using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PremadeDeck : MonoBehaviour
{
    //By Tapani Kronkvist
    //This class contains information of a premade standard deck with cards ment to get a game up and running without making a new.
    //This list is called in game to instantiate card copies from a prefab. 

    [SerializeField] List<string> premade;
    string readLine;
    string textPath;
    StreamReader readData;

    void Awake() //Retrives data from file and adds to list.
    {
        readData = new StreamReader(Application.dataPath + "/Data/DataFiles/PremadeDeckData.txt");
        while ((readLine = readData.ReadLine()) != null)
        {
            premade.Add(readLine);
        }
        readData.Close();
    }
}
