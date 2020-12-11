using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text timer;
    private bool finished = false;
    private float startTime;
    private bool timePause = false;
    private float timeSpend = 0;

    public bool TimePause { get => timePause; set => timePause = value; }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(finished)
        {
            return;
        }

        if (!TimePause)
        {
            timeSpend += Time.deltaTime;
        }
        
      
        string minutes = ((int)timeSpend / 60).ToString();
        string seconds = (timeSpend % 60).ToString("f2");

        timer.text = minutes + ":" + seconds;
    }


    public void finish()
    {
        finished = true;
        timer.color = Color.yellow;
    }
}
