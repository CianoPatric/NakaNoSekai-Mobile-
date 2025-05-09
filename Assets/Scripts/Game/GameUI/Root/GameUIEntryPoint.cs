using Game.GameRoot;
using Game.GameUI.Root;
using Game.GameUI.Root.View;
using UnityEngine;
using R3;

public class GameUIEntryPoint : MonoBehaviour
{
    [SerializeField] private GameUIRootBinder _sceneUIRootPrefab;
    // ReSharper disable Unity.PerformanceAnalysis
    public Observable<GameUIExitParams> Run(DIContainer container, GameUIEnterParams gameUIEnterParams)
    {
        GameUIRegistrationDI.Register(container, gameUIEnterParams);
        var gameUIViewModelContainer = new DIContainer(container);
        GameUIViewDIRegistration.Register(gameUIViewModelContainer);
        
        var uiScene = Instantiate(_sceneUIRootPrefab);
        var uiRoot = container.Resolve<UIRootView>();
        
        uiRoot.AttachSceneUI(uiScene.gameObject);
        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);
        var grid = Object.FindFirstObjectByType<BuildingsGrid>();
        (grid as IInjectable)?.Inject(container);
        var enterParams = new UIEnterParams("Fatality");
        var exitParams = new GameUIExitParams(enterParams);
        var exitToUISceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        return exitToUISceneSignal;
    }
}
