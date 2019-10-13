using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Search")]
public class SearchAction : Action
{
    public override void Act(IStateController controller)
    {
        Search((StateController)controller);
    }

    private void Search(StateController controller)
    {
        controller.navMeshAgent.destination = controller.lastKnownTargetLocation;
        controller.navMeshAgent.isStopped = false;
    }
}
