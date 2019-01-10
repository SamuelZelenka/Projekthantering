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
    public GameObject username;
    public GameObject password;
    public GameObject confpassword;
    public Text infotext;
    private string Username;
    private string Password;
    private string ConfPassword;
    private string form;


    public void RegisterButton()
    {
        bool UN = false;
        bool PW = false;
        bool CPW = false;
        string path = (@"users/" + Username + ".txt");
        if (Username != "")
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
        if (Password != "")
        {
            if (Password.Length > 5)
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


        if (ConfPassword != "")
        {
            if (ConfPassword == Password)
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
        if (UN == true && PW ==true && CPW == true)
        {   //saves the user into a text file, with the username on the first line and the password on the second line
            form = (Username + Environment.NewLine + Password);
            File.AppendAllText(path, form);
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confpassword.GetComponent<InputField>().text = "";
            infotext.gameObject.SetActive(true);
            infotext.text = "Registration Complete";
        }     
        
    }
    
    // Update is called once per frame
    void Update()
    {   //tab through all the input fields
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confpassword.GetComponent<InputField>().Select();
            }
            if (confpassword.GetComponent<InputField>().isFocused)
            {
                username.GetComponent<InputField>().Select();
            }
        }//completes the registry with the enter key
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Password !=""&&ConfPassword != "")
            {
                RegisterButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confpassword.GetComponent<InputField>().text;   
    }
}
