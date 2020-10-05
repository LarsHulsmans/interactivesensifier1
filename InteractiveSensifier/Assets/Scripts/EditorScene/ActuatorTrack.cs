using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActuatorTrack : MonoBehaviour
{
    public RectTransform track;
    public Text Title;

    public float lenghtPerSecond;

    float tracklenght;

    public List<IKeyframe> keyframes = new List<IKeyframe>();
    public GameObject KeyframePrefab;

    public Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType actuatorType;


    private void Start()
    {
        //for testing purposes
        SetTrackLength(100);
        for (int i = 1; i < 10; i++)
        {
            AddKeyframe(i * 10);
        }
    }

    public void SetTrackLength(int videoLenght) 
    {
        tracklenght = videoLenght * lenghtPerSecond;

        RectTransform thisTransform = GetComponent<RectTransform>();
        thisTransform.sizeDelta = new Vector2(235 + tracklenght , thisTransform.sizeDelta.y);

        transform.parent.GetComponent<ScaleContainer>().ScaleContent();
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



        //frame.SetIntensity(Random.Range(0, 101));
        //keyframes.Add(frame);
       
        //todo: add to timeline
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