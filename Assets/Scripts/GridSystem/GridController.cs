using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(BeatTimer))]
public class GridController : MonoBehaviour
{

    private static float waitcounter;

    public static bool inFightSceneMode=true;
    public static bool readyToCount=false;
    public float measuretime=2;
    public float waititime = 4;
    public GameObject LeftBoundary;
    public GameObject RightBoundary;
    public static Action Reset;
    int MAX_MEASURES = 15;
    int MEASURECOUNT;
/*    List<GameObject> listOfMeasures=new List<GameObject>();*/
    void Start()
    {
        
    }
    private void OnEnable()
    {
        BeatTimer.beatTime += beatCalled;
    }

    // Update is called once per frame
    void Update()
    {
      if(inFightSceneMode)
        {  
            waitcounter += Time.deltaTime;

            if(waitcounter>=waititime )
            {
                readyToCount = true;
               
            }
        }
    }

    public void SpawnMeasure()
    {
      GameObject measure= GameObject.Instantiate(Resources.Load<GameObject>("MeasureLine"),LeftBoundary.transform.position,Quaternion.identity);
        MeasureController ms = measure.GetComponent<MeasureController>();
        ms.rightPosition = RightBoundary;



    }
    public void beatCalled(float t)
    {
        if(readyToCount && MEASURECOUNT<MAX_MEASURES)
        {
            SpawnMeasure();
            MEASURECOUNT++;
        }
        else
        {
            reset();

        }
    }
    public void reset()
    {
        Reset.Invoke();
        readyToCount = false;
        waitcounter = 0;
        MEASURECOUNT = 0;
        
        inFightSceneMode = false;
       
    }
}
