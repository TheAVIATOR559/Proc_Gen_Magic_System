using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decrease Mana Cost", menuName = "Effects/Decrease Mana Cost", order = 20)]
public class Decrease_Mana_Cost : Effect
{
    [SerializeField] private float minOpacity, maxOpacity;
    [SerializeField] int manaCost;
    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
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

        mat.SetFloat("_Min_Opacity", minOpacity);
        mat.SetFloat("_Max_Opacity", maxOpacity);

        spell.activeColors.Add(Color.white);
    }
}
