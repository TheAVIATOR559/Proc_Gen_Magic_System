using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Projectile : MonoBehaviour
{
    public bool useArc;
    public float speed;
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;

    private float distanceTravelled;

    private bool useArcDEFAULT = false;
    private float speedDEFAULT = 1;

    private enum State
    {
        NOT_MOVING,
        MOVING,
        CONTACTED,
        ACTIVATED
    }

    [SerializeField] private State currState = State.NOT_MOVING;

    private List<GameObject> OnContactEffects = new List<GameObject>();

    public void Reset()
    {
        transform.position = transform.parent.position;
        useArc = useArcDEFAULT;
        speed = speedDEFAULT;
        OnContactEffects.Clear();
        currState = State.NOT_MOVING;
    }

    public void Rearm()
    {
        transform.position = transform.parent.position;
        distanceTravelled = 0;
        currState = State.NOT_MOVING;
    }

    public void Fire()
    {
        currState = State.MOVING;
    }

    private void Update()
    {
        switch (currState)
        {
            case State.NOT_MOVING:
                //do nothing
                break;
            case State.MOVING:
                //move until hit target
                Move();
                break;
            case State.CONTACTED:
                Activate();
                //activate on contact effects
                break;
            case State.ACTIVATED:
                //hold until activation is complete
                break;
            default:
                Debug.LogError("PROJECTILE STATE FALL THROUGH ERROR");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currState = State.CONTACTED;
    }

    private void Activate()
    {
        currState = State.ACTIVATED;

        foreach(GameObject effect in OnContactEffects)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
        }

        Debug.Log("KABOOM");
        Rearm();
    }

    private void Move()
    {
        if(useArc)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
        }
    }

    public void AddOnContactEffect(GameObject effect)
    {
        OnContactEffects.Add(effect);
    }
}
