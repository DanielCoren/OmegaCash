using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel1;

    public Button AButton;
    public bool IsOpen;
    public bool IsOpen1;
    public void Start()
    {
//       Text txt = GameObject.Find("Text").GetComponent<Text>();
//       txt.text = "a ";

        var script = GetComponent<PanelOpener>();
        if (script.enabled) IsOpen = true;
        AButton = GetComponent<Button>();
        
        AButton.onClick.AddListener(OnButtonClick);

    }

    public void OnButtonClick()
    {
        if (AButton.name == "BtnAcc")
        {
            General.MainMenuAct = 1;
            General.Table_Head = "Tmp_Head";
            General.Table_Item = "Tmp_Item";
            General.Table_Rem = "Tmp_Rem";
            General.NEW_NUMBER = General.controlTable.LAST_ACCOUNT + 1;

            Panel.SetActive(true);
            Panel1.SetActive(false);
        }
        else
        {
            Panel.SetActive(true);
            Panel1.SetActive(false);
        }

    }

    public void OpenPanel()
    {
        if (IsOpen)
        {
            if (Panel != null)
            {
                Panel.SetActive(true);
            }
        }

    }
    public void OpenPanel1()
    {

        if (IsOpen1)
        {
            if (Panel1 != null)
            {
                Panel1.SetActive(true);
            }
        }
        else
        {
            if (Panel1 != null)
            {
                Panel1.SetActive(false);
            }

        }
    }

}



