using UnityEngine;
using R3;

public class GameUIEntryPoint : MonoBehaviour
{
    [SerializeField] private GameUIRootBinder _sceneUIRootPrefab;
    // ReSharper disable Unity.PerformanceAnalysis
    public Observable<GameUIExitParams> Run(DIContainer container, GameUIEnterParams gameUIEnterParams)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        var uiRoot = container.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(uiScene.gameObject);
        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);
        Debug.Log($"{gameUIEnterParams.Width}x{gameUIEnterParams.Height}");
        var enterParams = new UIEnterParams("Fatality");
        var exitParams = new GameUIExitParams(enterParams);
        var exitToUISceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        return exitToUISceneSignal;
    }
}
