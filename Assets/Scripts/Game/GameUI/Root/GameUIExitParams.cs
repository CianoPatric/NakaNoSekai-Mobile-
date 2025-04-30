using UnityEngine;

public class GameUIExitParams
{
    public UIEnterParams UIEnterParams { get; }
    public GameUIExitParams(UIEnterParams uiEnterParams)
    {
        UIEnterParams = uiEnterParams;
    }
}
