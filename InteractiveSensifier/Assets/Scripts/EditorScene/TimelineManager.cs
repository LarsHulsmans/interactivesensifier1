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


    private void Start()
    {
        SetupActuators();
    }

    private void SetupActuators() 
    {
        foreach (FanPosition f in Enum.GetValues(typeof(FanPosition)))
        {
            UnityEngine.Debug.Log(Pascal(f.ToString()));
        }
        foreach (HeaterPosition h in Enum.GetValues(typeof(HeaterPosition))) 
        {
            UnityEngine.Debug.Log(Pascal(h.ToString()));
        }
        foreach(Scent s in Enum.GetValues(typeof(Scent))) 
        {
            UnityEngine.Debug.Log(Pascal(s.ToString()));
        }
        foreach (LightPanelPosition p in Enum.GetValues(typeof(LightPanelPosition)))
        {
            foreach (LightPanelChannel c in Enum.GetValues(typeof(LightPanelChannel)))
            {
                UnityEngine.Debug.Log(Pascal(p.ToString() + " : " + c.ToString()));
            }
        }
        foreach (CeilingAnimation a in Enum.GetValues(typeof(CeilingAnimation)))
        {
            UnityEngine.Debug.Log(Pascal(a.ToString()));
        }
    }
    
    public string Pascal(string s)
    {
        System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder();

        foreach (char c in s)
        {
            // Replace anything, but letters and digits, with space
            if (!Char.IsLetterOrDigit(c))
            {
                resultBuilder.Append(" ");
            }
            else
            {
                resultBuilder.Append(c);
            }
        }

        string result = resultBuilder.ToString();

        // Make result string all lowercase, because ToTitleCase does not change all uppercase correctly
        result = result.ToLower();

        // Creates a TextInfo based on the "en-US" culture.
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        result = myTI.ToTitleCase(result);//.Replace(" ", String.Empty);

        return result;
    }
}
