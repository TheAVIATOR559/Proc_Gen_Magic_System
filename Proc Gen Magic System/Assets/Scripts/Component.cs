using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Construction_Position
{
    POWER,
    TARGET,
    ELEMENT,
    COUNT
}

public enum Elements
{
    NULL,
    FIRE,
    ICE,
    LIGHTNING,
    LIGHT,
    SHADOW
}

[CreateAssetMenu(fileName = "Default", menuName = "Component")]
public class Component : ScriptableObject
{
    public Effect primaryEffect, secondaryEffect;
    public bool usePrimaryEffect = true;
}
