using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/States/Search")]
public class SearchState : State
{
    /// <summary>
    /// NavMesh speed mutliplier when this state is entered
    /// </summary>
    public float speedMultiplier = 1.5f;
    public override void Enter(IStateController controller)
    {
        base.Enter(controller);
        StateController castedController = (StateController)controller;
        if (StateController.BeganSearching != null)
        {
            StateController.BeganSearching();
        }
    }

    public override void Exit(IStateController controller)
    {
        base.Exit(controller);
        StateController castedController = (StateController)controller;
    }
}