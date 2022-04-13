using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSpellButton : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Button thisButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile.canFire)
        {
            thisButton.interactable = true;
        }
        else
        {
            thisButton.interactable = false;
        }
    }

    public void Click()
    {
        projectile.TryFire();
    }
}
