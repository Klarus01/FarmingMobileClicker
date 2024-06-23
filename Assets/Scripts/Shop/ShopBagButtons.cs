using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBagButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] uiToClose;
    [SerializeField] private GameObject[] uiToOpen;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(UiToClose);
        button.onClick.AddListener(UiToOpen);
    }

    private void UiToClose()
    {
        foreach (var ui in uiToClose)
        {
            ui.gameObject.SetActive(false);
        }
    }

    private void UiToOpen()
    {
        foreach (var ui in uiToOpen)
        {
            Debug.Log(ui);

            ui.gameObject.SetActive(true);
        }
    }
}
