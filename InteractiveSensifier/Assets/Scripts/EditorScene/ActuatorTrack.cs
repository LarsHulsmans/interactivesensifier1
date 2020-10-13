﻿using Sensiks.SDK.Shared.SensiksDataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActuatorTrack : MonoBehaviour
{
    public RectTransform track;
    public Text actuatorTitle;
    public Text actuatorPosition;

    public float lenghtPerSecond;

    float tracklenght;

    public List<IKeyframe> keyframes = new List<IKeyframe>();
    public GameObject KeyframePrefab;

    public Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType actuatorType;

    private Sensiks.SDK.Shared.SensiksDataTypes.HeaterPosition heaterPosition;
    private Sensiks.SDK.Shared.SensiksDataTypes.FanPosition fanPostion;
    private Sensiks.SDK.Shared.SensiksDataTypes.Scent scent;
    private Sensiks.SDK.Shared.SensiksDataTypes.LightPanelPosition lightPanelPosition;


    /* private void Start()
     {
         //for testing purposes
         SetTrackLength(100);
         for (int i = 1; i < 10; i++)
         {
             AddKeyframe(i * 10);
         }
     }*/

    public void SetPosition(Sensiks.SDK.Shared.SensiksDataTypes.HeaterPosition pos) 
    {
        heaterPosition = pos;
    }

    public void SetPosition(Sensiks.SDK.Shared.SensiksDataTypes.FanPosition pos)
    {
        fanPostion = pos;
    }

    public void SetPosition(Sensiks.SDK.Shared.SensiksDataTypes.Scent pos)
    {
        scent = pos;
    }

    public void SetPosition(Sensiks.SDK.Shared.SensiksDataTypes.LightPanelPosition pos)
    {
        lightPanelPosition = pos;
    }

    public void SetType(ActuatorType t) 
    {
        actuatorType = t;
    }

    public void SetTrackLength(int videoLenght) 
    {
        actuatorTitle.text = StringExtentions.Pascal(actuatorType.ToString());

        switch (actuatorType)
        {
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.HEATER:
                actuatorPosition.text = StringExtentions.Pascal(heaterPosition.ToString());
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.FAN:
                actuatorPosition.text = StringExtentions.Pascal(fanPostion.ToString());
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.SCENT:
                actuatorPosition.text = StringExtentions.Pascal(scent.ToString());
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.CEILING:
                actuatorPosition.text = "";
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.LIGHT_PANEL:
                actuatorPosition.text = StringExtentions.Pascal(lightPanelPosition.ToString());
                break;
        }

        tracklenght = videoLenght * lenghtPerSecond;

        RectTransform thisTransform = track.GetComponent<RectTransform>();
        thisTransform.sizeDelta = new Vector2(tracklenght , thisTransform.sizeDelta.y);

        track.transform.parent.GetComponent<ScaleContainer>().ScaleContent();
    }

    public void AddKeyframe(float timestamp) 
    {
        Debug.Log(actuatorType);
        //IKeyframe frame = null;
        switch (actuatorType) 
        {
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.HEATER:
                HeaterKeyframe hFrame = new HeaterKeyframe(timestamp);
                keyframes.Add(hFrame);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.FAN:
                FanKeyframe fFrame = new FanKeyframe(timestamp);
                keyframes.Add(fFrame);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.SCENT:
                ScentKeyframe sFrame = new ScentKeyframe(timestamp);
                keyframes.Add(sFrame);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.CEILING:
                CeilingAnimationKeyframe cFrame = new CeilingAnimationKeyframe(timestamp);
                keyframes.Add(cFrame);
                break;
            case Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType.LIGHT_PANEL:
                LightpanelKeyframe lFrame = new LightpanelKeyframe(timestamp);
                keyframes.Add(lFrame);
                break;
        }

        float keyframeXPos = timestamp * lenghtPerSecond;
        GameObject keyframeGameObject = GameObject.Instantiate(KeyframePrefab, track);
        keyframeGameObject.GetComponent<RectTransform>().localPosition = new Vector2( keyframeXPos, -50);
        keyframeGameObject.GetComponent<KeyframeBehaviour>().AddKeyframe(keyframes[keyframes.Count-1]);
    }

    public void RemoveKeyframe(IKeyframe frame) 
    {
        if (keyframes.Contains(frame)) 
        {
            keyframes.Remove(frame);
            //todo: remove from timeline
        }
    }
}