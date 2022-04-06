using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollider : MonoBehaviour
{
    [SerializeField] float maxColliderSize = 3f;
    [SerializeField] SphereCollider collider;
    [SerializeField] List<Target> targets = new List<Target>();

    private void Start()
    {
        StartCoroutine(IncreaseColliderSize());
    }

    private IEnumerator IncreaseColliderSize()
    {
        for(float f = 0f; f < maxColliderSize; f += 0.1f)
        {
            collider.radius = f;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Target>())
        {
            targets.Add(other.GetComponent<Target>());
        }
    }

    private void OnDestroy()
    {
        foreach(Target target in targets)
        {
            target.Health -= 10;
        }
    }
}
