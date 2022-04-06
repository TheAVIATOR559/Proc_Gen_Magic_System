using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light", menuName = "Effects/Light", order = 7)]
public class Light : Elemental_Effect
{
    [SerializeField] private Texture2D lightNormal;
    [SerializeField] int manaCost;
    [SerializeField] int damageStack;
    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
        spell.projectileHolder.useLight = true;
        spell.projectileHolder.lightDamageStack = damageStack;
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
        mat.SetTexture("_Normal_Texture", lightNormal);

        spell.activeColors.Add(Color.white);
    }
}
