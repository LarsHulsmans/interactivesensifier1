using Sensiks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupExperienceButtons : MonoBehaviour
{
    private SavesManager saves = null;

    public Transform experiencesParent;

    public GameObject experiencePrefab;

    private void Start()
    {
        StartCoroutine(CheckForExperiences());
    }

    IEnumerator CheckForExperiences() 
    {   
        while(saves == null) 
        {
            try 
            {
                saves = SavesManager.Instance;
                SetupExperiences();
            }
            catch 
            {
                
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetupExperiences()
    {
        foreach(SerializableSavedata dat in saves.savedata) 
        {
            Debug.Log("test");
            GameObject temp = Instantiate(experiencePrefab, experiencesParent);
            temp.GetComponent<ExperienceItem>().Savedata = dat;
        }
    }
}
