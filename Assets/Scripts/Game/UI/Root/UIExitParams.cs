public class UIExitParams
{
    public GameUIEnterParams GameUIEnterParams { get; }
    public UIExitParams(GameUIEnterParams gameUIEnterParams)
    {
        GameUIEnterParams = gameUIEnterParams;
    }
}
