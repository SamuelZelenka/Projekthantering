using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public GameObject usernameObject;
    public GameObject passwordObject;
    public Text infotext;
    private string username;
    private string password;
    private string[] lines;


    public void LoginButton()
    {
        //bool for username and password
        bool UN = false;
        bool PW = false;
        //path for where the users are saved
        string path = (Application.dataPath + $"/Data/Users/{username}.txt");
        if (username != "")
        {
            if (File.Exists(path))
            {
                UN = true;
                lines = File.ReadAllLines(path);
            }
            else
            {   //writes out if the username is invalid
                infotext.gameObject.SetActive(true);
                infotext.text = "Username Invalid";
            }
        }
        else
        {   //writes out if the username field is empty
            infotext.gameObject.SetActive(true);
            infotext.text = "Username field Empty";
        }

        if (password != "")
        {
            if (File.Exists(path))
            {   //checks if the password on the second line of the txt file is the same
                if (password == lines[1])
            {
                PW = true;
            }
            else
                {       //writes out if the password was invalid
                    infotext.gameObject.SetActive(true);
                    infotext.text = "Password is Invalid";
            }
            }
        }
        else
        {   //writes out if the password field was empty
            infotext.gameObject.SetActive(true);
            infotext.text = "Password field empty";
        }
        //if the username and the password is correct it changes the scene
        if (UN == true && PW == true)
        {
            usernameObject.GetComponent<InputField>().text = "";
            passwordObject.GetComponent<InputField>().text = "";
            SceneManager.LoadScene("HeroAndCardSelection");
        }
    }

    

    // Update is called once per frame
    void Update()
    {   //if the user wants to tab through the input fields
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (usernameObject.GetComponent<InputField>().isFocused)
            {
                passwordObject.GetComponent<InputField>().Select();
            }
            if (passwordObject.GetComponent<InputField>().isFocused)
            {
                usernameObject.GetComponent<InputField>().Select();
            }

        }
        //if the user wants to push enter to login
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (password != "" && password != "")
            {
                LoginButton();
            }
        }
        username = usernameObject.GetComponent<InputField>().text;
        password = passwordObject.GetComponent<InputField>().text;
    }
}
