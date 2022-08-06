using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameDataScriptableObject")]
public class GameData : ScriptableObject
{
    [Header("Default Player Settings")]
    public float PlayerHealth;
    public float PlayerStamina;

    public float PlayerBurrowSpeed;

    [Tooltip("Player's default movement speed")]
    public float MovementSpeed;

    [Header("Gameplay Settings")]
    //[Header("MoleBaby Settings")]
    // how fast baby hunger value goes down
    [Tooltip("how fast baby hunger value goes down")]
    public float DEFAULT_BabyHungerScale;


    // duration of the year in mins/fps
    //[Header("Calandar Settings")]
    public int MAX_calandarTime;


}
