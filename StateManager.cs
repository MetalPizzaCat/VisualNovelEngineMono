public enum GameState
{
    /// <summary>
    /// Dialog when player has absolutely no control over what is happening
    /// and just has to embrace the text 
    /// </summary>
    Dialog,
    /// <summary>
    /// Player gets to choose one of the dialog options presented
    /// </summary>
    DialogOption,
    /// <summary>
    /// Player gets to choose stuff outside of dialog
    /// Like choosing a target on the map
    /// </summary>
    FreeFormOption
}
public class StateManager
{
    private GameState _currentState;

    public GameState State
    {
        get => _currentState;
        set
        {
            //???
            //wait where is code???
            _currentState = value;
        }
    }
    public StateManager()
    {
        _currentState = GameState.Dialog;
    }
}