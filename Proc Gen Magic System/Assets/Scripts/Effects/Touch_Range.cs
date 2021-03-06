using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Touch Range", menuName = "Effects/Touch Range", order = 11)]
public class Touch_Range : Effect
{
    [SerializeField] Texture2D extraTexture;
    [SerializeField] Color extraColor;
    [SerializeField] int manaCost;
    [SerializeField] int speedIncrease;
    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
        spell.projectileHolder.speed = speedIncrease;
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