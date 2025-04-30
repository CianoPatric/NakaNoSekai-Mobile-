using UnityEngine;

public class GameUIScript : MonoBehaviour
{
    public void SetUI()
    {
        ChangerScene.Load(ChangerScene.Scene.UI);
    }
}
