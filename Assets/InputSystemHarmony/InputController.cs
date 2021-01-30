using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject cube;
    float timecounter = 0;
    Dictionary<float, List<KeyPressedWeight>> stored;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

     /*   if(Input.GetButtonDown("Horizontal"))
        float translation = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");*/

        if(PlayerSceneMode.mode==PlayerSceneMode.PlayerSceneModeType.Fighting)
        {
            
        }
        
    }
}
