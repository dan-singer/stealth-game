using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateController : MonoBehaviour 
{
    public State currentState;
    public NavMeshAgent navMeshAgent;
    public List<Transform> wayPointList;
    public Transform eyes;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        currentState = nextState;
    }
}