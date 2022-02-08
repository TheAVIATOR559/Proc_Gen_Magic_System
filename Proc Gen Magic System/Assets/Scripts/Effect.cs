using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Default", menuName = "Effects/Default", order = 0)]
public class Effect : ScriptableObject
{
    public string effectName;
    public Construction_Position constructionPosition;
    public string effectDesc;
    public string gameplayEffectDesc;
    public string visualEffectDesc;

    public virtual void AddGameplayEffect()
    {
        //POPULATED IN CHILDREN
    }

    public virtual void AddVisualEffect()
    {
        //POPULATED IN CHILDREN
    }
}
