using UnityEngine;

public class ScriptUI : MonoBehaviour
{
    public void SetGameUI()
    {
        ChangerScene.Load(ChangerScene.Scene.GameUI);
    }
}
