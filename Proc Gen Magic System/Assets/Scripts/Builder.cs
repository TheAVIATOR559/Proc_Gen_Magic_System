using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject componentPanelPrefab;

    [SerializeField] private TMP_Text spellNameText;
    [SerializeField] private Image spellCircleImage;
    [SerializeField] private Transform componentHolder;
    [SerializeField] private List<Component> components;

    private static Builder Instance;

    public string spellName;
    public List<Component> spellComps;

    public static void GenerateNewSpell()
    {
        Instance.DestroyCurrentSpell();
        Instance.BuildSpell();
    }

    private void DestroyCurrentSpell()
    {
        spellComps.Clear();
        spellName = "";

        //reset all UI info
        spellNameText.text = "##NAME##";
        //spellCircleImage I HAVE NO IDEA WHAT TO DO HERE
        foreach(Transform child in componentHolder)
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

        spellComps.Add(Instantiate(components[Random.Range(0, components.Count)]));
        Construction_Position prevPosition = spellComps[0].primaryEffect.constructionPosition;

        for(int i = 0; i < 2; i++)
        {
            spellComps.Add(Instantiate(components[Random.Range(0, components.Count)]));

            if (spellComps[spellComps.Count - 1].primaryEffect.constructionPosition == prevPosition)
            {
                spellComps[spellComps.Count - 1].usePrimaryEffect = false;
                prevPosition = spellComps[spellComps.Count - 1].secondaryEffect.constructionPosition;
            }
            else
            {
                prevPosition = spellComps[spellComps.Count - 1].primaryEffect.constructionPosition;
            }
        }

        //spellComps.Add(components[0]);
        //spellComps.Add(components[1]);
        //spellComps.Add(components[2]);

        //generate spell circle
        spellCircleImage.fillAmount = 1f;

        //generate name

        foreach (Component comp in spellComps)
        {
            DisplayComponent(comp);
        }
    }

    private void DisplayComponent(Component comp)
    {
        Color darkenedEffectColor = new Color(0.3921569f, 0.3921569f, 0.3921569f, 1f);

        GameObject newPanel = Instantiate(componentPanelPrefab, componentHolder);

        newPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = comp.name;

        newPanel.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = comp.primaryEffect.effectName;
        newPanel.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = comp.primaryEffect.constructionPosition.ToString();
        newPanel.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = comp.primaryEffect.effectDesc;
        newPanel.transform.GetChild(1).GetChild(3).GetComponent<TMP_Text>().text = comp.primaryEffect.gameplayEffectDesc;
        newPanel.transform.GetChild(1).GetChild(4).GetComponent<TMP_Text>().text = comp.primaryEffect.visualEffectDesc;

        newPanel.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = comp.secondaryEffect.effectName;
        newPanel.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = comp.secondaryEffect.constructionPosition.ToString();
        newPanel.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = comp.secondaryEffect.effectDesc;
        newPanel.transform.GetChild(2).GetChild(3).GetComponent<TMP_Text>().text = comp.secondaryEffect.gameplayEffectDesc;
        newPanel.transform.GetChild(2).GetChild(4).GetComponent<TMP_Text>().text = comp.secondaryEffect.visualEffectDesc;

        if(comp.usePrimaryEffect)
        {
            //darken secondary effect
            newPanel.transform.GetChild(2).GetComponent<Image>().color = darkenedEffectColor;
            newPanel.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(2).GetChild(3).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(2).GetChild(4).GetComponent<TMP_Text>().color = darkenedEffectColor;
        }
        else
        {
            //darken primary effect
            newPanel.transform.GetChild(1).GetComponent<Image>().color = darkenedEffectColor;
            newPanel.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(1).GetChild(3).GetComponent<TMP_Text>().color = darkenedEffectColor;
            newPanel.transform.GetChild(1).GetChild(4).GetComponent<TMP_Text>().color = darkenedEffectColor;
        }
    }

    public void Awake()
    {
        Instance = this;
        spellCircleImage.fillAmount = 0f;
    }
}
