using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureController : MonoBehaviour
{
    public float speed;
    public GameObject rightPosition;
    public float vertspacing = 0.5f;
    public static GameObject LatestMeasure;
    public static float widthOfMeasure=-1;
    
    // Start is called before the first frame update
    void Start()
    {
        if(LatestMeasure)
        Debug.Log(transform.position - LatestMeasure.transform.position);
        LatestMeasure = gameObject;
        for(int i=0;i<12;i++)
        {
          GameObject object1= GameObject.Instantiate(Resources.Load<GameObject>("HorizLine"),new Vector3(transform.position.x,transform.position.y+ i*vertspacing,transform.position.z), Quaternion.identity);
            object1.transform.parent = transform;
        }
        if(widthOfMeasure<=0)
        {

            //since measure lives for 2 seconds
            widthOfMeasure = speed * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>=rightPosition.transform.position.x*2)
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

    }
}
