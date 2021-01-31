using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureController : MonoBehaviour
{
    public float speed;
    public GameObject rightPosition;
    float vertspacing = 0.5f;
    // Start is called before the first frame update
    void Start()
    {   
        for(int i=0;i<12;i++)
        {
          GameObject object1= GameObject.Instantiate(Resources.Load<GameObject>("HorizLine"),new Vector3(transform.position.x,i*vertspacing,transform.position.z), Quaternion.identity);
            object1.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>=rightPosition.transform.position.x)
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

    }
}
