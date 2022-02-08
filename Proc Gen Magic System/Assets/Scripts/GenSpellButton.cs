using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenSpellButton : MonoBehaviour
{

    public void OnClick()
    {
        Builder.GenerateNewSpell();
    }
}
