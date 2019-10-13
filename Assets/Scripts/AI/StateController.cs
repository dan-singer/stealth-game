using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// StateController for enemy AIs
/// </summary>
public class StateController : MonoBehaviour, IStateController 
{
    /// <summary>
    /// The current state of this ai
    /// </summary>
    public State currentState;
    public NavMeshAgent navMeshAgent;
    public List<Transform> wayPointList;
    public Transform eyes;
    public float sightRadius = 2.0f;
    public float maxSight = 5.0f;
    public float wanderRadius = 10.0f;

    public int nextWayPoint { get; set; }
    public Transform chaseTarget { get; set; }
    public Vector3 lastKnownTargetLocation { get; set; }
    public bool hasWanderTarget { get; set; }
    public float stateEnterTime { get; set; }
    /// <summary>
    /// How long it takes for the agent to lose track of where the player is
    /// </summary>
    public float targetSightMemoryDuration = .75f;

    public float lastTargetSightTime { get; set; }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateEnterTime = Time.time;
        lastTargetSightTime = -targetSightMemoryDuration; // Makes sure look doesn't fire incorrectly
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

    /// <summary>
    /// Transition to a state IF it's not null and not the current state
    /// </summary>
    /// <param name="nextState"></param>
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