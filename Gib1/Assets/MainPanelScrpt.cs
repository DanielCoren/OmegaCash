using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.Text;
//using RTLTMPro;
using TMPro;


public class MainPanelScrpt : MonoBehaviour
{
    private static string FbconnString, SqlString;
    private static FbConnection FbConn;
    private static string DatabasePath;
    private FbCommand Fbcmd;
    public static string UserNbr;
    
    //    public ControlTable ControlTable1 = new ControlTable();
    public static FbDataReader ComFbReader;
//    public GameObject buttonsPanel;

    void Start()
    {

        DatabasePath = @"c:\Test\";
        FbconnString = "User=SYSDBA;" +
                             "Password=t1995112;" +
                              "Database=" + DatabasePath + "Omega.Gdb;" +
                              "DataSource=localhost;" +
                              "Dialect=3;" +
                              "Charset=WIN1255;" +
                              "Port=3050;";
        TextMeshProUGUI ShopName = GameObject.Find("ShopName").GetComponent<TextMeshProUGUI>();

        using (FbConnection FbConn = new FbConnection(FbconnString))
        {

            UserNbr = "1";
            SqlString = "SELECT *  FROM Control where Main_key = " + UserNbr;
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();

            Fbcmd = new FbCommand(SqlString, FbConn);
            Fbcmd.CommandType = CommandType.Text;
            FbDataReader reader = Fbcmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ShopName.text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(reader[2].ToString()));  
                    ControlTable controlTable = new ControlTable();
                    controlTable.NAME = reader["NAME"].ToString();
                }
            }

        }

        Text txt = GameObject.Find("Daniel").GetComponent<Text>();
        txt.text = "0";

//        GameObject uiObj = Instantiate<GameObject>(buttonsPanel, new Vector3(495, 550, 0), Quaternion.identity);
//        uiObj.transform.SetParent(this.transform);
    }


    void Update()
    {
//        txt.text = "1" + bname;
    }
}
