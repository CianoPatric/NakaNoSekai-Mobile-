using Game.LocalView.Root.View;
using R3;
using UnityEngine;

namespace Game.LocalView.Root
{
    public class LocalViewEntryPoint: MonoBehaviour
    {
        [SerializeField] private LocalViewRootBinder _sceneUIRootPrefab;
        public Observable<GameUIExitParams> Run(UIRootView uiRoot)
        {
            //var uiScene = Instantiate(_sceneUIRootPrefab);
            //uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubj = new Subject<Unit>();
            //uiScene.Bind(exitSceneSignalSubj);
            var enterParams = new UIEnterParams("Fatality");
            var exitParams = new GameUIExitParams(enterParams);
            var exitToUISceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
            return exitToUISceneSignal;
        }
    }
}