using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReadMeButton : MonoBehaviour , ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{

    private Text txt; 
    public Button MyButton;


    // When highlighted with mouse.

    public void Start()
    {
        MyButton = GetComponent<Button>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        var script = MyButton.GetComponent<UIGradientReg>();
        script.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var script = MyButton.GetComponent<UIGradientReg>();
        script.enabled = true;
    }
    public void OnSelect(BaseEventData eventData)
    {
    }
}
