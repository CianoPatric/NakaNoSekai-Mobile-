using UnityEngine;
using UnityEngine.SceneManagement;

public static class ChangerScene
{
    public enum Scene
    {
        UI,
        GameUI
    }
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
