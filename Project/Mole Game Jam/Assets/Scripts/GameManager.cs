using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }

    private float _moleBabiesHungerValue = 10;
    [SerializeField] float _wormHP = 10;
    public float WORM_HP { get => _wormHP; }
    public float MoleBabiesHungerValue { get => _moleBabiesHungerValue; set => _moleBabiesHungerValue = value; }


    void Awake() {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }


    public void GameOver() {
        //_tempUI.SetStatusText("Game over!");
    }
}
