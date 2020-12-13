using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class LevelManger : MonoBehaviour
{
    public Text enemyDeathText;
    public spawnManager spawnManager;
    private static LevelManger m_Instance;
    public static LevelManger Instance
    { get { return m_Instance; } }
    private int Deathcount = 0;
    public int deathcount
    { get { return Deathcount; } set { Deathcount = value; } }
    // Start is called before the first frame update
    void Start()
    {
        m_Instance = this;
        if (spawnManager !=null)
        {
            spawnManager.Startspawner();
        }
        else
        {
            Debug.Log("SpawnManager is null");
        }
    }

    //// Update is called once per frame
    void Update()
    {
        enemyDeathText.text = "EnemyDeathCount: "+ Deathcount.ToString();
    }

}
