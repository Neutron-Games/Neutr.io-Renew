using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetNameList : MonoBehaviour
{
    public TextAsset nameText;
    public static string[] names;
    private void OnValidate()
    {
        names = nameText ? nameText.text.Split(separator: new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
    }
}
