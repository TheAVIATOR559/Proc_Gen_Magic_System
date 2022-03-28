using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hitscan Shot", menuName = "Effects/Hitscan Shot", order = 18)]
public class Hitscan_Shot : Effect
{
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float minOpacity;
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
        mat.SetFloat("_Min_Opacity", minOpacity);
        mat.SetInt("_Use_Blinking_Effect", 1);

        spell.activeColors.Add(Color.white);
    }
}