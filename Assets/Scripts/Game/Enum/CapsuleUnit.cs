using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CapsuleUnit : Enum, DestroydObject, INavigateObject
{
    private NavMeshAgent Agent;
    public Enum MyTarget;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    [SerializeField]
    private float hp;
    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {
           
        }
    }

    [SerializeField]
    private float maxHp;
    public float MaxHp
    {
        get
        {
            return maxHp;
        }

        set
        {
            
        }
    }

    public void Translate(Vector3 pos)
    {
        Agent.destination = pos;
    }


    public float Speed
    {
        get { return Agent.speed; }
        set { Agent.speed = value; }
    }

    private void OnTriggerStay(Collider other)
    {
        if(MyTarget != null)
            return;
        if (!other.isTrigger)
        {
            var _enum = other.GetComponent<Enum>();
            if (_enum && _enum.Command != Command)
                MyTarget = _enum;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == MyTarget.gameObject)
            MyTarget = null;
    }
}
