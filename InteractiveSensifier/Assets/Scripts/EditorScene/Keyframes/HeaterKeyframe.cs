using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaterKeyframe : IKeyframe
{
    public float timestamp { get; set; }
    public Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType actuatorType { get; set; }
    public int intensity;

    public HeaterKeyframe(float timestamp)
    {
        this.timestamp = timestamp;
        actuatorType = Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.HEATER;
    }
}
