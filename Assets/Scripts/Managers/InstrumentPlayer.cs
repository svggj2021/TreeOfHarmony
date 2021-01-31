using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentPlayer : MonoBehaviour
{
    //this script is used to manage and play audio from the InstrumentSampler script ...see playSound() function below to see how to call play from another script

    public List<GameObject> instrumentList = new List<GameObject>();
    public int instrumentIndex;
    public int noteIndex;
    public Component instrument;
    public static InstrumentPlayer Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;


    }

    // Update is called once per frame
    void Update()
    {
        //   NoteIndexKeyCheck();
        instrument = instrumentList[instrumentIndex].GetComponent<InstrumentSampler>();
        Debug.Log("Instrument Being Used: " + instrumentIndex + " " + instrument.name);
        playSound(5, true, 2f, 0);
    }


    //to call this from another script do something like this:   InstrumentPlayer.Instance.playSound(5, true, 2f, 0);  -- this will play the 5h instrument we have, in live play mode, for 2 sec duration, on the first note of the index
    public void playSound(int instrumentListIndex, bool livePlayingMode, float playDuration, int noteIndex)
    {
        instrumentIndex = instrumentListIndex;
        if (livePlayingMode == true)
        {
             
                instrument = instrumentList[instrumentListIndex].GetComponent<InstrumentSampler>();
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
        if (Input.GetKeyDown("0"))
            noteIndex = 0;
        else if (Input.GetKeyDown("1"))
            noteIndex = 1;
        else if (Input.GetKeyDown("2"))
            noteIndex = 2;
        else if (Input.GetKeyDown("3"))
            noteIndex = 3;
        else if (Input.GetKeyDown("4"))
            noteIndex = 4;
        else if (Input.GetKeyDown("5"))
            noteIndex = 5;
        else if (Input.GetKeyDown("6"))
            noteIndex = 6;
        else if (Input.GetKeyDown("7"))
            noteIndex = 7;
        else if (Input.GetKeyDown("8"))
            noteIndex = 8;
        else if (Input.GetKeyDown("9"))
            noteIndex = 9;
        else if (Input.GetKeyDown(KeyCode.Keypad0))
            noteIndex = 10;
        else if (Input.GetKeyDown(KeyCode.Keypad1))
            noteIndex = 11;
    }

}
