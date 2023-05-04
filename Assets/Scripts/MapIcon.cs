using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIcon : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = mapStatic.mapData[SceneManager.GetActiveScene().name].iconLocation;
    }
}
