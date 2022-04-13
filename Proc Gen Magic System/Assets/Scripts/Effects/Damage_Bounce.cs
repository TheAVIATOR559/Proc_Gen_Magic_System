using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Bounce", menuName = "Effects/Damage Bounce", order = 7)]
public class Damage_Bounce : Effect
{
    [SerializeField] private Color color;
    [SerializeField] int manaCost;
    [SerializeField] int bounceCount = 1;

    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
        spell.projectileHolder.useBounce = true;
        spell.projectileHolder.bounceMax += bounceCount;
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
