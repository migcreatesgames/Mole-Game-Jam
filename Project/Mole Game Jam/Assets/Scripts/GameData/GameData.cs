using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameDataScriptableObject")]
public class GameData : ScriptableObject
{
   
    public float MAX_PlayerHealth;
    public float MAX_PlayerStamina;

    public float DEFAULT_PlayerBurrowSpeed;
    // base move movement speed for player
    public float DEFAULT_MovementSpeed;


    // duration of the year in mins/fps
    // MAX_calandarTime


    // how fast baby hunger value goes down
    //defaultBabyHungerScale

}
