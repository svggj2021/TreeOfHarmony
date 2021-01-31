using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BeatTimer : MonoBehaviour
{
    public static Action<float> beatTime;
    public static float MeasureTime=-2;
 
    // Measure is unused at this time
    [Serializable]
    public struct TimerVariables
    {
    
        public float bpm;
        public float minutesInSeconds;
    }
    int counter = 0;

    //Expose Time Variables 
    public TimerVariables TimeSettings;
    
    // Private variable for current time
    private float beat_time;
    private void OnEnable()
    {
        GridController.Reset += reset;
    }
    private void Awake()
    {
        if (TimeSettings.bpm == 0)
            TimeSettings.bpm = 120;
        if (TimeSettings.minutesInSeconds == 0)
            TimeSettings.minutesInSeconds = 60;
    }

    void Update()
    {
        update_beat();
    }

    // Checks for a beat ever X seconds
    public void update_beat()
    {
        if (GridController.readyToCount)
        {
            if (Time.time > beat_time)
            {
                beat_time = Time.time + TimeSettings.bpm / TimeSettings.minutesInSeconds;

                // Call functions here to be executed every X seconds

                // This is just a debug
                counter += 1;
                MeasureTime += 2;
                beatTime.Invoke(Mathf.Round(beat_time));
                
            }
        }
    }
  
    public void reset()
    {
        MeasureTime = -2;
        BasicPlayer.timecounter = 0;
    }
}
