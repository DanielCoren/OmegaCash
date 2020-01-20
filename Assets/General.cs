using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;

public class General
{
    public static FbCommand Fbcmd;

    public static int MainMenuAct;
    public static int Doc_ItemTableImageIndStart;
    public static int Doc_ButtonsImageIndStart;
    public static int NEW_NUMBER;
    public static int Current_Acc_Item_Line;

    public static bool IsOk;
    public static bool NoDubbleDisc;

    public static string FbconnString, SqlString;
    public static string DatabasePath;
    public static string Table_Head;
    public static string Table_Item;
    public static string Table_Rem;
    public static string DocTopPanelText;

    public static List<Doc_ItemTable> DocItemTable = new List<Doc_ItemTable>();
    public static List<ButtonsCTable> DocButtonsCTable = new List<ButtonsCTable>();


    public static ButtonsTable DocButtons = new ButtonsTable();
    public static ControlTable controlTable = new ControlTable();
    public static ItemsTable itemsTable = new ItemsTable();
    public static Doc_HeadTable doc_HeadTable = new Doc_HeadTable();
    public static FbDataReader BtnReader;

}

