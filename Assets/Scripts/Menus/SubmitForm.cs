using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Data;

public class SubmitForm : MonoBehaviour {

    public GameObject GenderOptions;
    public GameObject AgeText;
    public GameObject NationalityText;

    public void Submit()
    {
        string userGender = "";
        //find which option the radio button for gender is on
        foreach(Transform child in GenderOptions.transform)
        {
            if(child.GetComponent<Toggle>().isOn)
            {
                userGender = child.Find("Label").GetComponent<Text>().text;
                break;
            }
        }

        //get the age range value
        string userAgeRange = AgeText.GetComponent<Text>().text;
        //get the nationality value
        string userNationality = NationalityText.GetComponent<Text>().text;

        var userDetails = new UserDetails(userGender, userAgeRange, userNationality);
        
        PlayerData.Instance.InsertNewUserToDatabase(userDetails);
        //load the game
        SceneManager.LoadScene("TestLevel");
    }
}
