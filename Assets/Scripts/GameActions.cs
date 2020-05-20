using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActions : MonoBehaviour
{
    [SerializeField]
    GameObject grid = null;
    [SerializeField]
    GameObject scroll = null;
    [SerializeField]
    GameObject panelAlgorithms = null;

    public void ShowHide()
    {
        if (grid != null) grid.SetActive(!grid.activeSelf);
        if (scroll != null) scroll.SetActive(!scroll.activeSelf);
        if (panelAlgorithms != null) panelAlgorithms.SetActive(!panelAlgorithms.activeSelf);
    }
}
