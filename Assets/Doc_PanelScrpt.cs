using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.IO;
using UnityEngine.UI;
using System.Text;
using TMPro;
using RTLTMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;




public class Doc_PanelScrpt : MonoBehaviour
{
    public TextMeshProUGUI DocTopPanelText;
    //    M4uProperty<string> clAddress = new M4uProperty<string>();

    //public string ClAddress { get { return clAddress.Value; } set { clAddress.Value = value; } }
    public GameObject DocBody;
    public GameObject DocHeader;

    void Start()
    {

        
        if (General.controlTable.SHOP_TYPE != 5)
        {
            DocBody.gameObject.transform.localScale += new Vector3(0, -0.1F, 0);
            DocBody.gameObject.transform.localPosition = new Vector3(0, 70, 0);
            DocHeader.SetActive(true);

        }
        else
        {
            GameObject.Find("Doc_Header").SetActive(false);
        }
        DatabaseEngine.GetRecordDoc();
        StartCoroutine(DatabaseEngine.GetRecordButtons(0));
        DatabaseEngine.Doc_New_Number();

        InputField RegInputField = GameObject.Find("InPutClNum").GetComponent<InputField>();
        RegInputField.text = General.doc_HeadTable.CL_NUM.ToString();


        DocTopPanelText.text = General.DocTopPanelText;
        Text AnyText = GameObject.Find("DPhoneNbrText").GetComponent<Text>();
        AnyText.text = General.doc_HeadTable.CL_PHONE;
        TMP_InputField TMPInputField = GameObject.Find("InPutClName").GetComponent<TMP_InputField>();
        TMPInputField.text = General.doc_HeadTable.CL_NAME;

        TMPInputField.MoveTextEnd(true);

        TMPInputField = GameObject.Find("InPutClAddress").GetComponent<TMP_InputField>();
        TMPInputField.text = General.doc_HeadTable.CL_ADDRESS;
        TMPInputField.MoveTextEnd(true);


    }

    void Awake()
    {
//        AppM4u.Instance.Doc_PanelScrpt = this;
//        GetComponent<M4uContextRoot>().Context = AppM4u.Instance;
    }

    // Update is called once per frame
    void Update()
    {



//        Debug.Log(TMPInputField.text);





        //        Text txt = GameObject.Find("DocTopPanelText").GetComponent<Text>();
        //        txt.text = General.DocItemTable.Count.ToString();
        //        Debug.Log(General.DocItemTable.Count.ToString());

    }

}
