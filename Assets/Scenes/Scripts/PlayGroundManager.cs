using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGroundManager : MonoBehaviour
{
    public Dictionary<string, GameObject> CharacterList = new Dictionary<string, GameObject>();
    public List<string> nameList = new List<string>();
    public List<GameObject> characterList = new List<GameObject>();

    public static PlayGroundManager instance;
    [SerializeField] private int characterCount;

    public GameObject player;

    private void Awake()
    {
        instance = this;
        characterCount = nameList.Count;
        for (int i = 0; i < characterCount; i++)
        {
            CharacterList.Add(nameList[i], characterList[i]);
        }
    }

    private void Start()
    {
        GameHasStarted(PlayerData.CharacterName);
    }
    public void GameHasStarted(string playerId)
    {
        player = Instantiate(CharacterList[playerId], new Vector3(0, 0, 0), Quaternion.identity);
        AIManager.playerTransform = player.transform;
        if (player)
        {
            player.AddComponent<PlayerController>();
            player.GetComponent<PlayerController>().animator.SetBool("isGameStart", true);
            PlayerController.instance.cam = GameObject.Find("Camera").GetComponent<Camera>();
            PlayerController.instance.joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        }
    }
}
