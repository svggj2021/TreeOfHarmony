using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BeatTimer : MonoBehaviour
{
    
    // Measure is unused at this time
    [Serializable]
    public struct TimerVariables
    {
        public float measure;
        public float bpm;
        public float minutesInSeconds;
    }

    //Expose Time Variables 
    public TimerVariables TimeSettings;
    
    // Private variable for current time
    private float beat_time;

    void Update()
    {
        update_beat();
    }

    // Checks for a beat ever X seconds
    public void update_beat()
    {
        if(Time.time > beat_time)
        {
            beat_time = Time.time + TimeSettings.bpm / TimeSettings.minutesInSeconds;
            
            // Call functions here to be executed every X seconds
            
            // This is just a debug
            Debug.Log(Mathf.Round(beat_time) + " Seconds has elapsed");
        }
    }
}
