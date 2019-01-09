using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class txtToString
{

    static StreamReader readData;
    static List<string> stringArray = new List<string>();
    // Start is called before the first frame update

    public static string[] Convert(string deck, string profile)
    {
        string readLine;

        readData = new StreamReader(Application.dataPath + $"/Data/DataFiles/Decks/{profile}/{deck}.txt");
        while ((readLine = readData.ReadLine()) != null)
        {
            stringArray.Add(readLine);
        }
        readData.Close();

        return stringArray.ToArray();
    }
}
