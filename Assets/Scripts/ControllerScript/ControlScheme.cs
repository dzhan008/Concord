using UnityEngine;

public class ControlScheme : ScriptableObject
{
    public KeyCode lightAttack;
    public KeyCode strongAttack;
    public KeyCode interact;
    public KeyCode start;
    public KeyCode jump;
    public KeyCode InventoryScrollLeft;
    public KeyCode InventoryScrollRight;
    public KeyCode InventoryUseItem;
    public KeyCode InventoryDiscard;

    public static ControlScheme createControlMap(int entID)
    {
        ControlScheme controls = ScriptableObject.CreateInstance<ControlScheme>();
        switch (entID)
        {
            case -1:
                controls.jump = KeyCode.RightShift;
                break;
            case 0:
                controls.lightAttack = KeyCode.Z;
                controls.strongAttack = KeyCode.X;
                controls.interact = KeyCode.E;
                controls.start = KeyCode.Escape;
                controls.jump = KeyCode.Space;
                controls.InventoryScrollLeft = KeyCode.R;
                controls.InventoryScrollRight = KeyCode.F;
                controls.InventoryUseItem = KeyCode.Q;
                controls.InventoryDiscard = KeyCode.Z;
                break;
            case 1:
                controls.lightAttack = KeyCode.Joystick1Button1;
                controls.interact = KeyCode.Joystick1Button0;
                controls.start = KeyCode.Joystick1Button9;
                break;
            case 2:
                controls.lightAttack = KeyCode.Joystick2Button1;
                break;
            default:
                break;
        }
        return controls;
    }
}
