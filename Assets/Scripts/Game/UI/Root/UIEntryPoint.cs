using UnityEngine;
using R3;

public class UIEntryPoint : MonoBehaviour
{
    [SerializeField] private UIRootBinder _sceneUIRootPrefab;
    int width;
    int height;
    // ReSharper disable Unity.PerformanceAnalysis
    public Observable<UIExitParams> Run(DIContainer container, UIEnterParams uiEnterParams)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        var uiRoot = container.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);
        var enterParams = new GameUIEnterParams(width, height);
        var exitParams = new UIExitParams(enterParams);
        var exitToGameSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        Debug.Log($"UI Scene has loaded, with result: {uiEnterParams?.Result}");
        return exitToGameSceneSignal;
    }
}
