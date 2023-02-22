using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static string Name = "Doğukan Topçu";
    public static string CharacterName = "BlackWizzard";
    public static string MapName;

    public static PlayerData instance;

    private void Awake()
    {
        instance = this;
    }

    public void InstantiatePlayer(GameObject player)
    {
        GameObject newPlayer = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        newPlayer.AddComponent<PlayerController>();
    }


}
