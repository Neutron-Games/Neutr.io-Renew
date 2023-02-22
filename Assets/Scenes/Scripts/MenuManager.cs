using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private Transform cam;
    private GameObject leftButton;
    private GameObject rightButton;
    private int count = 0;

    private GameObject GameManagerObject;

    GameObject player;

    public TMP_InputField inputField;

    RaycastHit ChosenCharacter;


    GameManager gameManager;
    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().transform;
        leftButton = GameObject.FindGameObjectWithTag("LeftButton");
        rightButton = GameObject.FindGameObjectWithTag("RightButton");

        leftButton.GetComponent<Button>().enabled = false;
        leftButton.GetComponent<Image>().color = Color.gray;

        GameManagerObject = GameObject.Find("Game Manager");
        inputField = GameObject.Find("NameField").GetComponent<TMP_InputField>();
    }

    public void Left()
    {
        cam.position -= new Vector3(10, 0, 0);
        rightButton.GetComponent<Button>().enabled = true;
        count--;
        rightButton.GetComponent<Image>().color = Color.white;
        if (count == 0)
        {
            leftButton.GetComponent<Button>().enabled = false;
            leftButton.GetComponent<Image>().color = Color.gray;
        }
    }

    public void Right()
    {
        cam.position += new Vector3(10, 0, 0);
        leftButton.GetComponent<Button>().enabled = true;
        count++;
        leftButton.GetComponent<Image>().color = Color.white;
        if (count == 5)
        {
            rightButton.GetComponent<Button>().enabled = false;
            rightButton.GetComponent<Image>().color = Color.gray;
        }
    }


    public void StartGame()
    {
        Physics.Raycast(cam.position, transform.forward, out ChosenCharacter, 100f);
        PlayerData.Name = inputField.text;
        PlayerData.CharacterName = ChosenCharacter.collider.gameObject.name;
        Debug.Log(ChosenCharacter.collider.gameObject.name);
        player = GameObject.Find(PlayerData.CharacterName);
        player.gameObject.AddComponent<PlayerController>();
        GameManager.instance.StartGame(player);

        DontDestroyOnLoad(GameManagerObject);
        SceneManager.LoadScene("PlayGround");
    }
}
