using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Effects/Shotgun", order = 12)]
public class Shotgun : Effect
{
    [SerializeField] private int projectileCount;
    [SerializeField] private float projectileScale;
    [SerializeField] private float projectileDistance;
    [SerializeField] int manaCost;
    [SerializeField] int damageIncrease;

    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
        spell.projectileHolder.damage += damageIncrease;
        spell.projectileHolder.useShotgun = true;
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

        spell.projectileCount = projectileCount;
        spell.projectileScale = projectileScale;
        spell.projectileDistance = projectileDistance;
        spell.activeColors.Add(Color.white);
    }
}
