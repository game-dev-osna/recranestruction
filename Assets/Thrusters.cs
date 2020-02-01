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


    // Update is called once per frame
    void Update()
    {
        if(gooController.thrust.x > 0) right.Play(); else right.Stop();
        if(gooController.thrust.x < 0) left.Play(); else left.Stop();
        if(gooController.thrust.y > 0) forward.Play(); else forward.Stop();
        if(gooController.thrust.y < 0) back.Play(); else back.Stop();
    }
}
