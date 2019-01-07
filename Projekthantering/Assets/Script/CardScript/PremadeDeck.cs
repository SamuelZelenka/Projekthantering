using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PremadeDeck : MonoBehaviour
{
    //This class contains information of a premade standard deck with cards ment to get a game up and running without making a new.

    [SerializeField] List<string> premade;
    string readLine;
    string textPath;
    StreamReader readData;

    void Awake()
    {
        readData = new StreamReader(textPath);
        while ((readLine = readData.ReadLine()) != null)
        {
            premade.Add(readLine);
        }
        readData.Close();
    }
    // Start is called before the first frame update

    void Start()
    {
        //readData();
    }

}
