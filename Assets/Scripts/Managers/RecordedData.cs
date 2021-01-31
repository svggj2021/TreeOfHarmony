using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedData
{
    public int instrument;
    public float duration;
    public int pitchindex;
    public RecordedData(int instrument,
    float duration,int pitchindex)
    {

        this.instrument = instrument;
        this.duration = duration;
        this.pitchindex = pitchindex;
    }

    
}
