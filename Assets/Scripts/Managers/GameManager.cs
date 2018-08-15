using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public enum GameState
{
    Menu = 0,
    In_Game,
    Cutscene,
    MAX
}

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player testPlayer;

    [SerializeField]
    private Transform startingPos;

    [SerializeField]
    private Trigger endingPos;

    //[SerializeField]
    private Hashtable characters;

    void Start()
    {
        characters = new Hashtable();
    }

    GameState state;

    private void Awake()
    {
        //endingPos.Initialize(triggerHit);
        //testPlayer.transform.position = startingPos.position;
        //To-do Change this to instantiate all players on start so IDs can correctly be set
        Blackboard.gameManager = this;
        DontDestroyOnLoad(this);
    }
    private void endLevel()
    {
        Time.timeScale = 0;
    }

    public void Incombat()
    {
        Camera.Instance.canMove = false;
    }

    public void OutOfCombat()
    {
        Camera.Instance.canMove = true;
    }

    public void save(List<GameObject> playerData)
    {
        foreach(GameObject player in playerData)
        {
            string key = player.gameObject.transform.GetChild(0).GetComponent<Player>().playerName;
            Debug.Log(key);
            PlayerData p = new PlayerData();
            p.name = key;
            p.playerRole = player.gameObject.transform.GetChild(0).GetComponent<Player>().currentRole;
            characters.Add(key, p);
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream gameFile = File.Create(Application.persistentDataPath + "/data.gd");
        bf.Serialize(gameFile, characters);
        gameFile.Close();
        
    }

    public void load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream gameFile = File.Open(Application.persistentDataPath + "/data.txt", FileMode.Open);

        gameFile.Close();
    }
}


