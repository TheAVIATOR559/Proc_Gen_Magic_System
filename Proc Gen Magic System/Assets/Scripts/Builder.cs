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
    [SerializeField] private Renderer spellCircleBeta;
    [SerializeField] private Spell spell;
    [SerializeField] private Spell spellBeta;
    [SerializeField] private Transform effectHolder;
    [SerializeField] private List<Effect> effects;

    private static Builder Instance;

    public string spellName;
    public List<Effect> spellEffects;

    private Material spellCircleMat;
    private Material spellCircleBetaMat;
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
        spellBeta.ResetSpell();

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

        spellCircleBetaMat.SetFloat("_Rotation_Speed", rotationSpeed);
        spellCircleBetaMat.SetFloat("_Pulse_Speed", pulseSpeed);
        spellCircleBetaMat.SetFloat("_Min_Opacity", minOpacity);
        spellCircleBetaMat.SetFloat("_Max_Opacity", maxOpacity);
        spellCircleBetaMat.SetColor("_Outer_Color", Color.white);
        spellCircleBetaMat.SetColor("_Middle_Color", Color.white);
        spellCircleBetaMat.SetColor("_Inner_Color", Color.white);
        spellCircleBetaMat.SetInt("_Use_Blinking_Effect", 0);
        spellCircleBetaMat.SetColor("_Extra_Texture_Color", Color.white);
        spellCircleBetaMat.SetInt("_Add_Extra_Texture", 0);
        spellCircleBetaMat.SetInt("_Add_Normal_Texture", 0);
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
        spellCircleBeta.gameObject.SetActive(true);
        spellEffects[0].AddVisualEffect(CircleLocation.OUTER, spellCircleMat, spell);
        spellEffects[1].AddVisualEffect(CircleLocation.MIDDLE, spellCircleMat, spell);
        spellEffects[2].AddVisualEffect(CircleLocation.INNER, spellCircleMat, spell);
        spellEffects[0].AddVisualEffect(CircleLocation.OUTER, spellCircleBetaMat, spellBeta);
        spellEffects[1].AddVisualEffect(CircleLocation.MIDDLE, spellCircleBetaMat, spellBeta);
        spellEffects[2].AddVisualEffect(CircleLocation.INNER, spellCircleBetaMat, spellBeta);

        //generate name
        spellName = spellEffects[0].noun;

        if(spellEffects[1].adjectivePosition < spellEffects[2].adjectivePosition)
        {
            spellName = spellName.Insert(0, spellEffects[2].adjective + " ");
            spellName = spellName.Insert(0, spellEffects[1].adjective + " ");
            //Debug.Log(spellEffects[1].adjectivePosition + "(" + (int)spellEffects[1].adjectivePosition + ")::" + spellEffects[2].adjectivePosition + "(" + (int)spellEffects[2].adjectivePosition + ")");
        }
        else if(spellEffects[1].ID == spellEffects[2].ID)
        {
            spellName = spellName.Insert(0, spellEffects[1].adjective + " ");
            //Debug.Log(spellEffects[1].adjectivePosition + "(" + (int)spellEffects[1].adjectivePosition + ")::" + spellEffects[2].adjectivePosition + "(" + (int)spellEffects[2].adjectivePosition + ")");
        }
        else
        {
            spellName = spellName.Insert(0, spellEffects[1].adjective + " ");
            spellName = spellName.Insert(0, spellEffects[2].adjective + " ");
            //Debug.Log(spellEffects[2].adjectivePosition + "(" + (int)spellEffects[2].adjectivePosition + ")::" + spellEffects[1].adjectivePosition + "(" + (int)spellEffects[1].adjectivePosition + ")");
        }

        spellNameText.text = spellName;

        foreach (Effect effect in spellEffects)
        {
            DisplayEffect(effect);
        }

        //generate gameplay effects
        spellEffects[0].AddGameplayEffect(spellBeta);
        spellEffects[1].AddGameplayEffect(spellBeta);
        spellEffects[2].AddGameplayEffect(spellBeta);

        spell.EnableSpell();
        spellBeta.EnableSpell();

        //spellBeta.Fire();
    }

    private void DisplayEffect(Effect comp)
    {
        GameObject newPanel = Instantiate(EffectPanelPrefab, effectHolder);

        newPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = comp.effectName;
        newPanel.transform.GetChild(1).GetComponent<TMP_Text>().text += comp.ID.ToString();
        newPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = comp.constructionPosition.ToString();
        newPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = comp.gameplayEffectDesc;
        newPanel.transform.GetChild(4).GetComponent<TMP_Text>().text = comp.visualEffectDesc;

        //TODO if construction posistion is element also list its element

        if(comp.conflictingEffects.Count > 0)
        {
            foreach(byte value in comp.conflictingEffects)
            {
                newPanel.transform.GetChild(5).GetComponent<TMP_Text>().text += value + ", ";
            }
        }
        else
        {
            newPanel.transform.GetChild(5).GetComponent<TMP_Text>().text += "None";
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

    public void RunTest()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        Debug.Log("TEST START");
        for (int i = 0; i < 10000; i++)
        {
            Debug.Log(i);
            GenerateNewSpell();
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("TEST END");
    }

    public void Awake()
    {
        Instance = this;
        spellCircleImage.SetActive(false);
        spellCircleBeta.gameObject.SetActive(false);
        spellCircleMat = spellCircleImage.GetComponent<Renderer>().material;
        spellCircleBetaMat = spellCircleBeta.material;
        rotationSpeed = spellCircleMat.GetFloat("_Rotation_Speed");
        pulseSpeed = spellCircleMat.GetFloat("_Pulse_Speed");
        minOpacity = spellCircleMat.GetFloat("_Min_Opacity");
        maxOpacity = spellCircleMat.GetFloat("_Max_Opacity");
    }
}
