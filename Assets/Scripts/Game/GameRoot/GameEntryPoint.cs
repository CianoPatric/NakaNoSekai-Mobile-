using System.Collections;
using Game.LocalView.Root;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint _instance;
    private Coroutines _coroutines;
    private UIRootView _uiRoot;
    private readonly DIContainer _rootContainer = new();
    private DIContainer CashContainer;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void FirstLoad()
    {
        Application.targetFrameRate = 30;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _instance = new GameEntryPoint();
        _instance.StartAPP();
    }

    private GameEntryPoint()
    {
        _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);
        var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
        _uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_uiRoot.gameObject);
        _rootContainer.RegisterInstance(_uiRoot);
    }
    private void StartAPP()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "UI")
        {
            _coroutines.StartCoroutine(LoadAndStartAPP());
            return;
        }
        if(sceneName == "GameUI")
        {
            GameUIEnterParams _gameUIEnter = new GameUIEnterParams(3, 3);
            _coroutines.StartCoroutine(LoadAndStartGameUI(_gameUIEnter));
            return;
        }
        if (sceneName != "Boot")
        {
            return;
        }
#endif
        _coroutines.StartCoroutine(LoadAndStartAPP());
    }
    private IEnumerator LoadAndStartAPP(UIEnterParams enterParams = null)
    {
        _uiRoot.ShowLoadingScreen();
        CashContainer?.Dispose();
        yield return LoadScene("Boot");
        yield return LoadScene("UI");
        yield return new WaitForSeconds(0.5f);
        var sceneEntryPoint = Object.FindFirstObjectByType<UIEntryPoint>();
        var UIContainer = CashContainer = new DIContainer(_rootContainer);
        sceneEntryPoint.Run(UIContainer, enterParams).Subscribe(uiExitParams =>
        {
            var targetSceneName = uiExitParams.TargetSceneEnterParams.SceneName;
            if (targetSceneName == "GameUI")
            {
                _coroutines.StartCoroutine(LoadAndStartGameUI(uiExitParams.TargetSceneEnterParams.As<GameUIEnterParams>()));   
            }
            else
            {
                _coroutines.StartCoroutine(LoadAndStartLocalView());
            }
        });
        _uiRoot.HideLoadingScreen();
    }
    private IEnumerator LoadAndStartGameUI(GameUIEnterParams gameUIEnterParams)
    {
        _uiRoot.ShowLoadingScreen();
        CashContainer?.Dispose();
        yield return LoadScene("Boot");
        yield return LoadScene("GameUI");
        yield return new WaitForSeconds(0.5f);
        var sceneEntryPoint = Object.FindFirstObjectByType<GameUIEntryPoint>();
        var gameUIContainer = CashContainer = new DIContainer(_rootContainer);
        sceneEntryPoint.Run(gameUIContainer, gameUIEnterParams).Subscribe(gameUIExitParams =>
        {
            _coroutines.StartCoroutine(LoadAndStartAPP(gameUIExitParams.UIEnterParams));
        });
        _uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartLocalView(GameUIEnterParams gameUIEnterParams = null)
    {
        _uiRoot.ShowLoadingScreen();
        yield return LoadScene("Boot");
        yield return LoadScene("LocalView");
        yield return new WaitForSeconds(0.5f);
        var sceneEntryPoint = Object.FindFirstObjectByType<LocalViewEntryPoint>();
        sceneEntryPoint.Run(_uiRoot).Subscribe(gameUIExitParams =>
        {
            _coroutines.StartCoroutine(LoadAndStartAPP());
        });
        _uiRoot.HideLoadingScreen();
    }
    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
