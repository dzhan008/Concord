using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlCreator {

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
