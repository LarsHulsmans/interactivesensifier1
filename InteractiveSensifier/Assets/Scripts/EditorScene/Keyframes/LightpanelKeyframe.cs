using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightpanelKeyframe : IKeyframe
{
    public float timestamp { get; set; }
    public Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType actuatorType { get; set; }
    public int r, g, b, w;
    public LightpanelKeyframe(float timestamp)
    {
        this.timestamp = timestamp;
        actuatorType = Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.LIGHT_PANEL;
    }

    public void SetColor(int r, int g, int b, int w) 
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.w = w;
    }
}
