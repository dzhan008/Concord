using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

	public Player MyPlayer;         // Set by UIManager
    public Inventory MyInventory;   // Set by UIManager
    public RectTransform InventoryPanel;
    public RectTransform LeftArrow;
    public RectTransform ItemFrame;
    public Image ItemSprite;
    public RectTransform RightArrow;
    public RectTransform Message;
    public Animator InventoryMessageAnimator;

    [SerializeField] private Image HPMeter;
    [SerializeField] private Image StaminaMeter;
    [SerializeField] private Image SuperMeter;

	// Reference to the border around the face of the assigned player
    public RectTransform FaceBorder {
        get { return _FaceBorder; }
        set {}
	} [SerializeField] private RectTransform _FaceBorder;

	// Reference to the vertical layout group that holds the meters for the assigned player
    public RectTransform Meters {
        get { return _Meters; }
        set {}
	} [SerializeField] private RectTransform _Meters;

    public void RefreshIventory() {
        if (MyInventory.CurrentItem == null) {
            ItemSprite.enabled = false;
            return;
        }
        ItemSprite.sprite = MyInventory.CurrentItem.MySprite;
        ItemSprite.enabled = true;
    }

    public void ScrollLeft() 
    {   
        if (MyInventory.CurrentItem == null)
            return;
        MyPlayer.ScrollLeft();
        ItemSprite.sprite = MyInventory.CurrentItem.MySprite;
        ItemSprite.enabled = true;
        if (LeftArrow.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("LeftArrowIdle"))
            LeftArrow.GetComponent<Animator>().SetTrigger("Select");
    }

    public void ScrollRight() 
    {
        if (MyInventory.CurrentItem == null)
            return;
        MyPlayer.ScrollRight();
        ItemSprite.sprite = MyInventory.CurrentItem.MySprite;
        ItemSprite.enabled = true;
        if (RightArrow.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("RightArrowIdle"))
            RightArrow.GetComponent<Animator>().SetTrigger("Select");
    }

    public void UseItem() 
    {
        MyPlayer.UseItem();
        if (ItemFrame.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ItemFrameIdle"))
            ItemFrame.GetComponent<Animator>().SetTrigger("Select");
    }

    public void DisplayMessage(string message)
    {
        if (InventoryMessageAnimator.GetCurrentAnimatorStateInfo(0).IsName("InventoryMessageHidden"))
        {
            Message.GetComponentInChildren<Text>().text = message;
            InventoryMessageAnimator.SetTrigger("DisplayInventoryMessage");
        }
    }

    private void Update() {
        // TODO REMOVE THIS DEMO
            HPMeter.fillAmount = .5f;
            SuperMeter.fillAmount = 0.3f;
        //HPMeter.fillAmount = MyPlayer.HP / MyPlayer.MaxHP;
        //...
        //ItemSprite = MyPlayer.FocusItem.MySprite;
    }
}
