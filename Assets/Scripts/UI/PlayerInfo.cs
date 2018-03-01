using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

	public Player MyPlayer;
    public RectTransform InventoryPanel;
    public RectTransform LeftArrow;
    public RectTransform ItemFrame;
    public RectTransform RightArrow;
    [SerializeField] private Image HPMeter;
    [SerializeField] private Image StaminaMeter;
    [SerializeField] private Image SuperMeter;
    [SerializeField] private Image ItemIcon;

	// Reference to the border around the face of the assigned player
    public RectTransform FaceBorder {
        get { return _FaceBorder; }
        set {}
	}
    [SerializeField]
    private RectTransform _FaceBorder;

	// Refernce to the vertical layout group that holds the meters for the assigned player
    public RectTransform Meters {
        get { return _Meters; }
        set {}
	}
    [SerializeField]
    private RectTransform _Meters;

    private void Update() {
        // TODO REMOVE THIS DEMO
            HPMeter.fillAmount = .5f;
            SuperMeter.fillAmount = 0.3f;
        //HPMeter.fillAmount = MyPlayer.HP / MyPlayer.MaxHP;
        //...
        //ItemIcon = MyPlayer.FocusItem.Icon;
    }
}
