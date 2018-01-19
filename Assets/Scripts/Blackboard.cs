using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Blackboard {
    public static Player[] playerArr = new Player[9];

    public static void setPlayerRef(Player player, int playerNum)
    {
        playerArr[playerNum] = player;
    }
}
