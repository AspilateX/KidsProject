using System;

public static class Gamemode
{
    public static event Action<GamemodeType> OnGamemodeChanged;
    public static GamemodeType CurrentGamemode { get; private set; }
    public static void ChangeGamemode(GamemodeType newGamemode)
    {
        CurrentGamemode = newGamemode;
        OnGamemodeChanged?.Invoke(newGamemode);
    }
}

public enum GamemodeType
{
    Building,
    PowerConfiguration
}
