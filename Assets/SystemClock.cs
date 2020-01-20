using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemClock : MonoBehaviour
{
    public Text SysteClock;
    void Update()
    {
        SysteClock.text = System.DateTime.Now.ToString();
    }
}
