using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introduceSource, loopSource;
    // Start is called before the first frame update
    void Start()
    {
        introduceSource.Play();
        loopSource.PlayScheduled(AudioSettings.dspTime + introduceSource.clip.length);
    }

}
