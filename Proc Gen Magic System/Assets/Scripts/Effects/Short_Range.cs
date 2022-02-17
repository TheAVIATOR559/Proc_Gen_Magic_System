using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Short Range", menuName = "Effects/Short Range", order = 13)]
public class Short_Range : Effect
{
    [SerializeField] Texture2D extraTexture;
    [SerializeField] Color extraColor;
    public override void AddGameplayEffect()
    {
        //TODO POPULATE ME
    }

    public override void AddVisualEffect(CircleLocation location, Material mat)
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
    }
}
