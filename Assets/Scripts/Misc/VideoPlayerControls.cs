using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerControls : MonoBehaviour
{

    public VideoPlayer vp ;
    public Image Progress;


    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        
        vp.Play();
        vp.Pause();
        vp.time = .1f;
    }

    public void Play()
    {
        vp.Play();
    }

    public void Pause()
    {
        vp.Pause();
    }
    public void Stop()
    {
        vp.time = 0f;
        vp.Pause();
    }
    
    public void Rewind()
    {
        vp.time -= 2f;
    }

    public void Foward()
    {
        vp.time += 2f;
    }

    public void Update()
    {
        if (vp.frameCount >0)
        {
            Progress.fillAmount = (float)vp.frame/(float)vp.frameCount;
        }
    }
}
