using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty
{
    //ENCAPSULATION
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
                //1 van every 3 seconds (max 2)
                maxEnemyInScene = 2;
                maxVanInScene = 2;
                maxTankInScene = 0;
                maxPlaneInScene =0;
                spawnCooldown = 3.0f;
                pointToReach = 300;
                break;
            case 2:
                //1 van every 2 seconds (max 3)
                maxEnemyInScene = 3;
                maxVanInScene = 3;
                maxTankInScene = 0;
                maxPlaneInScene = 0;
                spawnCooldown = 2.0f;
                pointToReach = 800;
                break;
            case 3:
                //Spawn 1 tank
                maxEnemyInScene = 1;
                maxVanInScene = 0;
                maxTankInScene = 1;
                maxPlaneInScene = 0;
                spawnCooldown = 5.0f;
                pointToReach = 950;
                break;
            case 4:
                //1 van every 4 sec and 1 tank (1 by 1) 
                maxEnemyInScene = 4;
                maxVanInScene = 4;
                maxTankInScene = 1;
                maxPlaneInScene = 0;
                spawnCooldown = 4.0f;
                pointToReach = 1500;
                break;
            case 5:
                //1 tank every 5 sec
                maxEnemyInScene = 4;
                maxVanInScene = 0;
                maxTankInScene = 4;
                maxPlaneInScene = 0;
                spawnCooldown = 5.0f;
                pointToReach = 2250;
                break;
            case 6:
                //spawn 1 plane
                maxEnemyInScene = 1;
                maxVanInScene = 0;
                maxTankInScene = 0;
                maxPlaneInScene = 1;
                spawnCooldown = 10.0f;
                pointToReach = 3000;
                break;
            case 7:
                //spawn 1 plane every 10s + 1 tank every 5 sec
                maxEnemyInScene = 6;
                maxVanInScene = 4;
                maxTankInScene = 4;
                maxPlaneInScene = 1;
                spawnCooldown = 5.0f;
                pointToReach = 99999999;
                break;
        }
    }
}
