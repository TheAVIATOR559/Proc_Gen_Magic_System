using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] private Material mat;

    [SerializeField] private Gradient gradient;
    [SerializeField] private float gradientSpeed = 1f;

    private GradientColorKey[] colorKeys;
    private GradientAlphaKey[] alphaKeys;

    private bool useEmission = false;

    private void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;
    }

    public void CreateGradient(List<Color> activeColors, float rotationSpeed = 50, bool useEmission = false)
    {
        gradient = new Gradient();
        colorKeys = new GradientColorKey[activeColors.Count];
        alphaKeys = new GradientAlphaKey[activeColors.Count];

        for (int i = 0; i < activeColors.Count; i++)
        {
            colorKeys[i].color = activeColors[i];
            colorKeys[i].time = (float)(i) / (activeColors.Count-1);
            alphaKeys[i].alpha = 0.5f;
            alphaKeys[i].time = (float)(i) / (activeColors.Count-1);
        }

        gradient.SetKeys(colorKeys, alphaKeys);

        this.rotationSpeed = rotationSpeed;
        this.useEmission = useEmission;
        mat.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        mat.color = gradient.Evaluate(Mathf.PingPong(Time.time / gradientSpeed, 1f));

        if(useEmission)
        {
            mat.SetColor("_EmissionColor", gradient.Evaluate(Mathf.PingPong(Time.time / gradientSpeed, 1f)));
        }

    }
}
