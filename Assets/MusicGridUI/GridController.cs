using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static float timecounter;
    private static float waitcounter;
    private static int measurecounter;
    public static bool inFightSceneMode=true;
    public static bool readyToCount=false;
    public float measuretime=2;
    public float waititime = 4000;
    public GameObject LeftBoundary;
    public GameObject RightBoundary;
    private bool waitover;
    int measurecount = 3;
    List<GameObject> listOfMeasures=new List<GameObject>();
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
        if(readyToCount)
        {
            SpawnMeasure();
        }
    }
}
