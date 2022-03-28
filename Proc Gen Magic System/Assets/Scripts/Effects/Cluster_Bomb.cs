using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cluster Bomb", menuName = "Effects/Cluster Bomb", order = 16)]
public class Cluster_Bomb : Effect
{
    [SerializeField] private int projectileCount;
    [SerializeField] private float projectileScale;
    [SerializeField] private float projectileDistance;

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

        spell.projectileCount = projectileCount;
        spell.projectileScale = projectileScale;
        spell.projectileDistance = projectileDistance;

        spell.activeColors.Add(Color.white);
    }
}

