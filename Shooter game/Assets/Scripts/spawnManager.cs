using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public List<UnitSpawn> spawns;

    public void Startspawner()
    {
        foreach (UnitSpawn spawn in spawns)
        {
            spawn.StartSpawner();
        }
    }
}
 
