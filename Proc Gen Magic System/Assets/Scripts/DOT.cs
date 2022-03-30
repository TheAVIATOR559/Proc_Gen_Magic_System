using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DOT
{
    public enum DOTType
    {
        FIRE,
        ICE,
        SHOCK,
        BLIND
    }

    public DOTType Type;
    public int DamagePerTick;
    public int Duration;
    public GameObject conncectedEffect;

    public Target Target;

    public DOT(DOTType type, int damage, int duration)
    {
        Type = type;
        DamagePerTick = damage;
        Duration = duration;
        Target = Target.instance;
    }
    
    ~DOT()
    {

    }

    public void MergeDOT(DOT dot)
    {
        if(dot.Type == this.Type)
        {
            this.Duration += dot.Duration;
            this.DamagePerTick += (dot.DamagePerTick / 2);
        }
    }

    public DOT CopyOf()
    {
        DOT newDot = new DOT(Type, DamagePerTick, Duration);
        newDot.conncectedEffect = this.conncectedEffect;
        return newDot;
    }
}
