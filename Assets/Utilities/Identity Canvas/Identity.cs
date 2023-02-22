using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Identity : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        if (PlayerData.Name == "")
        {
            this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerData.CharacterName;
        }
        else
        {
            this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerData.Name;
        }
    }

    private void Update()
    {
        transform.LookAt(cam.transform);
    }

}
