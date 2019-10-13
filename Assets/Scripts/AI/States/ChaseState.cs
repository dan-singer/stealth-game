using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/States/Chase")]
public class ChaseState : State
{
    /// <summary>
    /// NavMesh speed mutliplier when this state is entered
    /// </summary>
    public float speedMultiplier = 1.5f;
    public override void Enter(IStateController controller)
    {
        base.Enter(controller);
        StateController castedController = (StateController)controller;
        castedController.navMeshAgent.speed *= speedMultiplier;
    }

    public override void Exit(IStateController controller)
    {
        base.Exit(controller);
        StateController castedController = (StateController)controller;
        castedController.navMeshAgent.speed /= speedMultiplier;
    }
}