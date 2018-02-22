using UnityEngine;

public class ControlScheme : ScriptableObject
{
    public KeyCode attack;
    public KeyCode interact;
    public KeyCode start;
    public KeyCode jump;

    public static ControlScheme createControlMap(int entID)
    {
        ControlScheme controls = ScriptableObject.CreateInstance<ControlScheme>();
        switch (entID)
        {
            case 0:
                controls.attack = KeyCode.Mouse0;
                controls.interact = KeyCode.E;
                controls.start = KeyCode.Escape;
                controls.jump = KeyCode.Space;
                break;
            case 1:
                controls.attack = KeyCode.Joystick1Button1;
                controls.interact = KeyCode.Joystick1Button0;
                controls.start = KeyCode.Joystick1Button9;
                break;
            default:
                break;
        }
        return controls;
    }
}
