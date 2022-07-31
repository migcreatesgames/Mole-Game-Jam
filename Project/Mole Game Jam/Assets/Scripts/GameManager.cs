using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private int _babyCount = 3; 
    private int _foodSaved = 0;
    private int _minFoodRequired = 10;

    private float _moleBabiesHungerValue = 10;
    private float _introLength = 30f; // get length from timeline
    private float _curIntoTime = 0;

    private bool _gameStarted = false;
    private bool _introPlaying = false;
    private int counter;

    public static GameManager Instance { get => _instance; set => _instance = value; }
    public float MoleBabiesHungerValue { get => _moleBabiesHungerValue; set => _moleBabiesHungerValue = value; }
    public int BabyCount { get => _babyCount; set => _babyCount = value; }
    public bool GameStarted { get => _gameStarted; set => _gameStarted = value; }
    public bool IntroPlaying { get => _introPlaying; set => _introPlaying = value; }


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
        GameEvents.OnGameBegin += BeginGame;
        BeginGame();
    }

    private void BeginGame() => StartCoroutine("InitGame");

    private IEnumerator InitGame()
    {
        _introPlaying = true;
        
        counter = (int)_introLength;
        while (counter > 0)
        {
            // enable ai movement for intro cutscene 
            if (counter == _introLength - 20)
                PlayerController.Instance.NavMeshAgent.enabled = true;

            yield return new WaitForSeconds(1);
            counter--;
        }
        StartGame();
    }
    
    public void StartGame()
    {
        _gameStarted = true;
        PlayerController.Instance.NavMeshAgent.enabled = false;
        PlayerController.Instance.EnableInput = true;
        PlayerController.Instance.EnableMovement = true;
        CameraManager.Instance.EnableMainCamera();
        _introPlaying = false;
        StopCoroutine("StartGame");
    }

    public void GameOver(FailStates failState) 
    {
        _gameStarted = false;
        PlayerController.Instance.EnableInput = false;
        PlayerController.Instance.EnableMovement = false;
        UIManager.Instance.DisplayEndMenu(failState);
    }

    private void GameComplete()
    {
        _gameStarted = false;
        PlayerController.Instance.EnableInput = false;
        PlayerController.Instance.EnableMovement = false;
        GetEndResults();
    }

    private void GetEndResults()
    {
        // random perecentage of child(ren) dying based on how
        // much food was saved and total children alive
        string resultSummary = "";
        if (_foodSaved >= _minFoodRequired)
            resultSummary = $"You've accumulated enough food to feed your babies - {_babyCount + 1} mole(s) survied";
        else
        {
            float d = Random.Range(0f, 100f);
            if ((d -= 30) < 0)
            {
                _babyCount -= 2;
                resultSummary = $"You didn't collect enough food for the Winter - only {_babyCount + 1} mole(s) survied \n" +
                    $"The rest died of hunger...";
            };
            if ((d -= 12) < 0)
            {
                resultSummary = $"You didn't collect enough food for the Winter \n" +
                    $"All your babies died...";
            };
            if ((d -= 45) < 0)
            {
                _babyCount--;
                resultSummary = $"You didn't collect enough food for the Winter - only {_babyCount + 1} mole(s) survied \n" +
                  $"One died of hunger...";
            };
        }
        
        UIManager.Instance.DisplayEndMenu(resultSummary);
    }

    private void SaveFood(int value)
    {
        _foodSaved += value;
        Debug.Log($"food saved:{_foodSaved}");
    }

    private void RemoveFood(int value)
    {
        _foodSaved += value;
        Debug.Log($"food removed:{_foodSaved}");
    }

    public void EatBaby()
    {
        _babyCount--;
        if (_babyCount == 0)
            GameOver(FailStates.babiesDied);
    }
}

public enum FailStates
{
    playerDied, babiesDied
}
