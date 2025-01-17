using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/States/Wander")]
public class WanderState : State 
{
    public override void Enter(IStateController controller)
    {
        base.Enter(controller);
        StateController castedController = (StateController)controller;
        castedController.hasWanderTarget = false;
    }

    public override void Exit(IStateController controller)
    {
        base.Exit(controller);
        // Consider stopping wandering as the end of the searching cycle
        if (StateController.FinishedSearching != null)
        {
            StateController.FinishedSearching();
        }
    }

}