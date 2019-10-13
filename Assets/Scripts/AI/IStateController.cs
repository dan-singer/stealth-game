/// <summary>
/// Interface for StateControllers 
/// </summary>
public interface IStateController 
{
    void TransitionToState(State state);
}