using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
   
    public spawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnManager !=null)
        {
            spawnManager.Startspawner();
        }
        else
        {
            Debug.Log("SpawnManager is null");
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
