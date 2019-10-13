using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/States/Chase")]
public class ChaseState : State
{
    public override void Enter(IStateController controller)
    {
        base.Enter(controller);
        StateController castedController = (StateController)controller;
        castedController.navMeshAgent.speed *= 1.5f;
    }

    public override void Exit(IStateController controller)
    {
        base.Exit(controller);
        StateController castedController = (StateController)controller;
        castedController.navMeshAgent.speed /= 1.5f;
    }
}