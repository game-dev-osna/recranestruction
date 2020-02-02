using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrusters : MonoBehaviour
{
    public ParticleSystem left;
    public ParticleSystem right;
    public ParticleSystem forward;
    public ParticleSystem back;

    public GooController gooController;
    public AudioSource audioA;
    public AudioSource audioB;


    private void Start()
    {
        forward.Stop();
        back.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (gooController.thrust.x > 0)
        {
            if(!audioA.isPlaying)
                audioA.Play();
            if(!right.isPlaying)
                right.Play();
        }
        else
        {
            if (audioA.isPlaying)
                audioA.Stop();
            if (right.isPlaying)
                right.Stop(); 
        }
        if (gooController.thrust.x < 0)
        {
            if (!audioB.isPlaying)
                audioB.Play();
            if (!left.isPlaying)
                left.Play();
        }
        else
        {
            if (audioB.isPlaying)
                audioB.Stop();
            if (left.isPlaying)
                left.Stop(); 
        }
        //if (gooController.thrust.y > 0) forward.Play(); else forward.Stop();
        //if (gooController.thrust.y < 0) back.Play(); else back.Stop();

    }
}
