using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLS.Widgets.Table;
using UnityEngine.UI;

public class Doc_ItemTableImageScrpt : MonoBehaviour
{
    private Table table1;
    private int fontSize;

    void Start()
    {
        
    }

    void Update()
    {
        if (General.Doc_ItemTableImageIndStart == 1)
        {
            General.Doc_ItemTableImageIndStart = 0;
            MakeDefaults.Set();
            table1 = this.GetComponent<Table>();
/*            #if UNITY_EDITOR
                    this.table1.defaultFontSize = 20;
            #elif UNITY_IOS
                        this.table1.defaultFontSize = 30;
            #elif UNITY_ANDROID
                        this.table1.defaultFontSize = 30;
            #else
                        this.table1.defaultFontSize = 20;
            #endif
*/
            table1.ResetTable();
            fontSize = 11;
          Column c1;
            c1 = this.table1.AddTextColumn("         כ''הס ",null,70,70);
            c1.horAlignment = Column.HorAlignment.RIGHT;
            c1.headerFontSize = fontSize;
//            c1 = this.table1.AddTextColumn("החנה%");
//            c1.horAlignment = Column.HorAlignment.CENTER;
//            c1.headerFontSize = fontSize-2;
            c1 = this.table1.AddTextColumn("       ריחמ", null, 50, 50);
            c1.horAlignment = Column.HorAlignment.RIGHT;
            c1.headerFontSize = fontSize;
            c1 = this.table1.AddTextColumn("      תומכ", null, 50, 65);
            c1.horAlignment = Column.HorAlignment.RIGHT;
            c1.headerFontSize = fontSize;
            c1 = this.table1.AddTextColumn("טירפ רואת", null, 151, 151);
            c1.horAlignment = Column.HorAlignment.RIGHT;
            c1.headerFontSize = fontSize;


            table1.Initialize(this.OnTableSelected);
//            Text txt = GameObject.Find("DebugText").GetComponent<Text>();
//            txt.text = General.DocItemTable.Count.ToString();
            // Populate Your Rows (obviously this would be real data here)
            for (int i = 0; i < General.DocItemTable.Count; i++)
          {
              Doc_ItemTable p1 = General.DocItemTable[i];
              Datum d1 = Datum.Body(i.ToString());
              d1.elements.Add(p1.IT_TOTAL.ToString("#########0.00 "));
//              d1.elements.Add(p1.IT_DISC.ToString("###.###"));
              d1.elements.Add(p1.IT_PRICE.ToString("#######0.00 "));
              d1.elements.Add(p1.IT_QTY.ToString("#######0.000 "));
              d1.elements.Add(p1.IT_DESC+" ");
              this.table1.data.Add(d1);
          }


          table1.StartRenderEngine();

      }


    }

    public void OnTableSelected(Datum datum, Column column)
    {
        string cidx = "N/A";
        if (column != null) cidx = column.idx.ToString();
    }

}



