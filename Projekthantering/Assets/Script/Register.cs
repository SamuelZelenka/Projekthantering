using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;

public class Register : MonoBehaviour
{
    public GameObject usernameObject;
    public GameObject passwordObject;
    public GameObject confpasswordObject;
    public Text infotext;
    private string username;
    private string password;
    private string confPassword;
    private string form;


    public void RegisterButton()
    {
        bool UN = false;
        bool PW = false;
        bool CPW = false;
        string path = (Application.dataPath + $"/Data/Users/{username}.txt");
        if (username != "")
        {

            if (!File.Exists(path))
            {
                UN = true;
            }
            else
            {
                infotext.gameObject.SetActive(true);
                infotext.text = "Username Taken";
            }
        }
        else
        {
            infotext.gameObject.SetActive(true);
            infotext.text = "Username Empty";
        }
        if (password != "")
        {
            if (password.Length > 5)
            {
                PW = true;
            }
            else
            {
                infotext.gameObject.SetActive(true);
                infotext.text = "Password must be atleast 6 characters long";
            }
        }
        else
        {
            infotext.gameObject.SetActive(true);
            infotext.text = "Password field empty";
        }


        if (confPassword != "")
        {
            if (confPassword == password)
            {
                CPW = true;
            }
            else
            {
                infotext.gameObject.SetActive(true);
                infotext.text = "Passwords does not match";
            }
        }

        else
        {
            infotext.gameObject.SetActive(true);
            infotext.text = "Confirm Password field is empty";
        }
        if (UN == true && PW == true && CPW == true)
        {   //saves the user into a text file, with the username on the first line and the password on the second line
            form = (username + Environment.NewLine + password);

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(usernameObject.GetComponent<InputField>().text);
                usernameObject.GetComponent<InputField>().text = "";
                writer.WriteLine(passwordObject.GetComponent<InputField>().text);
                passwordObject.GetComponent<InputField>().text = "";
                confpasswordObject.GetComponent<InputField>().text = "";
                infotext.gameObject.SetActive(true);
                infotext.text = "Registration Complete";
                writer.Close();
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {   //tab through all the input fields
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (usernameObject.GetComponent<InputField>().isFocused)
            {
                passwordObject.GetComponent<InputField>().Select();
            }
            if (passwordObject.GetComponent<InputField>().isFocused)
            {
                confpasswordObject.GetComponent<InputField>().Select();
            }
            if (confpasswordObject.GetComponent<InputField>().isFocused)
            {
                usernameObject.GetComponent<InputField>().Select();
            }
        }//completes the registry with the enter key
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (password != "" && password !=""&&confPassword != "")
            {
                RegisterButton();
            }
        }
        username = usernameObject.GetComponent<InputField>().text;
        password = passwordObject.GetComponent<InputField>().text;
        confPassword = confpasswordObject.GetComponent<InputField>().text;   
    }
}
