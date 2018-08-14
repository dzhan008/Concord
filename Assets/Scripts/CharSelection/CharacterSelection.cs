using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterSelection : MonoBehaviour {
    private GameObject[] characters;
    private Sprite[] characterSprites;
    private GameObject selectionScreen;
    private int index = 0;
    private CharacterManager characterManagerScript;

    // Methods
    public void SetPlayer(int playerIndex, bool positive)
    {
        characters[index].SetActive(false);
        if (positive)
        {
            index++;
            if (index > transform.childCount - 1)
            {
                index = 0;
            }
        }
        else
        {
            index--;
            if (index < 0)
            {
                index = transform.childCount - 1;
            }
        }
        characters[index].SetActive(true);
    }

    public void SetPlayerStats(int playerIndex)
    {
        Text playerStats = characterManagerScript.playerScreen[playerIndex].transform.Find("Stats").gameObject.GetComponent<Text>();
        playerStats.text = "STR\t" + characters[index].gameObject.GetComponent<Player>().strength + "\nAGI\t" + characters[index].gameObject.GetComponent<Player>().agility + "\nINT\t" + characters[index].gameObject.GetComponent<Player>().intelligence;
    }

    public void SetPlayerSplash(int playerIndex)
    {
        Image playerSplash = selectionScreen.transform.GetChild(playerIndex).gameObject.transform.Find("Splash").gameObject.GetComponent<Image>();
        if (playerSplash)
        {
            playerSplash.sprite = characterSprites[index];
        }
    }
    
    private void Awake()
    {
        selectionScreen = GameObject.Find("Canvas");
        // Load character splash art
        characterSprites = new Sprite[transform.childCount];
        characterSprites = Resources.LoadAll("Sprites/SplashArt", typeof(Sprite)).Cast<Sprite>().ToArray();
    }

    private void Start()
    {
        GameObject characterManager = GameObject.Find("CharacterManager");
        characterManagerScript = characterManager.GetComponent<CharacterManager>();
        
        // Reserving array size for each character
        characters = new GameObject[transform.childCount];

        // Load objects/sprites
        for (int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
        }

        // Turn off all models
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Turn on first model
        characters[index].SetActive(true);
    }
}
