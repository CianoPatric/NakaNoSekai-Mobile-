using Game.UI.Root;
using Game.UI.Root.View;
using UnityEngine;
using R3;

public class UIEntryPoint : MonoBehaviour
{
    [SerializeField] private UIRootBinder _sceneUIRootPrefab;
    public int width;
    public int height;
    // ReSharper disable Unity.PerformanceAnalysis
    public Observable<UIExitParams> Run(DIContainer container, UIEnterParams uiEnterParams)
    {
        UIRegistrationDI.Register(container, uiEnterParams);
        var uiViewModelContainer = new DIContainer(container);
        UIViewDIRegistration.Register(uiViewModelContainer);

        var uiScene = Instantiate(_sceneUIRootPrefab);
        var uiRoot = container.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(uiScene.gameObject);
        
        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);
        var gridSizePicker = uiScene.GetComponentInChildren<GridSizePicker>();
        var enterParams = new GameUIEnterParams(width, height);
        if (gridSizePicker != null)
        {
            gridSizePicker.OnSizeSelected.Subscribe(newParams =>
            {
                Debug.Log($"[UIEntryPoint] Получены новые размеры: {newParams.Width} x {newParams.Height}");
                enterParams = new GameUIEnterParams(newParams.Width, newParams.Height);
            });
        }
        else
        {
            Debug.LogWarning("GridSizePicker не найден в UI-префабе!");
        }
        var exitParams = new UIExitParams(enterParams);
        var exitToGameSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        Debug.Log($"UI Scene has loaded, with result: {uiEnterParams?.Result}");
        return exitToGameSceneSignal;
    }
}
