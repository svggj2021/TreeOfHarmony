using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testclickscript : MonoBehaviour
{
    public float timecounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GridController.readyToCount)
        {
            timecounter += Time.deltaTime;

            /*if (Input.GetMouseButtonDown(0) && BeatTimer.MeasureTime >= 0)
            {

                //1/8th of a measure hence 0.25f
                //divide by 2 because for 2 units -->x so for the visual width of the measure MeasureController.widthOfMeasure --> (x/2)*MeasureController.widthofMeasure
                //    | is the measure and --- is the wall, so offset is calculated from the wall and back:  <-.(15.7)-----|(14th one)
                *//*
                                float offset = MeasureController.widthOfMeasure * (Mathf.Round((timecounter - BeatTimer.MeasureTime) / 0.25f) * 0.25f) / 2f;
                                GameObject gameobjecttemp = GameObject.Instantiate(Resources.Load<GameObject>("Note"), new Vector3(MeasureController.LatestMeasure.transform.position.x - offset, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, MeasureController.LatestMeasure.transform.position.z), Quaternion.identity);
                                gameobjecttemp.transform.SetParent(MeasureController.LatestMeasure.transform, true);*//*
                timecounter += Time.deltaTime;

                //1/8th of a measure hence 0.25f
                //divide by 2 because for 2 units -->x so for the visual width of the measure MeasureController.widthOfMeasure --> (x/2)*MeasureController.widthofMeasure
                //    | is the measure and --- is the wall, so offset is calculated from the wall and back:  <-.(15.7)-----|(14th one)

                float offset = MeasureController.widthOfMeasure * (Mathf.Round((timecounter - BeatTimer.MeasureTime) / 0.25f) * 0.25f) / 2f;



                float vertspacing = MeasureController.LatestMeasure.GetComponent<MeasureController>().vertspacing;
                int index = (int)Mathf.Clamp(Mathf.Round(.y / vertspacing), 0, 12 * vertspacing);
              GameObject gameobjecttemp = GameObject.Instantiate(Resources.Load<GameObject>("Note"), new Vector3(MeasureController.LatestMeasure.transform.position.x - offset, MeasureController.LatestMeasure.transform.position.y + index * vertspacing, MeasureController.LatestMeasure.transform.position.z), Quaternion.identity);
                gameobjecttemp.transform.SetParent(MeasureController.LatestMeasure.transform, true);


            }*/
        }
    }
}
