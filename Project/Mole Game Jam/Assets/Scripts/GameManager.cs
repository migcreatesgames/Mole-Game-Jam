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
        GameEvents.OnFoodSaved += SaveFood;
        GameEvents.OnFoodRemoved += RemoveFood;
    }

    public void GameOver(FailStates failState) 
    {
        if (failState == FailStates.babiesDied)
            Debug.Log("Game Over - babies died");
        else
            Debug.Log("Game Over - Player Died");
    }
    private void GameComplete()
    {
        if (_foodSaved >= _minFoodRequired)
            Debug.Log($"enough food for the season - {_babyCount} mole(s) survied");
        else
            GetEndResults();
    }
    private void GetEndResults()
    {
        // random perecentage of child(ren) dying based on how much food was saved and total children alive
        //float totalFoodPercentage = _foodSaved / _minFoodRequired;
        // if 
        float d = Random.Range(0f,100f);
        if ((d -= 30) < 0) 
        {
            _babyCount -= 2;
            Debug.Log($"Not Enough food for the season - only {_babyCount} mole(s) survied"); 
            Debug.Log($"The rest died of hunger..."); 
        };
        if ((d -= 12) < 0) { 
            Debug.Log($"Not Enough food for the season"); 
            Debug.Log($"All your babies die"); 
        };
        if ((d -= 45) < 0)
        {
            _babyCount--;
            Debug.Log($"Not Enough food for the season - only {_babyCount} mole(s) survied");
            Debug.Log($"One died of hunger...");
        };
    }

    private void SaveFood(int value)
    {
        _foodSaved += value;
        Debug.Log($"food saved:{_foodSaved}");
       
    }
    private void RemoveFood(int value)
    {
        _foodSaved -= value;
        Debug.Log($"food removed:{_foodSaved}");
      
    }
}

public enum FailStates
{
    playerDied, babiesDied
}
