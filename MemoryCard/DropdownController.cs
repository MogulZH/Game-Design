using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownController : MonoBehaviour
{
    [SerializeField] TMP_Dropdown drop;
    [SerializeField] TMP_Text txt;
    TMP_Dropdown.OptionData optData1, optData2, optData3, optData4, optData5, optData6;
    List<TMP_Dropdown.OptionData> messages = new List<TMP_Dropdown.OptionData>();
    int msgIndex;
    [SerializeField] SceneController SceneCtrl;


    // Start is called before the first frame update
    void Start()
    {
        SceneCtrl = GameObject.Find("Controller").GetComponent<SceneController>();
        txt = GameObject.Find("Label").GetComponent<TMP_Text>();
        //Fetch the Dropdown GameObject the script is attached to
        drop = GetComponent<TMP_Dropdown>();
        //Clear the old options of the Dropdown menu
        drop.ClearOptions();

        //Create a new option for the Dropdown menu which reads "Option 1" and add to messages List
        optData1 = new TMP_Dropdown.OptionData();
        optData1.text = "2 x 4";
        messages.Add(optData1);

        //Create a new option for the Dropdown menu which reads "Option 2" and add to messages List
        optData2 = new TMP_Dropdown.OptionData();
        optData2.text = "2 x 3";
        messages.Add(optData2);

        optData3 = new TMP_Dropdown.OptionData();
        optData3.text = "2 x 5";
        messages.Add(optData3);

        optData4 = new TMP_Dropdown.OptionData();
        optData4.text = "3 x 4";
        messages.Add(optData4);

        optData5 = new TMP_Dropdown.OptionData();
        optData5.text = "4 x 4";
        messages.Add(optData5);

        optData6 = new TMP_Dropdown.OptionData();
        optData6.text = "4 x 5";
        messages.Add(optData6);



        //Take each entry in the message List
        foreach (TMP_Dropdown.OptionData message in messages)
        {
            //Add each entry to the Dropdown
            drop.options.Add(message);
            //Make the index equal to the total number of entries
            msgIndex = messages.Count - 1;
        }

        drop.onValueChanged.AddListener(delegate { ValueChanged(drop); });
        drop.value = PlayerPrefs.GetInt("dropdown_value", 0);
        drop.RefreshShownValue();
        txt.text = "Option " + drop.value + ": " + drop.options[drop.value].text;
    }
  

    //Ouput the new value of the Dropdown into Text
    public void ValueChanged(TMP_Dropdown change)
    {
        txt.text = "Option " + change.value + ": " + change.options[change.value].text;
        switch (change.value)
        {
            case 0: //2 x 4
                SceneCtrl.SetSize(2, 4);
                PlayerPrefs.SetInt("rows", 2);
                PlayerPrefs.SetInt("columns", 4);
                PlayerPrefs.SetInt("dropdown_value", 0);
            break;
            case 1: //2 x 3
                SceneCtrl.SetSize(2, 3);
                PlayerPrefs.SetInt("rows", 2);
                PlayerPrefs.SetInt("columns", 3);
                PlayerPrefs.SetInt("dropdown_value", 1);
            break;  
            case 2: //2 x 5
                SceneCtrl.SetSize(2, 5);
                PlayerPrefs.SetInt("rows", 2);
                PlayerPrefs.SetInt("columns", 5);
                PlayerPrefs.SetInt("dropdown_value", 2);
            break;
            case 3: //3 x 4
                SceneCtrl.SetSize(3, 4);
                PlayerPrefs.SetInt("rows", 3);
                PlayerPrefs.SetInt("columns", 4);
                PlayerPrefs.SetInt("dropdown_value", 3);
            break;
            case 4: //4 x 4
                SceneCtrl.SetSize(4, 4);
                PlayerPrefs.SetInt("rows", 4);
                PlayerPrefs.SetInt("columns", 4);
                PlayerPrefs.SetInt("dropdown_value", 4);
            break;
            case 5: //4 x 5
                SceneCtrl.SetSize(4, 5);
                PlayerPrefs.SetInt("rows", 4);
                PlayerPrefs.SetInt("columns", 5);
                PlayerPrefs.SetInt("dropdown_value", 5);
            break;          

        }
        PlayerPrefs.Save();
        SceneCtrl.Restart();
    }
}
