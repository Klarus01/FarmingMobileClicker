using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ItemCountManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown countList;
    public Action onChangeCount;

    public int sellCount = 1;

    private void Awake()
    {
        countList.onValueChanged.AddListener(delegate { ChangedValue(countList.value); });
    }

    private void Update()
    {
        Debug.Log(countList.onValueChanged );
    }

    private void ChangedValue(int value)
    {
        switch(value)
        {
            case 0:
                sellCount = 1;
                break;
            case 1:
                sellCount = 5;
                break;
            case 2:
                sellCount = 10;
                break;
            case 3:
                sellCount = 99999;
                break;
        }

        onChangeCount?.Invoke();
    }
}
