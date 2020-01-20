using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLS.Widgets.Table;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using static MeanszHData;

public class MeanszHTable : MonoBehaviour
{

    private Table table;
    private string URL = "http://185.60.170.220:1127/?action=12&user=0504531600";
    private static string SElement;
    private static string SZDate;
    private static float FZTotal;
    private static int IZNumber;
    private List<MeanszHData.MeanszHObject> MeanszHlist; 
    List<MeanszHObject> MeanszHlist1 = new List<MeanszHObject>();
    private UnityWebRequest request1; 
    private XmlReader reader;

    void Start()
    {
        this.table = this.GetComponent<Table>();
        StartCoroutine(GetRecord());
        this.MeanszHlist = MeanszHlist1;
    }
    private void OnTableSelected(Datum datum, Column column)
    {
        string cidx = "N/A";
        if (column != null) cidx = column.idx.ToString();
        print("You Clicked: " + datum.uid + " Column: " + cidx);
    }

    private IEnumerator GetRecord()
    {
        using (request1 = UnityWebRequest.Get(URL))
        {
            yield return request1.SendWebRequest();
            reader = XmlReader.Create(new StringReader(request1.downloadHandler.text));
            StartCoroutine(ParseRecord());
        }
    }
    private IEnumerator ParseRecord()
    { 
        Text txt = GameObject.Find("Daniel").GetComponent<Text>();
        txt.text = "1" + reader.Name;
        reader.Read();
        yield return reader.Name;

        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    SElement = reader.Name;
                    txt.text = reader.Name;
                    break;
                case XmlNodeType.Text:
                    if (SElement == "Zdate") SZDate = reader.Value.Substring(0, 16);
                    if (SElement == "Number") IZNumber = reader.ReadContentAsInt();

                    if (SElement == "Total")
                    {
                        txt.text = txt.text + " 1" + reader.Value.ToString();
                        FZTotal = reader.ReadContentAsFloat();
                        MeanszHlist1.Add(new MeanszHData.MeanszHObject(IZNumber, 0, SZDate, FZTotal));
                    }
                    break;
                case XmlNodeType.EndElement:
                    break;
            }

        }
        reader.Close();

        float PopTotal = 0;
        for (int i = 0; i < this.MeanszHlist.Count; i++)
        {
            PopTotal += this.MeanszHlist[i].MeansTotal;
        }

        this.table.ResetTable();

        Column c;

        c = this.table.AddTextColumn("כ''הס");
        c.horAlignment = Column.HorAlignment.CENTER;
        c.headerFontSize = 20;
        c = this.table.AddTextColumn("ךיראת");
        c.horAlignment = Column.HorAlignment.CENTER;
        c.headerFontSize = 20;
        c = this.table.AddTextColumn("דז רפסמ");
        c.horAlignment = Column.HorAlignment.RIGHT;
        c.headerFontSize = 20;

        // Initialize Your Table
        this.table.Initialize(this.OnTableSelected);

        // Populate Your Rows (obviously this would be real data here)
        for (int i = 0; i < MeanszHlist.Count; i++)
        {
            MeanszHData.MeanszHObject p = MeanszHlist[i];

            Datum d = Datum.Body(i.ToString());
            d.elements.Add(p.MeansTotal.ToString("#,##0.00"));
            d.elements.Add(p.MeansDate);
            d.elements.Add(p.MeansNumber.ToString());
            this.table.data.Add(d);
        }

        // Draw Your Table
        this.table.StartRenderEngine();


    }
}

    
