using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice", menuName = "Effects/Ice", order = 4)]
public class Ice : Elemental_Effect
{
    [SerializeField] private Texture2D iceNormal;
    [SerializeField] DOT dot;

    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.AddOnContactDOT(dot.CopyOf());
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

        mat.SetInt("_Add_Normal_Texture", 1);
        mat.SetTexture("_Normal_Texture", iceNormal);

        spell.activeColors.Add(Color.white);
    }
}
