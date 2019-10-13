using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ChaseAction")]
public class ChaseAction : Action 
{
    public override void Act(IStateController controller)
    {
        Chase((StateController)controller);
    }
    private void Chase(StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.isStopped = false;
        controller.lastKnownTargetLocation = controller.chaseTarget.position;
    }
}