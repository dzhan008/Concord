using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIManager : MonoBehaviour {

	[SerializeField] private RectTransform UpperPanel;
	[SerializeField] private PlayerInfo PlayerInfoPrefab;
	private float info_width = 0.2f * Screen.width;
	private int panel_padding = (int) (0.01f * Screen.width);
	private float panel_height = 0; // Set at Start()
	private float MultiplayerSpacing = 0; // Set at Start()
	private float InventoryPanel_Ratio = 0.7f;
	private int PlayerInfoCount = 1;

	public void AddPlayerInfo(Player new_player) {
		UpperPanel.GetComponent<HorizontalLayoutGroup>().spacing = MultiplayerSpacing;
		PlayerInfo newPlayerInfo = Instantiate(PlayerInfoPrefab, UpperPanel);
		newPlayerInfo.MyPlayer = new_player;
		new_player.MyPlayerInfo = newPlayerInfo;
		new_player.GetComponent<PlayerControls>().MyPlayerInfo = newPlayerInfo;
		new_player.CheckHeldInventory();
		PlayerInfoCount++;
	}
	
	public void RemovePlayerInfo(int removee_EntID) {
		PlayerInfo[] infos = UpperPanel.GetComponentsInChildren<PlayerInfo>();
		foreach (PlayerInfo info in infos) {
			if (info.MyPlayer.EntID == removee_EntID) {
				Destroy(info);
				break;
			}
		}
		PlayerInfoCount--;
	}

	private void Start() {
		SetUISizes();
	}

	private void SetUISizes() {
		panel_height = info_width * 0.3f + panel_padding;
		if (!UpperPanel) UpperPanel = GameObject.Find("UpperPanel").GetComponent<RectTransform>();
		UpperPanel.sizeDelta = new Vector2(UpperPanel.sizeDelta.x, panel_height);
		HorizontalLayoutGroup hz_group = UpperPanel.GetComponent<HorizontalLayoutGroup>();
		hz_group.padding = new RectOffset(panel_padding, panel_padding, panel_padding, panel_padding);
		hz_group.spacing = 0;
		MultiplayerSpacing = (Screen.width-(4*info_width)-(2*panel_padding)) / 3;
		Canvas.ForceUpdateCanvases();
		
		// If the prefab is not set, get the first default PlayerInfo object on screen and modify it
		// All additional PlayerInfo objects to be instantiated will be a copy of this first one
		if (!PlayerInfoPrefab) {
			PlayerInfoPrefab = transform.GetComponentInChildren<PlayerInfo>();
			if (Blackboard.playerArr.Length > 0) {
				Player first_player = Blackboard.playerArr[0];
				PlayerInfoPrefab.MyPlayer = first_player;
				PlayerInfoPrefab.MyInventory = first_player.MyInventory;
				first_player.MyPlayerInfo = PlayerInfoPrefab;
				first_player.GetComponent<PlayerControls>().MyPlayerInfo = PlayerInfoPrefab;
				first_player.CheckHeldInventory();
			}
		}
		RectTransform pif_rt = PlayerInfoPrefab.GetComponent<RectTransform>();
		pif_rt.sizeDelta = new Vector2(info_width, pif_rt.sizeDelta.y);
		float face_icon_width = pif_rt.sizeDelta.y;
		PlayerInfoPrefab.FaceBorder.sizeDelta = new Vector2(face_icon_width, PlayerInfoPrefab.FaceBorder.sizeDelta.y);
		PlayerInfoPrefab.Meters.anchoredPosition = new Vector2(face_icon_width, 0);
		PlayerInfoPrefab.Meters.sizeDelta = new Vector2(pif_rt.sizeDelta.x - face_icon_width, PlayerInfoPrefab.Meters.sizeDelta.y);
		Canvas.ForceUpdateCanvases();

		PlayerInfoPrefab.InventoryPanel.sizeDelta = 
			new Vector2(PlayerInfoPrefab.InventoryPanel.sizeDelta.x, InventoryPanel_Ratio*PlayerInfoPrefab.FaceBorder.sizeDelta.x);
		PlayerInfoPrefab.InventoryPanel.GetComponent<HorizontalLayoutGroup>().padding = 
			new RectOffset(0, 0, (int) (0.2f*PlayerInfoPrefab.InventoryPanel.sizeDelta.y), 0);
		Canvas.ForceUpdateCanvases();
		Vector2 temp_v2 = new Vector2(PlayerInfoPrefab.InventoryPanel.sizeDelta.y, PlayerInfoPrefab.InventoryPanel.sizeDelta.y);
		PlayerInfoPrefab.LeftArrow.sizeDelta = temp_v2;
		PlayerInfoPrefab.ItemFrame.sizeDelta = temp_v2;
		PlayerInfoPrefab.RightArrow.sizeDelta = temp_v2;

		PlayerInfoPrefab.Message.localPosition = new Vector2(PlayerInfoPrefab.Message.localPosition.x, PlayerInfoPrefab.InventoryPanel.sizeDelta.y * 1.3f);
		// Make alert message half of Inventory's height
		PlayerInfoPrefab.Message.sizeDelta = 
			new Vector2(PlayerInfoPrefab.InventoryPanel.sizeDelta.x, PlayerInfoPrefab.InventoryPanel.sizeDelta.y * 0.5f);
		Canvas.ForceUpdateCanvases();
	}
	
	/*
	public PlayerInfo testFirstPlayerInfo;
	public RectTransform Mess;
	// TODO: REMOVE THIS DEMO
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.N)) 
		{
			if (PlayerInfoCount < 4) {
				AddPlayerInfo(null);
			}
		}

		if (Input.GetKeyDown(KeyCode.M)) 
		{
			if (testFirstPlayerInfo.InventoryMessageAnimator.GetCurrentAnimatorStateInfo(0).IsName("InventoryMessageHidden"))
        	{
            	Mess.GetComponentInChildren<Text>().text = "Inv Full";
            	testFirstPlayerInfo.InventoryMessageAnimator.SetTrigger("DisplayInventoryMessage");
        	}
			else {
				print("sad life");
			}
		}
	}
	*/
	
}
