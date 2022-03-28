using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single Slow Shot", menuName = "Effects/Single Slow Shot", order = 14)]
public class Single_Slow_Shot : Effect
{
    [SerializeField] private float slowAmount;
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

        mat.SetFloat("_Rotation_Speed", slowAmount);

        spell.activeColors.Add(Color.white);
    }
}