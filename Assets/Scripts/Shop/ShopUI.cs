using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Button shopButton;

    [SerializeField] private GameObject shop;
    [SerializeField] private Canvas shopCanvas;

    private void Awake()
    {
        shopButton.onClick.AddListener(ShopOpener);
    }

    private void OnDisable()
    {
        shop.SetActive(false);
    }

    private void ShopOpener()
    {
        shop.SetActive(true);
        shopCanvas.gameObject.SetActive(true);
    }
}
