using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dump All Mana", menuName = "Effects/Dump All Mana", order = 22)]
public class Dump_All_Mana : Effect
{
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float rotationSpeed;

    public override void AddGameplayEffect(Spell spell)
    {
        //TODO POPULATE ME
    }

    public override void AddVisualEffect(CircleLocation location, Material mat, Spell spell)
    {
        switch (location)
        {
            case CircleLocation.OUTER:
                mat.SetTexture("_Outer_Texture", circlePart);
                break;
            case CircleLocation.MIDDLE:
                mat.SetTexture("_Middle_Texture", circlePart);
                break;
            case CircleLocation.INNER:
                mat.SetTexture("_Inner_Texture", circlePart);
                break;
        }

        mat.SetFloat("_Pulse_Speed", pulseSpeed);
        mat.SetFloat("_Rotation_Speed", rotationSpeed);

        spell.activeColors.Add(Color.white);
    }
}
