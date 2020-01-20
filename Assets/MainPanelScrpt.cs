using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.Text;
using TMPro;
using System;


public class MainPanelScrpt : MonoBehaviour
{
   
    private static int IndButtons = 0;
    public static string UserNbr;

//    private Vector3 PrefapPosition;
//    private GameObject uiObj;


    //    public ControlTable ControlTable1 = new ControlTable();
    public static FbDataReader ComFbReader;
    public GameObject mainPanel;
    public GameObject buttonsPanel;
    public GameObject docPanel;

    void Start()
    {
        General.Doc_ItemTableImageIndStart = 0;
        DatabaseEngine.GetDatabasePath();
        TextMeshProUGUI ShopName = GameObject.Find("ShopName").GetComponent<TextMeshProUGUI>();
        DatabaseEngine.GetControlTable("1");

        DatabaseEngine.IfTableExists("BUTTONSC");
        if  (!General.IsOk) 
        {
            DatabaseEngine.CreateButtonsC();
        }

        ShopName.text = General.controlTable.NAME;   

        if (IndButtons == 0)
        {
            Button BtnAcc = GameObject.Find("BtnAcc").GetComponent<Button>();
            BtnAcc.transform.SetParent(this.transform);
            Button BtnInv = GameObject.Find("BtnInv").GetComponent<Button>();
            BtnInv.transform.SetParent(this.transform);
            Button BtnSeg = GameObject.Find("BtnSeg").GetComponent<Button>();
            BtnSeg.transform.SetParent(this.transform);
            Button BtnOrd = GameObject.Find("BtnOrd").GetComponent<Button>();
            BtnOrd.transform.SetParent(this.transform);
            Button BtnPac = GameObject.Find("BtnPac").GetComponent<Button>();
            BtnPac.transform.SetParent(this.transform);
            Button BtnTrn = GameObject.Find("BtnTrn").GetComponent<Button>();
            BtnTrn.transform.SetParent(this.transform);
            Button BtnEnt = GameObject.Find("BtnEnt").GetComponent<Button>();
            BtnEnt.transform.SetParent(this.transform);
            Button BtnIvc = GameObject.Find("BtnIvc").GetComponent<Button>();
            BtnIvc.transform.SetParent(this.transform);
            Button BtnLos = GameObject.Find("BtnLos").GetComponent<Button>();
            BtnLos.transform.SetParent(this.transform);
            Button BtnRep = GameObject.Find("BtnRep").GetComponent<Button>();
            BtnRep.transform.SetParent(this.transform);
            Button BtnSys = GameObject.Find("BtnSys").GetComponent<Button>();
            BtnSys.transform.SetParent(this.transform);
            Button BtnEom = GameObject.Find("BtnEom").GetComponent<Button>();
            BtnEom.transform.SetParent(this.transform);
            Button BtnRec = GameObject.Find("BtnRec").GetComponent<Button>();
            BtnRec.transform.SetParent(this.transform);
            Button BtnCrt = GameObject.Find("BtnCrt").GetComponent<Button>();
            BtnCrt.transform.SetParent(this.transform);
            Button BtnEod = GameObject.Find("BtnEod").GetComponent<Button>();
            BtnEod.transform.SetParent(this.transform);
            Button BtnClub = GameObject.Find("BtnClub").GetComponent<Button>();
            BtnClub.transform.SetParent(this.transform);
            Button BtnCou = GameObject.Find("BtnCou").GetComponent<Button>();
            BtnCou.transform.SetParent(this.transform);
            Button BtnSupPay = GameObject.Find("BtnSupPay").GetComponent<Button>();
            BtnSupPay.transform.SetParent(this.transform);
            Button BtnSupOrd = GameObject.Find("BtnSupOrd").GetComponent<Button>();
            BtnSupOrd.transform.SetParent(this.transform);
            Button BtnFix = GameObject.Find("BtnFix").GetComponent<Button>();
            BtnFix.transform.SetParent(this.transform);
            Button BtnCopy = GameObject.Find("BtnCopy").GetComponent<Button>();
            BtnCopy.transform.SetParent(this.transform);
            Button BtnShva = GameObject.Find("BtnShva").GetComponent<Button>();
            BtnShva.transform.SetParent(this.transform);
            Button BtnOpenDay = GameObject.Find("BtnOpenDay").GetComponent<Button>();
            BtnOpenDay.transform.SetParent(this.transform);
            Button BtnX = GameObject.Find("BtnX").GetComponent<Button>();
            BtnX.transform.SetParent(this.transform);
            IndButtons = 1;
        }
    }




    public static string ReverseString(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }


}
