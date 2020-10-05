using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyframeBehaviour : MonoBehaviour
{
    public IKeyframe Keyframe;

    public RectTransform button;

    public void AddKeyframe(IKeyframe frame) 
    {
        this.Keyframe = frame;
        Debug.Log(frame.actuatorType);
        switch (frame.actuatorType) 
        {
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.HEATER:
                HeaterKeyframe hkf = Keyframe as HeaterKeyframe;
                SetHeight(hkf.intensity);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.FAN:
                FanKeyframe fkf = Keyframe as FanKeyframe;
                SetHeight(fkf.intensity);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.SCENT:
                ScentKeyframe skf = Keyframe as ScentKeyframe;
                SetHeight(skf.intensity);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.CEILING:
                CeilingAnimationKeyframe ceilkf = Keyframe as CeilingAnimationKeyframe;
                if (ceilkf.isOn) 
                {
                    SetHeight(100);
                }
                else 
                {
                    SetHeight(0);
                }
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.LIGHT_PANEL:
                LightpanelKeyframe lkf = Keyframe as LightpanelKeyframe;
                Debug.Log(lkf.r);
                break;
        }
        
    }

    public void SetHeight(float intensity) 
    {
        float height = Remap(intensity, 0, 100, 10, 90);
        button.localPosition = new Vector2(0, height);
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
