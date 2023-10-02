namespace Vigilate.Core;

public class VigilateSettings
{
    #region PreviousState
    internal bool _previousState = false;
    public bool State
    {
        get { return _previousState; }
        set { _previousState = value; }
    }
    #endregion
    #region PreviousState
    internal int _pollPeriodMs = 30000;
    public int PollPeriodMs
    {
        get { return _pollPeriodMs; }
        set { _pollPeriodMs = value; }
    }
    #endregion
}
