using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class OnscreenKeyboard : MonoBehaviour
{
    InputField selected;

    EventSystem system;

    private bool keyboardOpen = false;

    private RectTransform trans;

    private void Start()
    {
        trans = GetComponent<RectTransform>();
    }

    private void Update()
    {
        system = EventSystem.current;
        try 
        {
            if(system.currentSelectedGameObject.GetComponent<InputField>() != null) 
            {
                selected = system.currentSelectedGameObject.GetComponent<InputField>();
                if(keyboardOpen == false) 
                {
                    OpenKeyboard();
                }
            }
        }
        catch { }
    }

    public void TypeCharacter(string character) 
    {
        if(selected != null) 
        {
            if (character == "backspace")
            {
                selected.text = selected.text.Substring(0, selected.text.Length - 1);
            }
            else if(character == "enter") 
            {
                
            }
            else 
            {
                selected.text += character;
            }
        } 
    }

    public void CloseKeyboard() 
    {
        //todo: actual closing logic
        trans.sizeDelta = new Vector2(0,0);
        keyboardOpen = false;
    }

    public void OpenKeyboard() 
    {
        //todo: actual opening logic
        trans.sizeDelta = new Vector2(0, 500);
        keyboardOpen = true;
    }
}