using System.Collections;
using System.Collections.Generic;


public interface IKeyframe 
{
    float timestamp { get; set; }
    Sensiks.SDK.Shared.SensiksDataTypes.ActuatorType actuatorType { get; set; }

}
