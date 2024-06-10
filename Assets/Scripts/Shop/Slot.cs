using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image icon;

    [SerializeField] private TextMeshProUGUI count;

    public Image Icon { get { return icon; } }
    public TextMeshProUGUI Count { get {  return count; } }
}
