using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour {
    private GameManager gameManager;
    private GameObject selectionScreen;
    private const int maxPlayerCount = 4;
    private int playerCount = 0;

    public List<GameObject> playerModels;
    public List<GameObject> playerScreen;

    // TBC
    private int playerScreenPosition = -450; //-500
    private float modelPosition = -4.5f;

    private bool[] playerExists = new bool[maxPlayerCount];
    public bool[] playerReady = new bool[maxPlayerCount];


    public void readyPlayer(int playerNumber)
    {
        playerReady[playerNumber] = true;

        for(int i = 0; i < playerCount; i++)
        {
            if (playerExists[i])
            {
                if (!playerReady[i])
                {
                    return;
                }
            }
        }

        Debug.Log("Player number: ");
        // Unrestrict movements & destroy excessive models
        foreach(GameObject playerModel in playerModels)
        {
            List<GameObject> destroyModels = new List<GameObject>();

            Debug.Log(playerModel.transform.childCount);
            for (int j = 0; j < playerModel.transform.childCount; j++)
            {
                if (playerModel.gameObject.transform.GetChild(j).gameObject.activeSelf)
                {
                    Rigidbody r = playerModel.gameObject.transform.GetChild(j).GetComponent<Rigidbody>();
                    r.constraints &= ~RigidbodyConstraints.FreezePositionX;
                    r.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                }
                else
                {
                    destroyModels.Add(playerModel.gameObject.transform.GetChild(j).gameObject);
                }
            }

            foreach(GameObject destroyModel in destroyModels)
            {
                DestroyImmediate(destroyModel);
            }
        }

        // Save character
        //gameManager.save(playerModels);

        // Change scene here
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void unreadyPlayer(int playerNumber)
    {
        playerReady[playerNumber] = false;
    }

    private void createPlayer(int playerNumber)
    {
        // Check if player already exists
        if (playerExists[playerNumber])    return;
        playerExists[playerNumber] = true;
        
        var newModel = Instantiate(Resources.Load<GameObject>("Prefabs/CharSelect/PlayerModels"), new Vector3(modelPosition, -1, -5), Quaternion.identity);

        DontDestroyOnLoad(newModel);
        playerModels.Add(newModel);
        
        // Set Player entID for each model
        for(int i = 0; i < newModel.transform.childCount; i++)
        {
            Player p = newModel.gameObject.transform.GetChild(i).GetComponent<Player>();
            if (p) p.EntID = playerNumber;
        }

        // Load splash
        playerModels[playerCount].GetComponent<CharacterSelection>().SetPlayerSplash(playerCount);

        // Reposition models and screens
        modelPosition += 3;
        playerCount++;
    }

    private void createPlayerBoxes()
    {
        var newPlayerScreen = Instantiate(Resources.Load<GameObject>("Prefabs/CharSelect/PlayerScreen"), new Vector3(0, 0, 0), Quaternion.identity);
        playerScreen.Add(newPlayerScreen);
        newPlayerScreen.transform.SetParent(selectionScreen.transform);
        newPlayerScreen.transform.localPosition = new Vector3(playerScreenPosition, 75, 0);
        playerScreenPosition += 300;
    }


    void Start () {
        // Find game manager and canvas
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        selectionScreen = GameObject.Find("Canvas");
        
        for(int i = 0; i < maxPlayerCount; i++)
        {
            createPlayerBoxes();
        }
    }
    
	private void Update () {
        if ((((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Keypad0))
          ||  (Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Keypad1))
          ||  (Input.GetKeyDown(KeyCode.Joystick3Button1) || Input.GetKeyDown(KeyCode.Keypad2))
          ||  (Input.GetKeyDown(KeyCode.Joystick4Button1) || Input.GetKeyDown(KeyCode.Keypad3))) && playerCount < maxPlayerCount))
        {
            Debug.Log("Player " + playerCount);
            createPlayer(playerCount);
        }
    }
}
