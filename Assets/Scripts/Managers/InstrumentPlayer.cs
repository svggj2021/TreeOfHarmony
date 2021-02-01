using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentPlayer : MonoBehaviour
{
    //this script is used to manage and play audio from the InstrumentSampler script ...see playSound() function below to see how to call play from another script

    public List<GameObject> instrumentList = new List<GameObject>();
    public int instrumentIndex;
    public static int globalinstrumentindex;
    public int noteIndex;
    public Component instrument;
    
    // Start is called before the first frame update
    void Start()
    {
 


    }

    // Update is called once per frame
    void Update()
    {
        NoteIndexKeyCheck();

    }


    //to call this from another script do something like this:   InstrumentPlayer.Instance.playSound(5, true, 2f, 0);  -- this will play the 5h instrument we have, in live play mode, for 2 sec duration, on the first note of the index
    public void playSound( bool livePlayingMode, float playDuration, int noteIndex)
    {
      
        if (livePlayingMode == true)
        {
             
                instrument = instrumentList[globalinstrumentindex].GetComponent<InstrumentSampler>();
                instrument.GetComponents<AudioSource>()[noteIndex].loop = true;
                instrument.GetComponents<AudioSource>()[noteIndex].Play();

                Debug.Log("Playing Sound: " + instrument.GetComponents<AudioSource>()[noteIndex].clip.name + "\n On Instrument: " + gameObject.name);
            if (instrument.GetComponents<AudioSource>()[noteIndex].time == playDuration)
                stopSound();

        }
    }

    public void playSound(int index,bool livePlayingMode, float playDuration, int noteIndex)
    {

        if (livePlayingMode == true)
        {

            instrument = instrumentList[index].GetComponent<InstrumentSampler>();
            instrument.GetComponents<AudioSource>()[noteIndex].loop = true;
            instrument.GetComponents<AudioSource>()[noteIndex].Play();

            Debug.Log("Playing Sound: " + instrument.GetComponents<AudioSource>()[noteIndex].clip.name + "\n On Instrument: " + gameObject.name);
            if (instrument.GetComponents<AudioSource>()[noteIndex].time == playDuration)
                stopSound();

        }
    }
    public void stopSound()
    {
            //below is necessary to ensure full cleanup of all audio stopping when it should (if you have something that switches instruments in the middle of holding down the fire key then you end up with a never ending loop...this fixes that with the 2 for statments
            for (int i = 0; i <= 11; i++)
            {
                for (int j = 0; j < instrumentList.Count; j++)
                {
                    instrument = instrumentList[j].GetComponent<InstrumentSampler>();
                    instrument.GetComponents<AudioSource>()[i].loop = false;
                    instrument.GetComponents<AudioSource>()[i].Stop();
                }
            }
        
    }

    public void NoteIndexKeyCheck()
    {
      
      if (Input.GetKeyDown("1"))
            globalinstrumentindex = 1;
        else if (Input.GetKeyDown("2"))
            globalinstrumentindex= 2;
        else if (Input.GetKeyDown("3"))
            globalinstrumentindex = 3;
        else if (Input.GetKeyDown("4"))
           globalinstrumentindex = 4;
        else if (Input.GetKeyDown("5"))
            globalinstrumentindex = 5;
        else if (Input.GetKeyDown("6"))
           globalinstrumentindex = 6;
        else if (Input.GetKeyDown("7"))
            globalinstrumentindex = 7;
        else if (Input.GetKeyDown("8"))
            globalinstrumentindex = 8;
      
    }

}
