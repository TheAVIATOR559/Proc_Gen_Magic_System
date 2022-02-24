using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.UI;

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

public enum CircleLocation
{
    OUTER,
    MIDDLE,
    INNER
}

[CreateAssetMenu(fileName = "Default", menuName = "Effects/Default", order = 0)]
public class Effect : ScriptableObject
{
    public string effectName;
    public byte ID;
    public Construction_Position constructionPosition;
    public string effectDesc;
    public string gameplayEffectDesc;
    public string visualEffectDesc;
    public List<byte> conflictingEffects = new List<byte>();
    public Texture2D circlePart;

    public virtual void AddGameplayEffect()
    {
        //POPULATED IN CHILDREN
    }

    public virtual void AddVisualEffect(CircleLocation location, Material mat, Spell spell)
    {
        //POPULATED IN CHILDREN
    }
}
