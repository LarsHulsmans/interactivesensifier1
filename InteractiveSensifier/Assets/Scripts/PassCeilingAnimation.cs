using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCeilingAnimation : MonoBehaviour
{
    public Sensiks.SDK.Shared.SensiksDataTypes.CeilingAnimation anim;

    public KeyframeBehaviour keyframe;

    public void ButtonPessed() 
    {
        keyframe.SetValueCeiling(anim);
    }
}
