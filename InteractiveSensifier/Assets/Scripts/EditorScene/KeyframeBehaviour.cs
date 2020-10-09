using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyframeBehaviour : MonoBehaviour
{
    public IKeyframe Keyframe;

    public RectTransform button;

    public Slider slider;

    public GameObject editValueSlider;
    public GameObject editValueCeiling;
    public GameObject editValueLightpanels;

    public void AddKeyframe(IKeyframe frame) 
    {
        this.Keyframe = frame;
        Debug.Log(frame.actuatorType);
        switch (frame.actuatorType) 
        {
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.HEATER:
                HeaterKeyframe hkf = Keyframe as HeaterKeyframe;
                SetHeight(hkf.intensity);
                button.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.FAN:
                FanKeyframe fkf = Keyframe as FanKeyframe;
                SetHeight(fkf.intensity);
                button.gameObject.GetComponent<Image>().color = Color.blue;
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.SCENT:
                ScentKeyframe skf = Keyframe as ScentKeyframe;
                SetHeight(skf.intensity);
                button.gameObject.GetComponent<Image>().color = Color.yellow;
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
                SetCeilingKeyframeColor(ceilkf.animation);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.LIGHT_PANEL:
                LightpanelKeyframe lkf = Keyframe as LightpanelKeyframe;
                Debug.Log(lkf.r);
                break;
        }
        
    }

    public void OpenEdit() 
    {
        switch (Keyframe.actuatorType) 
        {
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.HEATER:
                editValueSlider.SetActive(true);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.FAN:
                editValueSlider.SetActive(true);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.SCENT:
                editValueSlider.SetActive(true);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.CEILING:
                editValueCeiling.SetActive(true);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.LIGHT_PANEL:
                editValueLightpanels.SetActive(true);
                break;
        }
        
    }

    public void CloseEdit() 
    {
        editValueSlider.SetActive(false);
        editValueCeiling.SetActive(false);
        editValueLightpanels.SetActive(false);
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

    public void SetValue() 
    {
        switch (Keyframe.actuatorType) 
        {
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.HEATER:
                (Keyframe as HeaterKeyframe).intensity = (int)slider.value;
                SetHeight(slider.value);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.FAN:
                (Keyframe as FanKeyframe).intensity = (int)slider.value;
                SetHeight(slider.value);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.SCENT:
                (Keyframe as ScentKeyframe).intensity = (int)slider.value;
                SetHeight(slider.value);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.CEILING:
                //don't do anything becuase it needs a button
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.LIGHT_PANEL:
                //don't do anything because it needs a color picker
                break;
        }
    }

    private void SetCeilingKeyframeColor(Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation option) 
    {
        switch (option)
        {
            case Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation.FOREST:
                button.gameObject.GetComponent<Image>().color = Color.green;
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation.SKY:
                button.gameObject.GetComponent<Image>().color = Color.blue;
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation.STARS:
                button.gameObject.GetComponent<Image>().color = Color.black;
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation.SUN:
                button.gameObject.GetComponent<Image>().color = Color.yellow;
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation.OFF:
                button.gameObject.GetComponent<Image>().color = Color.gray;
                break;
        }

        if ((Keyframe as CeilingAnimationKeyframe).isOn)
        {
            SetHeight(100);
        }
        else
        {
            SetHeight(0);
        }
    }

    public void SetValueCeiling(Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation option) 
    {
        (Keyframe as CeilingAnimationKeyframe).animation = option;
        SetCeilingKeyframeColor(option);
    }

    public void SetPanelValue(Color col)
    {
        //(Keyframe as LightpanelKeyframe).position = pos;

        (Keyframe as LightpanelKeyframe).r = (int)(255 * col.r);
        (Keyframe as LightpanelKeyframe).g = (int)(255 * col.g);
        (Keyframe as LightpanelKeyframe).b = (int)(255 * col.b);
        (Keyframe as LightpanelKeyframe).w = (int)(255 * col.a);
        button.gameObject.GetComponent<Image>().color = new Color(col.r, col.g, col.b);
        
    }
}