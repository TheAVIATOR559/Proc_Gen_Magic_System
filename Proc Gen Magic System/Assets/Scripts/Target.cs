using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int Health
    {
        get
        {
            return m_health;
        }
        set
        {
            //Debug.Log(m_health);
            if(m_health <= 0)
            {
                m_health = 0;
                Debug.Log("Blargh, Im dead :: " + m_health);
            }
            else
            {
                m_health = value;
                //Debug.Log("Oww, that hurt :: " + m_health);
            }
        }
    }

    public float Accuracy
    {
        get
        {
            return m_accuracy;
        }
        set
        {
            if(m_accuracy < 0f)
            {
                m_accuracy = 0f;
            }
            else if(m_accuracy > 1f)
            {
                m_accuracy = 1f;
            }
            else
            {
                m_accuracy = value;
            }
        }
    }

    public float MoveSpeed
    {
        get
        {
            return m_moveSpeed;
        }
        set
        {
            if (m_moveSpeed < 0f)
            {
                m_moveSpeed = 0f;
            }
            else
            {
                m_moveSpeed = value;
            }
        }
    }

    public int Damage
    {
        get
        {
            return m_damage;
        }
        set
        {
            if (m_damage < 0)
            {
                m_damage = 0;
            }
            else
            {
                m_damage = value;
            }
        }
    }

    private int m_health = 100;
    private float m_accuracy = 0.8f;
    private float m_moveSpeed = 1f;
    private int m_damage = 10;

    [SerializeField] private List<DOT> activeDots = new List<DOT>();
    public static Target instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(TickDots());
    }

    public static void AddDOT(DOT dot)
    {
        int index = instance.activeDots.FindIndex(d => d.Type == dot.Type);

        Debug.Log(dot.DamagePerTick + "::" + dot.Duration);

        if(index >= 0)
        {
            instance.activeDots[index].MergeDOT(dot);
        }
        else
        {
            instance.activeDots.Add(dot.CopyOf());
        }
    }

    private IEnumerator TickDots()
    {
        while(true)
        {
            for(int i = 0; i < activeDots.Count; i++)
            {
                if(activeDots[i].Duration <= 0)
                {
                    activeDots.RemoveAt(i);
                }
                else
                {
                    Health -= activeDots[i].DamagePerTick;
                    activeDots[i].Duration--;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
