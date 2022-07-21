using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private int _babyCount = 3; 
    private int _foodSaved = 0;
    private int _minFoodRequired = 10;

    private float _moleBabiesHungerValue = 10;

    public static GameManager Instance { get => _instance; set => _instance = value; }
    public float MoleBabiesHungerValue { get => _moleBabiesHungerValue; set => _moleBabiesHungerValue = value; }
    public int BabyCount { get => _babyCount; set => _babyCount = value; }

    void Awake() {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        GameEvents.OnTimerFinished += GameComplete;
    }

    // you die or babies starve
    public void GameOver(FailStates failState) 
    {
        if (failState == FailStates.babiesDied)
            Debug.Log("game over babies died");
    }

    // time's up
    // check how many babies are alive
    // check if food requirement met
    // if less, random perecentage of child(s) die.
    // if more, good ending
    private void GameComplete()
    {
        if (_foodSaved >= _minFoodRequired)
            Debug.Log($"enough food for the season - {_babyCount} mole(s) survied");
        else
            GetEndResults();
    }
    private void GetEndResults()
    {
        // total 
    }

}

public enum FailStates
{
    playerDied, babiesDied
}
