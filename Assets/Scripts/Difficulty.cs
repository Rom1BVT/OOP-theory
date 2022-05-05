using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty
{
    public float spawnCooldown { get; private set; } // time between 2 spawn
    public int maxEnemyInScene { get; private set; } // max enemies in the scene
    public int maxTankInScene { get; private set; }
    public int maxVanInScene { get; private set; }
    public int maxPlaneInScene { get; private set; }
    public int pointToReach { get; private set; }    // point to reach before next difficulty

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                //Spawn 1 Van
                maxEnemyInScene = 1;
                maxVanInScene = 1;
                maxTankInScene = 0;
                maxPlaneInScene = 0;
                spawnCooldown = 1.0f;
                pointToReach = 50;
                break;
            case 1:
                //1 van every 3 seconds
                maxEnemyInScene = 3;
                maxVanInScene = 3;
                maxTankInScene = 1;
                maxPlaneInScene = 0;
                spawnCooldown = 3.0f;
                pointToReach = 9999;
                break;
            case 2:
                //1 van every 2 seconds
                break;
            case 3:
                //Spawn 1 tank
                break;
            case 4:
                //1 van every 4 sec and 1 tank (1 by 1) 
                break;
            case 5:
                //1 tank every 5 sec
                break;
            case 6:
                //spawn 1 plane
                break;
            case 7:
                //spawn 1 plane every 10s + 1 tank every 5 sec
                break;
        }
    }
}
