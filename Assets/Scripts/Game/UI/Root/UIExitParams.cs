using Game.GameRoot;
using UnityEngine;

public class UIExitParams
{
    public SceneEnterParams TargetSceneEnterParams { get; }
    public UIExitParams(SceneEnterParams targetScene)
    {
        TargetSceneEnterParams = targetScene;
    }
}
