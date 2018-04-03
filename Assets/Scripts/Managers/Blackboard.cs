using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The elemental attribute added to this weapon or enemy
// The arrows indicate which element are weak against this element.
// An attacker will do 1.3x more damage if the enemy is weak against this element
// A defender will take 0.7x the damage dealt if the attacker is weak against the defender's element
public enum Element
{
    none = 0,
    fire = 1,       // => ice
    ice = 2,        // => water
    water = 3,      // => fire
    holy = 4,      // => dark
    dark = 5,       // => mythical
    mythical = 6    // => holy
}

public static class Blackboard {

    public static Player[] playerArr = new Player[9];

    private static IntersectionMatrix ElementChart;

    public static void SetElementChartReference(IntersectionMatrix chart) {
        if (!ElementChart) {
            ElementChart = chart;
        }
    }

    public static void setPlayerRef(Player player, int playerNum)
    {
        playerArr[playerNum] = player;
    }

    // Should be > 1.0 if super effective, < 1.0 if ineffective, and = 1.0 if neutral
    public static float GetElementMultiplier(Element attacker, Element defender) {
        return ElementChart.GetValue((int) attacker, (int) defender);
    }

}
