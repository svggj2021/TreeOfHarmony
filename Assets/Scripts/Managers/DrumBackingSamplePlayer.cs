using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumBackingSamplePlayer : MonoBehaviour
{
    public List<AudioClip> drumBackingSamples = new List<AudioClip>();
    private int randomIndex;
    private AudioSource audioSource;

    public static DrumBackingSamplePlayer Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        randomIndex = Random.Range(0, drumBackingSamples.Count);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = drumBackingSamples[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        playSound();
        
    }

    public void playSound()
    {        
            audioSource.Play();
    }
}
