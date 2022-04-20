using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float speed = 0.01f;

    public void SetText(int amount)
    {
        this.text.text = amount.ToString();
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while(text.color.a >= 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.01f);
            transform.position += (Vector3.up * speed);
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);
    }
}
