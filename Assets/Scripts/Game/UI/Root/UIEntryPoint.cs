using Game.UI.Root;
using Game.UI.Root.View;
using UnityEngine;
using R3;

public class UIEntryPoint : MonoBehaviour
{
    [SerializeField] private UIRootBinder _sceneUIRootPrefab;

    private Subject<UIExitParams> _exitSceneSignalSubj = new();

    public Observable<UIExitParams> Run(DIContainer container, UIEnterParams uiEnterParams)
    {
        UIRegistrationDI.Register(container, uiEnterParams);
        var uiViewModelContainer = new DIContainer(container);
        UIViewDIRegistration.Register(uiViewModelContainer);

        var uiScene = Instantiate(_sceneUIRootPrefab);
        var uiRoot = container.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(uiScene.gameObject);
        
        var strategySelector = uiScene.GetComponentInChildren<StrategySelector>();
        if (strategySelector != null)
        {
            strategySelector.Init(OnStrategyChosen);
        }

        Debug.Log($"UI Scene has loaded, waiting for strategy...");
        return _exitSceneSignalSubj.AsObservable();
    }

    private void OnStrategyChosen(IGridSizeProvider strategy)
    {
        strategy.GetEnterParams()
            .Subscribe(gameParams =>
            {
                Debug.Log($"Strategy returned: {gameParams.Width}x{gameParams.Height}");
                var exitParams = new UIExitParams(gameParams);
                _exitSceneSignalSubj.OnNext(exitParams);
            });
    }
}
