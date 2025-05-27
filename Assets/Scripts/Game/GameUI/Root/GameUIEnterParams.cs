public class GameUIEnterParams
{
    public int Width { get; }
    public int Height { get; }
    public bool GameMode { get; }

    public GameUIEnterParams(int width, int height, bool gameMode)
    {
        Width = width;
        Height = height;
        GameMode = gameMode;
    }
}
