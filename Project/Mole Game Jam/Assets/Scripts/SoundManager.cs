using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector]
    public static SoundManager instance;
    [HideInInspector]
    public static AudioSource source;
    [SerializeField]
    public static AudioSource MusicSource;
    [HideInInspector]
    public static AudioSource SFXSource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        InitSoundManager();
    }

    public static void PlaySound(AudioClip sound)
    {
        if (SFXSource != null)
            SFXSource.PlayOneShot(sound);
    }

    public static void PlayMusicTrack()
    {
        MusicSource.Play();
    }

    public void InitSoundManager()
    {
        MusicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        SFXSource = GetComponent<AudioSource>();
    }
}