using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sensiks.SDK.Shared;
using Sensiks.SDK.Shared.SensiksDataTypes;
using System;
using System.Globalization;

public class TimelineManager : MonoBehaviour
{
    public GameObject ActuatorTrackPrefab;
    public GameObject ActuatorCardPrefab;
    public Transform trackHolder;
    public Transform cardHolder;
    private List<ActuatorTrack> actuatorTracks = new List<ActuatorTrack>();

    private void Start()
    {
        SetupActuators();
    }

    private void SetupActuators() 
    {
        foreach (FanPosition f in Enum.GetValues(typeof(FanPosition)))
        {
            //UnityEngine.Debug.Log(StringExtentions.Pascal(f.ToString()));
            GameObject track = GameObject.Instantiate(ActuatorTrackPrefab, trackHolder);
            GameObject card = GameObject.Instantiate(ActuatorCardPrefab, cardHolder);
            ActuatorTrack trackBehaviour = card.GetComponent<ActuatorTrack>();
            trackBehaviour.track = track.GetComponent<RectTransform>();
            trackBehaviour.SetType(ActuatorType.FAN);
            trackBehaviour.SetPosition(f);
            actuatorTracks.Add(trackBehaviour);
        }
        foreach (HeaterPosition h in Enum.GetValues(typeof(HeaterPosition))) 
        {
            //UnityEngine.Debug.Log(StringExtentions.Pascal(h.ToString()));
            GameObject track = GameObject.Instantiate(ActuatorTrackPrefab, trackHolder);
            GameObject card = GameObject.Instantiate(ActuatorCardPrefab, cardHolder);
            ActuatorTrack trackBehaviour = card.GetComponent<ActuatorTrack>();
            trackBehaviour.track = track.GetComponent<RectTransform>();
            trackBehaviour.SetType(ActuatorType.HEATER);
            trackBehaviour.SetPosition(h);
            actuatorTracks.Add(trackBehaviour);
        }
        foreach(Scent s in Enum.GetValues(typeof(Scent))) 
        {
            //UnityEngine.Debug.Log(StringExtentions.Pascal(s.ToString()));
            if (!s.ToString().ToLower().Contains("newscent")) 
            {
                GameObject track = GameObject.Instantiate(ActuatorTrackPrefab, trackHolder);
                GameObject card = GameObject.Instantiate(ActuatorCardPrefab, cardHolder);
                ActuatorTrack trackBehaviour = card.GetComponent<ActuatorTrack>();
                trackBehaviour.track = track.GetComponent<RectTransform>();
                trackBehaviour.SetType(ActuatorType.SCENT);
                trackBehaviour.SetPosition(s);
                actuatorTracks.Add(trackBehaviour);
            }
        }
        foreach (LightPanelPosition p in Enum.GetValues(typeof(LightPanelPosition)))
        {
            /*foreach (LightPanelChannel c in Enum.GetValues(typeof(LightPanelChannel)))
            {
                //UnityEngine.Debug.Log(StringExtentions.Pascal(p.ToString() + " : " + c.ToString()));
            }*/
            GameObject track = GameObject.Instantiate(ActuatorTrackPrefab, trackHolder);
            GameObject card = GameObject.Instantiate(ActuatorCardPrefab, cardHolder);
            ActuatorTrack trackBehaviour = card.GetComponent<ActuatorTrack>();
            trackBehaviour.track = track.GetComponent<RectTransform>();
            trackBehaviour.SetType(ActuatorType.LIGHT_PANEL);
            trackBehaviour.SetPosition(p);
            actuatorTracks.Add(trackBehaviour);

        }
        {
            GameObject track = GameObject.Instantiate(ActuatorTrackPrefab, trackHolder);
            GameObject card = GameObject.Instantiate(ActuatorCardPrefab, cardHolder);
            ActuatorTrack trackBehaviour = card.GetComponent<ActuatorTrack>();
            trackBehaviour.track = track.GetComponent<RectTransform>();
            trackBehaviour.SetType(ActuatorType.CEILING);
            actuatorTracks.Add(trackBehaviour);
        }

        foreach(ActuatorTrack a in actuatorTracks) 
        {
            a.SetTrackLength(100);
        }

        trackHolder.gameObject.GetComponent<ScaleContainer>().ScaleContent();
        /*foreach (CeilingAnimation a in Enum.GetValues(typeof(CeilingAnimation)))
        {
            //UnityEngine.Debug.Log(StringExtentions.Pascal(a.ToString()));
        }*/
    }
}
