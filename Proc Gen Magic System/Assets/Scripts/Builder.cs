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

    private static Builder Instance;

    public string spellName;
    public List<Effect> spellEffects;

    public static void GenerateNewSpell()
    {
        Instance.DestroyCurrentSpell();
        Instance.BuildSpell();
    }

    private void DestroyCurrentSpell()
    {
        spellEffects.Clear();
        spellName = "";

        //reset all UI info
        spellNameText.text = "##NAME##";
        //spellCircleImage I HAVE NO IDEA WHAT TO DO HERE
        foreach(Transform child in effectHolder)
        {
            Destroy(child.gameObject);
        }

        //reset all gameplay effect

        //reset all visual effect
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
            }
        }

        //spellComps.Add(components[0]);
        //spellComps.Add(components[1]);
        //spellComps.Add(components[2]);

        //generate spell circle
        spellCircleImage.SetActive(true);

        //generate name

        foreach (Effect effect in spellEffects)
        {
            DisplayEffect(effect);
        }

        //generate gameplay effects

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

        //if construction posistion is element also list its element

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
    }
}
