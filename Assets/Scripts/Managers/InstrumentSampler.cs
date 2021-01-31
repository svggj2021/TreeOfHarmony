using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSampler : MonoBehaviour
{
    public GameObject player;

    public AudioClip[] drumSamples_standard, drumSamples_electro;
    public AudioClip[] guitarSamples_distortion, guitarSamples_nylon;
    public AudioClip[] hornSamples_frenchHorn, hornSamples_TenorSax;
    public AudioClip[] voiceSamples_heavenlyVoices, voiceSamples_motionVoices;
    public AudioClip[] windSamples_panFlute, windSamples_expressiveFlute;


    public AudioSource[] audioSource;


    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i <= drumSamples_standard.Length; i++)
        {

            player.AddComponent<AudioSource>();
        }



        Debug.Log(player.GetComponents<AudioSource>().Length);

        for (int i = 0; i <= player.GetComponents<AudioSource>().Length + 1; i++)
        {
            audioSource[i] = player.GetComponents<AudioSource>()[i];
            audioSource[i].clip = drumSamples_standard[i];

        }



    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(PlaySequence());

    }

    IEnumerator PlaySequence()
    {
        for (int i = 0; i <= audioSource.Length - 1; i++)
        {


            yield return new WaitUntil(() => !audioSource[i].isPlaying);
            audioSource[i].Play();


        }

    }
}
