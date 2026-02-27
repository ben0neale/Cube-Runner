using UnityEngine;

public class NewManager : MonoBehaviour
{
    public static NewManager Instance { get; set; }
    public enum GameDifficulty { easy, medium, hard }
    public GameDifficulty Difficulty { get; set; }
    public enum GameState { menu, play, pause, advert, transition }
    private GameState m_state;

    public GameState m_nextState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // If an instance already exists and it's not this one, destroy this duplicate
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set this as the instance and ensure it persists across scenes
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public GameState State
    {
        get { return m_state; }
        set { m_state = GameState.transition; m_nextState = value; }
    }

}
