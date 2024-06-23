using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button sellButon;
    [SerializeField] private TextMeshProUGUI count;

    [SerializeField] private TextMeshProUGUI priceText;

    public Image Icon { get { return icon; } }
    public TextMeshProUGUI Count { get {  return count; } }
    public TextMeshProUGUI PriceText { get { return priceText; } }
    public Button SellButton { get { return sellButon; } }
}
