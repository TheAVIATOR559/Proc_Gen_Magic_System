using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weaken Target", menuName = "Effects/Weaken Target", order = 8)]
public class Weaken_Target : Effect
{
    [SerializeField] private Color color;
    [SerializeField] int manaCost;

    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
        //TODO POPULATE ME
    }

    public override void AddVisualEffect(CircleLocation location, Material mat, Spell spell)
    {
        switch (location)
        {
            case CircleLocation.OUTER:
                mat.SetTexture("_Outer_Texture", circlePart);
                mat.SetColor("_Outer_Color", color);
                break;
            case CircleLocation.MIDDLE:
                mat.SetTexture("_Middle_Texture", circlePart);
                mat.SetColor("_Middle_Color", color);
                break;
            case CircleLocation.INNER:
                mat.SetTexture("_Inner_Texture", circlePart);
                mat.SetColor("_Inner_Color", color);
                break;
        }

        spell.activeColors.Add(color);
    }
}
