using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable 
{
    bool IsAggroed { get; set; }
    bool IsWithinStrikingDistance { get; set; }
    void SetAggroedStatus(bool isAggoed);
    void SetStrikingDistanceBool(bool isWithinStrikingDistance);
}
