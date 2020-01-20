using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MeanszHData
{
    private static string SElement;
    private static string SZDate;
    private static float FZTotal;
    private static int IZNumber;

    public class MeanszHObject
    {
        public int MeansNumber { get; set; }
        public int MeansShop { get; set; }
        public string MeansDate { get; set; }
        public float MeansTotal { get; set; }

        public MeanszHObject(int MeansNumber, int MeansShop, string MeansDate, float MeansTotal)
        {
            this.MeansNumber = MeansNumber;
            this.MeansShop = MeansShop;
            this.MeansDate = MeansDate;
            this.MeansTotal = MeansTotal;
        }
    }

}