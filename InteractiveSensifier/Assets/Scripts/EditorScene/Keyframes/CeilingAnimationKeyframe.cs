using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingAnimationKeyframe : IKeyframe
{
    public float timestamp { get; set; }
    public Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType actuatorType { get; set; }
    public bool isOn = true;
    public Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation animation = Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation.OFF;

    public CeilingAnimationKeyframe(float timestamp)
    {
        this.timestamp = timestamp;
        actuatorType = Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.CEILING;
    }
}
