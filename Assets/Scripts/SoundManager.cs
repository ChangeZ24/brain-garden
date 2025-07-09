using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    public AudioSource audioSource;
    public AudioClip bgClip, happyClip, calmClip, sadClip, bossClip;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BgAudio()
    {
        audioSource.clip = bgClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void HappyAudio()
    {
        audioSource.clip = happyClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void CalmAudio()
    {
        audioSource.clip = calmClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void SadAudio()
    {
        audioSource.clip = sadClip;
        audioSource.loop = false;
        audioSource.Play();
    }

}
