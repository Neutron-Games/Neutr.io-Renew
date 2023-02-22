using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameStarted = false;
    public static GameObject player;


    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

    }

    public void StartGame(GameObject player)
    {
        gameStarted = true;
    }
}
