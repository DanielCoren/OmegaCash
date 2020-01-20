using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseEngine : MonoBehaviour
{

    public static void GetDatabasePath()
    {
        General.DatabasePath = @"c:\Test\";
        General.FbconnString = "User=SYSDBA;" +
                             "Password=t1995112;" +
                              "Database=" + General.DatabasePath + "Omega.Gdb;" +
                              "DataSource=localhost;" +
                              "Dialect=3;" +
                              "Charset=WIN1255;" +
                              "Port=3050;";
    }

    public static bool IfTableExists(string tableName)
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            General.SqlString = "SELECT RDB$RELATIONS.RDB$RELATION_NAME As Name FROM RDB$RELATIONS WHERE RDB$RELATIONS.RDB$RELATION_NAME = '"+ tableName + "'";
//            Debug.Log(General.SqlString);

            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();

            General.Fbcmd = new FbCommand(General.SqlString, FbConn);
            General.Fbcmd.CommandType = CommandType.Text;
            General.Fbcmd.Connection = FbConn;
            FbDataReader reader = General.Fbcmd.ExecuteReader();

            if (reader.HasRows)
            {
                try
                {
                    reader.Read();
//                    Debug.Log(reader["NAME"].ToString());
                    General.IsOk = true;
                }
                catch (Exception e)
                {
                    General.IsOk = false;
//                    Debug.Log("B");

                }
            }
            else
            {
//                Debug.Log("C");
                return false;

            }
            reader.Close();
            return General.IsOk;
        } 
    }


    public static void GetControlTable(string UserNbr)
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            General.SqlString = "SELECT * FROM Control where Main_key = " + UserNbr;
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();
            General.Fbcmd = new FbCommand(General.SqlString, FbConn);

            General.Fbcmd.CommandType = CommandType.Text;
            FbDataReader reader = General.Fbcmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    General.controlTable.NAME = reader["NAME"].ToString();
                    General.controlTable.LAST_ACCOUNT = int.Parse(reader["LAST_ACCOUNT"].ToString());
                    General.controlTable.SHOP_TYPE = Int16.Parse(reader["SHOP_TYPE"].ToString());
//                    Debug.Log(General.controlTable.SHOP_TYPE.ToString());
                }
            }

        }

    }

    public static void AddToDoc_Item()
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            General.Current_Acc_Item_Line = 1;
            General.SqlString = "select Max(IT_LINE) As IT_LINE from " + General.Table_Item + " Where Number = " + General.NEW_NUMBER.ToString();
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();
            General.Fbcmd = new FbCommand(General.SqlString, FbConn);

            General.Fbcmd.CommandType = CommandType.Text;
            FbDataReader reader = General.Fbcmd.ExecuteReader();
            //            Debug.Log("1");
            if (reader.HasRows)
            {
                reader.Read();
                //                Debug.Log("2");
                if (reader.GetString(0).Length > 0) General.Current_Acc_Item_Line = reader.GetInt32(0) + 1;
            }

            General.Fbcmd.CommandText = "Insert Into " + General.Table_Item + " (Number,IT_LINE,IT_CODE,IT_DESC,IT_QTY,IT_PRICE,IT_DISC,IT_TOTAL,SALEPERSON,CASHIER,IT_CHPRICE,CL_NUM,SALE,NOVAT,BARDISCT,SCALE,ICOM)"
                                        + "Values(" + General.NEW_NUMBER.ToString() + "," + General.Current_Acc_Item_Line.ToString() + ",'" + General.itemsTable.IT_CODE + "','"
                                        + General.itemsTable.IT_DESC + "',1," + General.itemsTable.PRICE1;
            if (General.itemsTable.IT_SALE == "Tr")
            {
                General.Fbcmd.CommandText = General.Fbcmd.CommandText + ","+ General.itemsTable.DISCOUNT+ "," + General.itemsTable.PRICE1 + ",1,1,'" + General.itemsTable.IT_CHPRICE + "',"
                                        + General.doc_HeadTable.CL_NUM + ",Null,'" + General.itemsTable.NOVAT + "','" + General.itemsTable.IF_DISCOUNT.Substring(1) + "','" + General.itemsTable.SCALE + "',Null)";
            }
            else
            {
                General.Fbcmd.CommandText = General.Fbcmd.CommandText + ",0," + General.itemsTable.PRICE1 + ",1,1,'" + General.itemsTable.IT_CHPRICE + "',"
                                        + General.doc_HeadTable.CL_NUM + ",Null,'" + General.itemsTable.NOVAT + "','" + General.itemsTable.IF_DISCOUNT.Substring(1) + "','" + General.itemsTable.SCALE + "',Null)";

            }
        

            string path = "c:/Test/test.sql";

            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(General.Fbcmd.CommandText);
            writer.Close();


//            Text txt = GameObject.Find("DebugText").GetComponent<Text>();
//            txt.text = General.Fbcmd.CommandText;
//            Debug.Log(General.Fbcmd.CommandText);
            General.Fbcmd.Connection = FbConn;
            General.Fbcmd.ExecuteNonQuery();
            GetRecordDoc();

        }




    }

    public static bool GetItemRecord(string It_Code)
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            General.SqlString = "SELECT * FROM Items WHERE It_Code = '" + It_Code + "'";
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();

            General.Fbcmd = new FbCommand(General.SqlString, FbConn);
            General.Fbcmd.CommandType = CommandType.Text;
            General.Fbcmd.Connection = FbConn;
//            Debug.Log("11");
            FbDataReader ItemReader = General.Fbcmd.ExecuteReader();
//            Debug.Log("12");

            if (ItemReader.HasRows)
            {
                try
                {
                   ItemReader.Read();

                   General.itemsTable.IT_CODE = ItemReader.GetString(0);
                   General.itemsTable.IT_DESC = ItemReader.GetString(1);
                   General.itemsTable.BARCODE = ItemReader.GetString(2);
                   General.itemsTable.IT_S_CODE = ItemReader.GetString(3);
                   General.itemsTable.ALTKEY = ItemReader.GetString(4);
                   if (ItemReader.GetString(5).Length > 0) General.itemsTable.RUNKEY = ItemReader.GetFloat(5);
                   if (ItemReader.GetString(6).Length > 0) General.itemsTable.ITSORT = ItemReader.GetInt32(6);
                   if (ItemReader.GetString(7).Length > 0) General.itemsTable.CUT = ItemReader.GetInt32(7);
                   if (ItemReader.GetString(8).Length > 0) General.itemsTable.ROLLER = ItemReader.GetInt32(8);
                   if (ItemReader.GetString(9).Length > 0) General.itemsTable.PRICE1 = ItemReader.GetFloat(9);
                   if (ItemReader.GetString(10).Length > 0) General.itemsTable.PRICE2 = ItemReader.GetFloat(10);
                   if (ItemReader.GetString(11).Length > 0) General.itemsTable.PRICE3 = ItemReader.GetFloat(11);
                   if (ItemReader.GetString(12).Length > 0) General.itemsTable.PRICE4 = ItemReader.GetFloat(12);
                   if (ItemReader.GetString(13).Length > 0) General.itemsTable.PRICE5 = ItemReader.GetFloat(13);
                   if (ItemReader.GetString(14).Length > 0) General.itemsTable.PRICE6 = ItemReader.GetFloat(14);
                   if (ItemReader.GetString(15).Length > 0) General.itemsTable.PRICE7 = ItemReader.GetFloat(15);
                   if (ItemReader.GetString(16).Length > 0) General.itemsTable.PRICE8 = ItemReader.GetFloat(16);
                   if (ItemReader.GetString(17).Length > 0) General.itemsTable.PRICE9 = ItemReader.GetFloat(17);
                   if (ItemReader.GetString(18).Length > 0) General.itemsTable.DISCOUNT = ItemReader.GetFloat(18);
                   if (ItemReader.GetString(19).Length > 0) General.itemsTable.STANDARTCOST = ItemReader.GetFloat(19);
                   if (ItemReader.GetString(20).Length > 0) General.itemsTable.LASTCOST = ItemReader.GetFloat(20);
                   if (ItemReader.GetString(21).Length > 0) General.itemsTable.AVARAGECOST = ItemReader.GetFloat(21);
                   if (ItemReader.GetString(22).Length > 0) General.itemsTable.QTY = ItemReader.GetFloat(22);
                   if (ItemReader.GetString(23).Length > 0) General.itemsTable.MINIQTY = ItemReader.GetFloat(23);
                   if (ItemReader.GetString(24).Length > 0) General.itemsTable.MAXQTY = ItemReader.GetFloat(24);
                   if (ItemReader.GetString(25).Length > 0) General.itemsTable.ENTRYQTY = ItemReader.GetFloat(25);
                   if (ItemReader.GetString(26).Length > 0) General.itemsTable.SOLDQTY = ItemReader.GetFloat(26);
                   if (ItemReader.GetString(27).Length > 0) General.itemsTable.TRNQTY = ItemReader.GetFloat(27);
                   if (ItemReader.GetString(28).Length > 0) General.itemsTable.ORDQTY = ItemReader.GetFloat(28);
                   if (ItemReader.GetString(29).Length > 0) General.itemsTable.DIFQTY = ItemReader.GetFloat(29);
                   if (ItemReader.GetString(30).Length > 0) General.itemsTable.ENTRYDATE = ItemReader.GetDateTime(30);
                   if (ItemReader.GetString(31).Length > 0) General.itemsTable.SOLDDATE = ItemReader.GetDateTime(31);
                   if (ItemReader.GetString(32).Length > 0) General.itemsTable.ORDDATE = ItemReader.GetDateTime(32);
                   if (ItemReader.GetString(33).Length > 0) General.itemsTable.DIFDATE = ItemReader.GetDateTime(33);
                   if (ItemReader.GetString(34).Length > 0) General.itemsTable.UPDDATE = ItemReader.GetDateTime(34);
                   if (ItemReader.GetString(35).Length > 0) General.itemsTable.SUPPLIER = ItemReader.GetInt32(35);
                   General.itemsTable.IT_SALE = ItemReader.GetString(36);
                   General.itemsTable.IT_CHPRICE = ItemReader.GetString(37);
                   General.itemsTable.SCALE = ItemReader.GetString(38);
                   General.itemsTable.FATHER = ItemReader.GetString(39);
                   General.itemsTable.OTHERS = ItemReader.GetString(40);
                   General.itemsTable.COIN = ItemReader.GetString(41);
                   if (ItemReader.GetString(42).Length > 0) General.itemsTable.COLOR = ItemReader.GetInt16(42);
                   if (ItemReader.GetString(43).Length > 0) General.itemsTable.MIDA = ItemReader.GetInt16(43);
                   if (ItemReader.GetString(44).Length > 0) General.itemsTable.SMELL = ItemReader.GetInt16(44);
                   General.itemsTable.IT_DESC2 = ItemReader.GetString(45);
                   if (ItemReader.GetString(46).Length > 0) General.itemsTable.QTYPERUNIT = ItemReader.GetFloat(46);
                   General.itemsTable.NOVAT = ItemReader.GetString(47);
                   General.itemsTable.IF_DISCOUNT = ItemReader.GetString(48);
                   if (ItemReader.GetString(49).Length > 0) General.itemsTable.COSTPERC = ItemReader.GetFloat(49);
                   General.itemsTable.SALEREMARK = ItemReader.GetString(50);
                   if (ItemReader.GetString(51).Length > 0) General.itemsTable.PROFITPERC = ItemReader.GetFloat(51);
//                    General.itemsTable.ICOM = ItemReader.GetString(52); 
                   General.IsOk = true;

                }
                catch (Exception e)
                {
                    General.IsOk = false;
                }
            }
            else
            {
                return false;

            }
            ItemReader.Close();
            return General.IsOk;
        }
    }


    public static void CallButtonsCBehiver(int ButtonNbr)
    {
        if (General.DocButtonsCTable[ButtonNbr].BUTTONTYPE == 0)
        {
            General.IsOk = false;
            GetItemRecord(General.DocButtonsCTable[ButtonNbr].IT_CODE);

            if (General.IsOk) { AddToDoc_Item(); }
        }
    }

    public static void InsertButtonsCTable(ButtonsCTable buttonsCTable)
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            FbConn.Open();
            FbCommand Fbcmd = new FbCommand();
            Fbcmd.Connection = FbConn;
            buttonsCTable.BUTTONDESC = buttonsCTable.BUTTONDESC.Replace("'", "''");

            Fbcmd.CommandText = "Insert Into ButtonsC (LEVELKEY,ButtonNbr,ButtonDESC,ButtonTYPE,ButtonColor,It_Code,Pic) Values(" +
            buttonsCTable.LEVELKEY.ToString() + "," + buttonsCTable.BUTTONNBR.ToString() + ",'" + buttonsCTable.BUTTONDESC + "'," +
            buttonsCTable.BUTTONTYPE.ToString() + ",'" + buttonsCTable.BUTTONCOLOR + "','" + buttonsCTable.IT_CODE + "','" +
            buttonsCTable.PIC + "');";
            Fbcmd.Connection = FbConn;
            Fbcmd.ExecuteNonQuery();
        }

    }
    public static void CreateButtonsC()
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            FbConn.Open();
            FbCommand Fbcmd = new FbCommand();
            Fbcmd.CommandType = CommandType.Text;
            Fbcmd.Connection = FbConn;
            Fbcmd.CommandText = "CREATE TABLE ButtonsC (LEVELKEY INTEGER Not Null, ButtonNbr INTEGER Not Null, ButtonDESC VarChar(40),ButtonTYPE SMALLINT,ButtonColor VarChar(40),It_Code Char(15),Pic Blob)";
           
            int result = Fbcmd.ExecuteNonQuery();

            Fbcmd.CommandText = "Create UNIQUE INDEX ButtonsC_MAIN_KEY on ButtonsC (LEVELKEY,ButtonNbr);";
            result = Fbcmd.ExecuteNonQuery();
            Fbcmd.CommandText = "SELECT * FROM Buttons";

            FbDataReader BtnReader = Fbcmd.ExecuteReader();
            if (BtnReader.HasRows)
            {
                ButtonsCTable buttonsCTable = new ButtonsCTable();
                while (BtnReader.Read())
                {
                    buttonsCTable.LEVELKEY = BtnReader.GetInt16(0);
                    int y;
                    y = 1;
                    string FieldName;
                    for (int i = 0; i < BtnReader.FieldCount - 1; i++)
                    {
                        if (i > 0)
                        {
                            FieldName = BtnReader.GetName(i);
                            FieldName = FieldName.Substring(1, 2);
                            y = System.Convert.ToInt32(FieldName);
                            FieldName = BtnReader.GetName(i);
                            FieldName = FieldName.Substring(3, FieldName.Length - 3);
                            if (FieldName == "CODE")
                            {
                                buttonsCTable.BUTTONNBR = y;
                                buttonsCTable.BUTTONDESC = BtnReader.GetString(i - 3);
                                if (BtnReader.GetString(i - 1).Length > 0)
                                    buttonsCTable.BUTTONTYPE = BtnReader.GetInt16(i - 1);
                                buttonsCTable.IT_CODE = BtnReader.GetString(i);
//                                Debug.Log("CREATEC");
                                InsertButtonsCTable(buttonsCTable);
                            }
                        }
                    }
                }
            }

            BtnReader.Close();
            Fbcmd.Dispose();
        }

    }

    public static IEnumerator GetRecordButtons(int LEVELKEY)
    {

        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            General.SqlString = "SELECT * FROM ButtonsC Where LEVELKEY = " + LEVELKEY.ToString();
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();
            General.Fbcmd = new FbCommand(General.SqlString, FbConn);
            General.Fbcmd.CommandType = CommandType.Text;
            //            FbDataReader BtnReader = General.Fbcmd.ExecuteReader();
//            Debug.Log("21");
            General.BtnReader = General.Fbcmd.ExecuteReader();
//            Debug.Log("22");
            string BtnName;
            string FieldNbr;
            //            TMP_Text Txt01;

            if (General.BtnReader.HasRows)
            {
                while (General.BtnReader.Read())
                {
                    ButtonsCTable DocButtons = new ButtonsCTable();
                    DocButtons.BUTTONNBR = General.BtnReader.GetInt16(1);
                    DocButtons.BUTTONDESC = General.BtnReader.GetString(2);
                    DocButtons.BUTTONTYPE = General.BtnReader.GetInt16(3);
                    DocButtons.IT_CODE = General.BtnReader.GetString(5);
                    General.DocButtonsCTable.Add(DocButtons);

                    if (General.BtnReader["BUTTONDESC"].ToString().Length > 0)
                    {
                        FieldNbr = General.BtnReader["BUTTONNBR"].ToString();
                        if (FieldNbr.Length == 1) FieldNbr = "0" + FieldNbr;
                        BtnName = "DocImage" + FieldNbr + "Text";
                        try
                        {
                            TMP_Text Txt01 = GameObject.Find(BtnName).GetComponent<TMP_Text>();
                            Txt01.text = General.BtnReader["BUTTONDESC"].ToString();
                        }
                        catch (Exception e)
                        {
//                            Debug.Log(BtnName + " " + General.BtnReader["BUTTONDESC"].ToString());
//                            Debug.Log(BtnName + " Exception caught." + e);
                        }
                    }
                }
            }

            General.BtnReader.Close();
            General.Doc_ButtonsImageIndStart = 1;
        }
        yield return new WaitForSeconds(1);

    }
    public static void  SumDoc()
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            String SQl;
            if (General.NoDubbleDisc)
                SQl = "sum(case When " + General.Table_Item + ".Sale is not null Then 0 Else Case When BARDISCT = 'T' Then IT_Total Else 0 End End) AS Sum_BarDiscount, sum(Case When Trim (" + General.Table_Item +
                    ".Sale) = 'T' Then 0 Else Case When Scale = 'T' Then 1 Else IT_QTY End End) AS Sum_It_QTY, sum(IT_total_cur) AS Sum_It_total_cur";
            else
                SQl = "sum(case When (" + General.Table_Item + ".Sale > '0' And " + General.Table_Item + ".Sale < '5') Then 0 Else Case When BARDISCT = 'T' Then IT_Total Else 0 End End) AS Sum_BarDiscount, sum(Case When Trim (" + General.Table_Item + ".Sale) = 'T' Then 0 Else Case When Scale = 'T' Then 1 Else IT_QTY End End) AS Sum_It_QTY, sum(IT_total_cur) AS Sum_It_total_cur";

            General.SqlString = "Select sum(IT_Total) AS Sum_It_total, sum(case When NoVat = 'T' Then 0 Else Case When BARDISCT = 'T' Then IT_Total-(IT_Total*" + General.doc_HeadTable.DISCT_P + "/100) Else IT_Total End End) AS Sum_Vat, sum(case When NoVat = 'T' Then IT_Total-(IT_Total*" + General.doc_HeadTable.DISCT_P + "/100) End) AS Sum_NoVat," + SQl + " From " + General.Table_Item + " Where number = " + General.NEW_NUMBER.ToString();
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();
            General.Fbcmd = new FbCommand(General.SqlString, FbConn);
            General.Fbcmd.CommandType = CommandType.Text;
            string path = "c:/Test/test.sql";

            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(General.Fbcmd.CommandText);
            writer.Close();


//            Text txt = GameObject.Find("DebugText").GetComponent<Text>();
//            txt.text = General.Fbcmd.CommandText;


            FbDataReader reader = General.Fbcmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                General.doc_HeadTable.TOTAL_B =  reader.GetFloat(1);
                Text txtDoc_SumTotal = GameObject.Find("Doc_SumTotal").GetComponent<Text>();
                txtDoc_SumTotal.text = General.doc_HeadTable.TOTAL_B.ToString("F");
                txtDoc_SumTotal = GameObject.Find("Doc_SumQty").GetComponent<Text>();
                txtDoc_SumTotal.text = reader["SUM_IT_QTY"].ToString();
            } 
        }
    }


    public static void /*IEnumerator*/ GetRecordDoc()
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            General.SqlString = "SELECT * FROM "+ General.Table_Item + " Where number = " + General.NEW_NUMBER.ToString()+" Order By It_Line";
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();
            General.Fbcmd = new FbCommand(General.SqlString, FbConn);
            General.Fbcmd.CommandType = CommandType.Text;
            FbDataReader reader = General.Fbcmd.ExecuteReader();

            if (reader.HasRows)
            {
                General.DocItemTable.Clear();
                while (reader.Read())
                {
                    Doc_ItemTable DocItemTable1 = new Doc_ItemTable();
                    DocItemTable1.NUMBER = reader.GetFieldValue<int>(0);
                    DocItemTable1.IT_LINE = reader.GetFieldValue<int>(1);
                    DocItemTable1.IT_CODE = reader["IT_CODE"].ToString();
                    DocItemTable1.IT_DESC = MainPanelScrpt.ReverseString(reader["IT_DESC"].ToString());
                    DocItemTable1.IT_QTY = reader.GetFloat(4);
                    //                    DocItemTable1.BONUS    = reader.GetFloat(5);
                    DocItemTable1.IT_PRICE = reader.GetFloat(6);
                    //                    DocItemTable1.IT_PRICE_CUR = reader.GetFloat(7);
                    DocItemTable1.IT_DISC = reader.GetFloat(8);
                    DocItemTable1.IT_TOTAL = reader.GetFloat(9);

                    /*



                                        reader["IT_TOTAL"].ToString(),
                                                            reader["IT_TOTAL_CUR"].ToString(),
                                                            reader["ORDNUMBER"].ToString(),
                                                            reader["ORDLINE"].ToString(),
                                                            reader["COLOR"].ToString(),
                                                            reader["MIDA"].ToString(),
                                                            reader["SALEPERSON"].ToString(),
                                                            reader["CASHIER"].ToString(),
                                                            reader["IT_CHPRICE"].ToString(),
                                                            reader["DLV_DATE"].ToString(),
                                                            reader["CL_NUM"].ToString(),
                                                            reader["SALE"].ToString(),
                                                            reader["NOVAT"].ToString(),
                                                            reader["BARDISCT"].ToString(),
                                                            reader["SCALE"].ToString(),
                                                            reader["ICOM"].ToString()
                                                           )); */
                    General.DocItemTable.Add(DocItemTable1);

                    /*                DocItemTable.Add(new Doc_ItemTable(
                                        reader["NUMBER"].ToString(),
                                        reader["IT_LINE"].ToString(),
                                        reader["IT_CODE"].ToString(),
                                        reader["IT_DESC"].ToString(),
                                        reader["IT_QTY"].ToString(),
                                        reader["BONUS"].ToString(),
                                        reader["IT_PRICE"].ToString(),
                                        reader["IT_PRICE_CUR"].ToString(),
                                        reader["IT_DISC"].ToString(),
                                        reader["IT_TOTAL"].ToString(),
                                        reader["IT_TOTAL_CUR"].ToString(),
                                        reader["ORDNUMBER"].ToString(),
                                        reader["ORDLINE"].ToString(),
                                        reader["COLOR"].ToString(),
                                        reader["MIDA"].ToString(),
                                        reader["SALEPERSON"].ToString(),
                                        reader["CASHIER"].ToString(),
                                        reader["IT_CHPRICE"].ToString(),
                                        reader["DLV_DATE"].ToString(),
                                        reader["CL_NUM"].ToString(),
                                        reader["SALE"].ToString(),
                                        reader["NOVAT"].ToString(),
                                        reader["BARDISCT"].ToString(),
                                        reader["SCALE"].ToString(),
                                        reader["ICOM"].ToString()
                                       )); */
                }
            }

            reader.Close();
            General.Doc_ItemTableImageIndStart = 1;
            SumDoc();
        }
//        yield return new WaitForSeconds(1);

    }

    public static void Doc_New_Number()
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {
            General.SqlString = "Select * from " + General.Table_Head +
                                " Where NUMBER = " + General.NEW_NUMBER.ToString();

            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();
            General.Fbcmd = new FbCommand(General.SqlString, FbConn);
            General.Fbcmd.CommandType = CommandType.Text;
            FbDataReader DocHeaderReader = General.Fbcmd.ExecuteReader();
            if (DocHeaderReader.HasRows)
            {
                DocHeaderReader.Read();
                General.doc_HeadTable.NUMBER = int.Parse(DocHeaderReader["NUMBER"].ToString());
                General.doc_HeadTable.CL_NUM = int.Parse(DocHeaderReader["CL_NUM"].ToString());
                General.doc_HeadTable.CL_NAME = DocHeaderReader["CL_NAME"].ToString();
                General.doc_HeadTable.CL_ADDRESS = DocHeaderReader["CL_ADDRESS"].ToString();
                General.doc_HeadTable.CL_PHONE = DocHeaderReader["CL_PHONE"].ToString();

            }
            else
            {
                //                General.SqlString = "Insert Into " + General.Table_Head +



            }

            switch (General.MainMenuAct)
            {
                case 1:
                    General.DocTopPanelText = General.NEW_NUMBER.ToString() +  " רפסמ תינובשח" ;
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }

    public static void AddRecordDoc_Item()
    {
        using (FbConnection FbConn = new FbConnection(General.FbconnString))
        {

            General.SqlString = "Select IT_Line,IT_CODE from " + General.Table_Item + 
                                "Where IT_CODE = '" + General.itemsTable.IT_CODE + "' AND NUMBER = " + General.NEW_NUMBER.ToString() +
                                "Order by IT_CODE,NUMBER";
            
            if (FbConn.State == System.Data.ConnectionState.Closed) FbConn.Open();
            General.Fbcmd = new FbCommand(General.SqlString, FbConn);
            General.Fbcmd.CommandType = CommandType.Text;
            FbDataReader DocItemReader = General.Fbcmd.ExecuteReader();
            if (DocItemReader.HasRows)
            {
                DocItemReader.Read();

            }
        }
    }
}