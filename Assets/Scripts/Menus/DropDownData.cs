using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownData : MonoBehaviour {

    public TextAsset NationalitiesTextFile;

    // Use this for initialization
    void Start ()
    {
        var nationalitiesArray = NationalitiesTextFile.text.Split('\n');
        List<string> nationalities = new List<string>(nationalitiesArray);
        var dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(nationalities);
    }
}
