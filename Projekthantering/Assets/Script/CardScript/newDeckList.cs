using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class newDeckList : MonoBehaviour
{
    string createText;
    [SerializeField]List<string> testList;
    //StreamWriter WriteData;
    //[SerializeField] string textPath;
    //public List<CardData> cardlist;

    // Start is called before the first frame update
    void Start()
    {
        testList = new List<string>(12);
        testList.Add("Men");
        testList.Add("jag");
        testList.Add("fuskar");
        testList.Add("förstås");
        testList.Add("aldrig");
        testList.Add("eller");
        testList.Add("hur!!!");
        testList.Add("Är");
        testList.Add("ni");
        testList.Add("snälla");
        testList.Add("mot");
        testList.Add("popcorn?");

        //cardlist = new List<CardData>();
        //string WriteLine;



        //while ((readLine = readData.ReadLine()) != null)
        //{
        //    ConvertStringToData(readLine);
        // }
        //WriteData = new StreamWriter(Application.dataPath + "/Data/DataFiles/CardData.txt");
        //WriteData.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            for (int i = 0; i < testList.Count; i++)
            {
                
                string Text = testList[i] + Environment.NewLine;
                File.AppendAllText(Application.dataPath + "/Data/DataFiles/SaveTest.txt",Text );
            }
            //String erase = "";
            //File.WriteAllText(Application.dataPath + "/Data/DataFiles/SaveTest.txt", erase);



        }
    }
}
