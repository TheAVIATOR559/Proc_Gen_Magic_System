using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shadow", menuName = "Effects/Shadow", order = 9)]
public class Shadow : Elemental_Effect
{
    [SerializeField] private Texture2D shadowNormal;

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

        mat.SetInt("_Add_Normal_Texture", 1);
        mat.SetTexture("_Normal_Texture", shadowNormal);

        spell.activeColors.Add(Color.white);
    }
}
