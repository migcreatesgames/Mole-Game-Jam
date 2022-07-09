using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }

    [SerializeField] float _wormHP = 10;
    public float WORM_HP { get => _wormHP; }
    [SerializeField] TempUI _tempUI;

    
    void Awake() {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }


    void Start()
    {
        _tempUI.SetStatusText("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        _tempUI.SetStatusText("Game over!");
    }
}
