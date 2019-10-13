using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateController : MonoBehaviour, IStateController 
{
    public State currentState;
    public NavMeshAgent navMeshAgent;
    public List<Transform> wayPointList;
    public Transform eyes;
    public float sightRadius = 2.0f;
    public float maxSight = 5.0f;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Vector3 lastKnownTargetLocation;

    [HideInInspector] public Vector3 wanderTarget;
    [HideInInspector] public float stateEnterTime;
    public float wanderRadius = 10.0f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateEnterTime = Time.time;
    }

    private void Update()
    {
        if (currentState)
        {
            currentState.UpdateState(this);
        }
    }

    void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 2.0f);
        }
    }
    public void TransitionToState(State nextState)
    {
        if (nextState != null && nextState != currentState)
        {
            currentState.Exit(this);
            currentState = nextState;
            currentState.Enter(this);
            stateEnterTime = Time.time;
        }

    }
}