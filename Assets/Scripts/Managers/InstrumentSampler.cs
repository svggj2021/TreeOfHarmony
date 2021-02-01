using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSampler : MonoBehaviour
{
    //this script sets up and assigns audio sources to each instrument and assigns the clips as well

    public GameObject instrument;
    public List<AudioClip> instrumentSamples = new List<AudioClip>();
    public List<AudioSource> audioSource;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= instrumentSamples.Count - 1; i++)
        {
            instrument.AddComponent<AudioSource>();
            instrument.GetComponents<AudioSource>()[i].spatialBlend = 0.5f;
            instrument.GetComponents<AudioSource>()[i].clip = instrumentSamples[i];
            instrument.GetComponents<AudioSource>()[i].playOnAwake = false;
        }
        for (int i = 0; i <= instrumentSamples.Count - 1; i++)
        {
            audioSource.Add(instrument.GetComponents<AudioSource>()[i]);
            audioSource[i].clip.LoadAudioData();
        }
    }


}