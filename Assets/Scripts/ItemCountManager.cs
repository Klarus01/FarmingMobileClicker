using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemCountManager : MonoBehaviour
{
    public Action onChangeCount;

    [SerializeField] private Button buttonX1;
    [SerializeField] private Button buttonX5;
    [SerializeField] private Button buttonX10;
    [SerializeField] private Button buttonXMax;

    public int sellCount = 1;

    private void Awake()
    {
        buttonX1.onClick.AddListener(delegate { SellCount(1); });
        buttonX5.onClick.AddListener(delegate { SellCount(5); });
        buttonX10.onClick.AddListener(delegate { SellCount(10); });
        buttonXMax.onClick.AddListener(delegate { SellCount(99999); });
    }
    private void SellCount(int count)
    {
        this.sellCount = count;
        onChangeCount?.Invoke();
    }

}
