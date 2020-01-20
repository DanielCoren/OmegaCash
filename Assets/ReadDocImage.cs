using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReadDocImage : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image MyImage;

    void Start()
    {
//        Debug.Log("1");
        MyImage = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var script = MyImage.GetComponent<UIGradientReg>();
        script.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var script = MyImage.GetComponent<UIGradientReg>();
        script.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MyImage.name.Substring(0, 8) == "DocImage")
        {
            int ButtonNbr = Int32.Parse(MyImage.name.Substring(8));
            DatabaseEngine.CallButtonsCBehiver(ButtonNbr-1);
        }
    }

}
