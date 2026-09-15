using UnityEngine;

public enum GameMode { Exploration, Combat}

public class Combat_Manager : MonoBehaviour
{
    public static GameMode _Current_Mode { get; private set; }

    public void EnterCombat()
    {
        _Current_Mode = GameMode.Combat;
    }

    public void EnterExploration()
    {
        _Current_Mode = GameMode.Exploration;
    }

    public GameMode Get_Current_State()
    {
        return _Current_Mode;
    }
}
