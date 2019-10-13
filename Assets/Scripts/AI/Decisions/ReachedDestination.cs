using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ReachedDestination")]
public class ReachedDestination : Decision
{
    public override bool Decide(IStateController controller)
    {
        StateController castedController = (StateController)controller;
        return castedController.navMeshAgent.remainingDistance <= castedController.navMeshAgent.stoppingDistance && !castedController.navMeshAgent.pathPending;
    }
}
