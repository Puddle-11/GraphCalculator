using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatrixCellValues : MonoBehaviour
{
    public TextMeshProUGUI TextElement;
    public void UpdateText(string _text)
    {
        TextElement.text = _text;  
    }
}
