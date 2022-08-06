using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameDataScriptableObject")]
public class GameData : ScriptableObject
{
    [Header("Default Player Settings")]
    public float PlayerHealth;
    public float PlayerStamina;

    [Tooltip("Player's default movement speed")]
    public float MovementSpeed;

    public float[] EncumberedSpeeds; 

    [Tooltip("Player's default digging speed")]
    public float PlayerDigSpeed;
    [Tooltip("Player's default digging speed")]
    public float DigStaminaCost;
    [Tooltip("Player's default burrow speed")]
    public float PlayerBurrowSpeed;
    [Tooltip("Player's default digging speed")]
    public float BurrowStaminaCost;

    [Header("Gameplay Settings")]
    //[Header("MoleBaby Settings")]
    [Tooltip("how fast baby hunger value goes down")]
    public float MoleBabyHungerScale;

    // duration of the year in mins/fps
    [Tooltip("how fast baby hunger value goes down")]
    public float CalandarDuration;

}
