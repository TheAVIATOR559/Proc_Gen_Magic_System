using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Projectile : MonoBehaviour
{
    public bool canFire = false;

    public bool useArc;
    public bool useCluster;
    public bool useManaDump;
    public bool useLight;
    public bool useShadow;
    public bool useBounce;
    public bool useShotgun;
    public int bounceMax;
    public float speed;
    public int damage;
    public float castDelay;
    public int lightDamageStack;
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;

    private float distanceTravelled;

    private bool useArcDEFAULT = false;
    private bool useClusterDEFAULT = false;
    private bool useManaDumpDEFAULT = false;
    private bool useLightDEFAULT = false;
    private bool useShadowDEFAULT = false;
    private bool useBounceDEFAULT = false;
    private bool useShotgunDEFAULT = false;
    private float speedDEFAULT = 1;
    private int damageDEFAULT = 10;
    private float castDelayDEFAULT = 2f;
    private int bounceMaxDEFAULT = 0;
    private enum State
    {
        NOT_MOVING,
        MOVING,
        CONTACTED,
        ACTIVATED
    }

    [SerializeField] private State currState = State.NOT_MOVING;

    private List<GameObject> OnContactEffects = new List<GameObject>();
    [SerializeField] private List<DOT> OnContactDots = new List<DOT>();
    [SerializeField] private GameObject defaultOnContactEffect;

    private Target target;

    public int currentMana;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private int manaRegenRate = 10;
    public int manaCost;

    [SerializeField] private int bounceCount = 0;
    private float distanceTraveled = 0;

    public void Reset()
    {
        transform.position = transform.parent.position;
        useArc = useArcDEFAULT;
        useCluster = useClusterDEFAULT;
        speed = speedDEFAULT;
        damage = damageDEFAULT;
        castDelay = castDelayDEFAULT;
        useManaDump = useManaDumpDEFAULT;
        useLight = useLightDEFAULT;
        useShadow = useShadowDEFAULT;
        useBounce = useBounceDEFAULT;
        currentMana = maxMana;
        bounceMax = bounceMaxDEFAULT;
        useShotgun = useShotgunDEFAULT;
        canFire = true;
        bounceCount = 0;
        distanceTraveled = 0;
        manaCost = 0;
        OnContactEffects.Clear();
        OnContactDots.Clear();
        currState = State.NOT_MOVING;
    }

    public void Rearm()//todo need to reset children position and states after cluster activation
    {
        transform.position = transform.parent.position;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        distanceTravelled = 0;
        currState = State.NOT_MOVING;
        bounceCount = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.GetComponent<Projectile_Rotation>().Reset();
        }
    }

    public void Fire()
    {
        if(useManaDump)
        {
            currentMana = 0;
            manaCost = maxMana;
        }
        else
        {
            currentMana -= manaCost;
        }
        currState = State.MOVING;
        canFire = false;
    }

    public void TryFire()
    {
        if (canFire)
        {
            if(currentMana >= manaCost)
            {
                Rearm();
                Fire();
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }
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
        target = other.GetComponent<Target>();
    }

    private void Activate()
    {
        currState = State.ACTIVATED;

        if(OnContactEffects.Count == 0)
        {
            Instantiate(defaultOnContactEffect, transform.position, Quaternion.identity);
        }
        else
        {
            foreach (GameObject effect in OnContactEffects)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
            }
        }

        foreach(DOT dot in OnContactDots)
        {
            Target.AddDOT(dot);
        }

        if(useCluster)
        {
            Scatter();
        }
        else
        {
            //Debug.Log("PROC");
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        if(useShadow)
        {
            target.Health -= damage + (int)(damage * (1 - ((target.MaxHealth - target.Health) / 100f)));
        }
        else if(useShotgun)
        {
            distanceTraveled = Vector3.Magnitude(transform.position - transform.parent.position);
            target.Health -= (int)(damage * (1 - (damage / distanceTraveled / 100f)));
            //Debug.Log(distanceTraveled + "::" + (1 - (damage / distanceTraveled / 100f)) + "::" + (damage * (1 - (damage / distanceTraveled / 100f))));
        }
        else
        {
            target.Health -= damage;
        }

        if(useLight)
        {
            if(target.Health <= 0)
            {
                damage += lightDamageStack;
            }
        }

        //Debug.Log("KABOOM");

        if(useBounce && bounceCount < bounceMax)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            Debug.Log((transform.parent.position.x - transform.position.x) / 2f + "::" + transform.position.x + "::" + transform.parent.position.x);

            //move away from target
            transform.Translate((transform.parent.position.x - transform.position.x) / 2f, 0, 0, Space.World);
            //move back toward target
            currState = State.MOVING;
            bounceCount++;
        }
        else
        {
            StartCoroutine(TickCastDelay());
        }
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

    public void AddOnContactDOT(DOT dot)
    {
        OnContactDots.Add(dot);
    }

    private void Scatter()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Projectile_Rotation>().SplitFromParent();
        }
    }

    private IEnumerator TickCastDelay()
    {
        yield return new WaitForSeconds(castDelay);

        
        canFire = true;
        Rearm();
    }

    private void OnEnable ()
    {
        StartCoroutine(ManaRegen());
    }

    private IEnumerator ManaRegen()
    {
        while(true)
        {
            //Debug.Log("REGENING MANA");
            if (currentMana < maxMana)
            {
                currentMana += manaRegenRate;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
