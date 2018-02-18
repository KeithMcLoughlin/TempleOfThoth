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
        foreach(Transform child in GenderOptions.transform)
        {
            if(child.GetComponent<Toggle>().isOn)
            {
                userGender = child.Find("Label").GetComponent<Text>().text;
                break;
            }
        }

        string userAgeRange = AgeText.GetComponent<Text>().text;
        string userNationality = NationalityText.GetComponent<Text>().text;

        var userDetails = new UserDetails(userGender, userAgeRange, userNationality);

        //commented out temporarily to prevent inserting to db
        //PlayerData.Instance.InsertNewUserToDatabase(userDetails);
        SceneManager.LoadScene("TestLevel");
    }
}
