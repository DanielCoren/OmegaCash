using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AddListner : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject MyImage;
    public InputField MyText;

    void Start()
    {

    }

    void Update()
    {

        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var script = MyImage.GetComponent<UIGradientReg>();
        script.enabled = false;
        MyImage.GetComponent<Image>().color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var script = MyImage.GetComponent<UIGradientReg>();
        script.enabled = true;
        MyImage.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MyImage.name == "CalcBtn9") MyText.text = MyText.text + "9"; 
        if (MyImage.name == "CalcBtn8") MyText.text = MyText.text + "8";
        if (MyImage.name == "CalcBtn7") MyText.text = MyText.text + "7";
        if (MyImage.name == "CalcBtn6") MyText.text = MyText.text + "6";
        if (MyImage.name == "CalcBtn5") MyText.text = MyText.text + "5";
        if (MyImage.name == "CalcBtn4") MyText.text = MyText.text + "4";
        if (MyImage.name == "CalcBtn3") MyText.text = MyText.text + "3";
        if (MyImage.name == "CalcBtn2") MyText.text = MyText.text + "2";
        if (MyImage.name == "CalcBtn1") MyText.text = MyText.text + "1";
        if (MyImage.name == "CalcBtn0") MyText.text = MyText.text + "0";
        if (MyImage.name == "CalcBtnDot") MyText.text = MyText.text + ".";
        if (MyImage.name == "CalcBtnStar") MyText.text = MyText.text + "*";
        if (MyImage.name == "CalcBtnMinus") MyText.text = MyText.text + "-";
        if (MyImage.name == "CalcBtnPlus") MyText.text = MyText.text + "+";
        MyImage.GetComponent<Image>().color = Color.grey;
    }
}
