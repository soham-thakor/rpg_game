using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string hoverInfo;
    public TextMeshProUGUI textBox;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Started Hovering");
        textBox.text = hoverInfo;
    }

    public void OnPointerExit(PointerEventData pointerEventData) 
    {
        Debug.Log("Stopped Hovering");
        textBox.text = "";
    }
}
