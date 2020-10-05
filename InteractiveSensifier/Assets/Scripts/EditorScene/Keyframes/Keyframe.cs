using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyframe
{
    public float timestamp { get; private set; }
    public int intensity { get; private set; }

    public Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType actuatorType;



    public Keyframe(float timestamp)
    {
        this.timestamp = timestamp;
    }


    public virtual void SetIntensity(int intensity) 
    {
        this.intensity = intensity;
    }
}
