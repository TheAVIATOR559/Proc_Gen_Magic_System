using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decrease Cast Delay", menuName = "Effects/Decrease Cast Delay", order = 21)]
public class Decrease_Cast_Delay : Effect
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float castDelayReduction;
    [SerializeField] int manaCost;

    public override void AddGameplayEffect(Spell spell)
    {
        spell.projectileHolder.manaCost += manaCost;
        spell.projectileHolder.castDelay -= castDelayReduction;
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

        spell.activeColors.Add(Color.white);
        spell.projectileRotation = rotationSpeed;
    }
}
