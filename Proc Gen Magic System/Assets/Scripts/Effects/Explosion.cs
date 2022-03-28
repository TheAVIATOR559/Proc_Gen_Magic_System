using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "Effects/Explosion", order = 3)]
public class Explosion : Effect
{
    [SerializeField] private Color color;
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
