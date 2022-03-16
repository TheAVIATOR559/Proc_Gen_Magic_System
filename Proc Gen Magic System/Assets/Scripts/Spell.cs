using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spell : MonoBehaviour
{
    [SerializeField] private int projectileCountDefault = 1;
    [SerializeField] private float projectileScaleDefault = 1;
    [SerializeField] private float projectileDistanceDefault = 0f;
    [SerializeField] private float projectileRotationDefault = 50f;
    [SerializeField] private Transform projectileHolder;

    [SerializeField] private GameObject projectilePrefab;

    public int projectileCount;
    public float projectileScale;
    public float projectileDistance;
    public float projectileRotation;
    public bool useEmission = false;
    public List<Color> activeColors = new List<Color>();

    private void Awake()
    {
        projectileCount = projectileCountDefault;
        projectileScale = projectileScaleDefault;
        projectileDistance = projectileDistanceDefault;
        projectileRotation = projectileRotationDefault;
        useEmission = false;
        projectileHolder.gameObject.SetActive(false);
    }

    public void ResetSpell()
    {
        foreach(Transform child in projectileHolder)
        {
            Destroy(child.gameObject);
        }

        projectileCount = projectileCountDefault;
        projectileScale = projectileScaleDefault;
        projectileDistance = projectileDistanceDefault;
        projectileRotation = projectileRotationDefault;
        useEmission = false;
        activeColors.Clear();
        projectileHolder.gameObject.SetActive(false);
    }

    public void EnableSpell()
    {
        projectileHolder.gameObject.SetActive(true);

        //remove duplicates
        activeColors = activeColors.Distinct().ToList();

        float angleOffset = (360f / projectileCount) * Mathf.Deg2Rad;

        for(int i = 0; i < projectileCount; i++)
        {
            GameObject newProj = Instantiate(projectilePrefab, projectileHolder);
            newProj.transform.localScale = new Vector3(projectileScale, projectileScale, projectileScale);

            newProj.transform.Translate(projectileDistance * Mathf.Sin(i * angleOffset), projectileDistance * Mathf.Cos(i * angleOffset), 0);
            newProj.GetComponent<Projectile_Rotation>().CreateGradient(activeColors, projectileRotation, useEmission);
        }
    }

    public void Fire()
    {
        //TODO PICK UP HERE
        //StopAllCoroutines();
        projectileHolder.position = transform.position;
        //StartCoroutine(MoveToTarget());
    }

    [SerializeField] private Transform target;
    private IEnumerator MoveToTarget()
    {
        while(Vector3.Distance(projectileHolder.position, target.position) > 0.05f)
        {
            projectileHolder.position = new Vector3(1, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }
}
