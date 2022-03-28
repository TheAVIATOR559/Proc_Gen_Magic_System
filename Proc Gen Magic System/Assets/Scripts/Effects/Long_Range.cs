using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Long Range", menuName = "Effects/Long Range", order = 17)]
public class Long_Range : Effect
{
    [SerializeField] Texture2D extraTexture;
    [SerializeField] Color extraColor;
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

        mat.SetTexture("_Extra_Texture", extraTexture);
        mat.SetColor("_Extra_Texture_Color", extraColor);
        mat.SetInt("_Add_Extra_Texture", 1);

        spell.activeColors.Add(extraColor);
    }
}
