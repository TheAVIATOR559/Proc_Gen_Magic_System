using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] private Material mat;

    [SerializeField] private Gradient gradient;
    [SerializeField] private float gradientSpeed = 1f;

    [SerializeField] Rigidbody rigbod;
    [SerializeField] SphereCollider coll;

    private GradientColorKey[] colorKeys;
    private GradientAlphaKey[] alphaKeys;

    private bool useEmission = false;

    private void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;
    }

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
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

    public void SplitFromParent()
    {
        coll.enabled = true;
        rigbod.useGravity = true;
        rigbod.AddForce((transform.position - transform.parent.position).normalized * 10, ForceMode.Impulse);
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

    public void Reset()
    {
        rigbod.velocity = Vector3.zero;
        rigbod.angularVelocity = Vector3.zero;
        transform.localPosition = startPos;
        rigbod.useGravity = false;
        coll.enabled = false;
    }
}
