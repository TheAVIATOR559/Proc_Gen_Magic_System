using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject EffectPanelPrefab;

    [SerializeField] private TMP_Text spellNameText;
    [SerializeField] private GameObject spellCircleImage;
    [SerializeField] private Transform effectHolder;
    [SerializeField] private List<Effect> effects;
    [SerializeField] private Spell spell;

    private static Builder Instance;

    public string spellName;
    public List<Effect> spellEffects;

    private Material spellCircleMat;
    private float rotationSpeed, pulseSpeed, minOpacity, maxOpacity;

    public static void GenerateNewSpell()
    {
        Instance.DestroyCurrentSpell();
        Instance.BuildSpell();
    }

    private void DestroyCurrentSpell()
    {
        spellEffects.Clear();
        spellName = "";
        spell.ResetSpell();

        //reset all UI info
        spellNameText.text = "##NAME##";
        //spellCircleImage I HAVE NO IDEA WHAT TO DO HERE
        foreach(Transform child in effectHolder)
        {
            Destroy(child.gameObject);
        }

        //reset all gameplay effect

        //reset all visual effect
        spellCircleMat.SetFloat("_Rotation_Speed", rotationSpeed);
        spellCircleMat.SetFloat("_Pulse_Speed", pulseSpeed);
        spellCircleMat.SetFloat("_Min_Opacity", minOpacity);
        spellCircleMat.SetFloat("_Max_Opacity", maxOpacity);
        spellCircleMat.SetColor("_Outer_Color", Color.white);
        spellCircleMat.SetColor("_Middle_Color", Color.white);
        spellCircleMat.SetColor("_Inner_Color", Color.white);
        spellCircleMat.SetInt("_Use_Blinking_Effect", 0);
        spellCircleMat.SetColor("_Extra_Texture_Color", Color.white);
        spellCircleMat.SetInt("_Add_Extra_Texture", 0);
        spellCircleMat.SetInt("_Add_Normal_Texture", 0);
    }

    private void BuildSpell()
    {
        //pick components

        /* if there is no prev comp, skip
         * 
         * pick new comp
         * if new comp has same primary const pos as prev, use secondary effect
         */

        spellEffects.Add(Instantiate(effects[Random.Range(0, effects.Count)]));
        List<byte> knownConflicts = new List<byte>();
        knownConflicts.AddRange(spellEffects[0].conflictingEffects);

        while (spellEffects.Count < 3)
        {
            int selectedComp = Random.Range(0, effects.Count);

            if (!knownConflicts.Contains(effects[selectedComp].ID))
            {
                spellEffects.Add(effects[selectedComp]);
                knownConflicts.AddRange(effects[selectedComp].conflictingEffects);
            }
        }

        //generate spell circle
        spellCircleImage.SetActive(true);
        spellEffects[0].AddVisualEffect(CircleLocation.OUTER, spellCircleMat, spell);
        spellEffects[1].AddVisualEffect(CircleLocation.MIDDLE, spellCircleMat, spell);
        spellEffects[2].AddVisualEffect(CircleLocation.INNER, spellCircleMat, spell);

        //generate name

        foreach (Effect effect in spellEffects)
        {
            DisplayEffect(effect);
        }

        //generate gameplay effects


        spell.EnableSpell();
    }

    private void DisplayEffect(Effect comp)
    {
        GameObject newPanel = Instantiate(EffectPanelPrefab, effectHolder);

        newPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = comp.effectName;
        newPanel.transform.GetChild(1).GetComponent<TMP_Text>().text += comp.ID.ToString();
        newPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = comp.constructionPosition.ToString();
        newPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = comp.effectDesc;
        newPanel.transform.GetChild(4).GetComponent<TMP_Text>().text = comp.gameplayEffectDesc;
        newPanel.transform.GetChild(5).GetComponent<TMP_Text>().text = comp.visualEffectDesc;

        //TODO if construction posistion is element also list its element

        if(comp.conflictingEffects.Count > 0)
        {
            foreach(byte value in comp.conflictingEffects)
            {
                newPanel.transform.GetChild(6).GetComponent<TMP_Text>().text += value + ", ";
            }
        }
        else
        {
            newPanel.transform.GetChild(6).GetComponent<TMP_Text>().text += "None";
        }

        /* 0 is name
         * 1 is id #
         * 2 is construction position
         * 3 is effect desc
         * 4 is gameplay effect
         * 5 is visual effect
         * 6 is effect conflicts
         */
    }

    public void Awake()
    {
        Instance = this;
        spellCircleImage.SetActive(false);
        spellCircleMat = spellCircleImage.GetComponent<Renderer>().material;
        rotationSpeed = spellCircleMat.GetFloat("_Rotation_Speed");
        pulseSpeed = spellCircleMat.GetFloat("_Pulse_Speed");
        minOpacity = spellCircleMat.GetFloat("_Min_Opacity");
        maxOpacity = spellCircleMat.GetFloat("_Max_Opacity");
    }
}
