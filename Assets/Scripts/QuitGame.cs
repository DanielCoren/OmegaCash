using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour //,  ISelectHandler,  IPointerEnterHandler
{
    public Button QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        QuitButton.onClick.AddListener(OnPointerClick);
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


//    public void OnPointerClick(PointerEventData eventData)
    public void OnPointerClick()
    {

//        Text txt = GameObject.Find("Daniel").GetComponent<Text>();
//        txt.text = "1";
//        if (eventData.button == PointerEventData.InputButton.Left)
        {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
//            Application.Quit();
        }
    }
    // Update is called once per frame

}
