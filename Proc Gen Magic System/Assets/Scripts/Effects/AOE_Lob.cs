using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE Lob", menuName = "Effects/AOE Lob", order = 15)]
public class AOE_Lob : Effect
{
    [SerializeField] Texture2D extraTexture;
    [SerializeField] Color extraColor;
    [SerializeField] GameObject arcExplosion;
    [SerializeField] int manaCost;
    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
        spell.projectileHolder.useArc = true;
        spell.projectileHolder.AddOnContactEffect(arcExplosion);
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

        mat.SetTexture("_Extra_Texture", extraTexture);
        mat.SetColor("_Extra_Texture_Color", extraColor);
        mat.SetInt("_Add_Extra_Texture", 1);

        spell.activeColors.Add(extraColor);
    }
}

