using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public PlayerData p1;
    public PlayerData p2;
    // Singleton
    public static AudioManager Instance;

    public AudioSource hitSound;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // Singleton
        if (Instance != null) Destroy(gameObject);
        if (Instance == null) Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {   
        // Doing this in the audio manager bc why not
        p1.LoadKeybinds();
        p2.LoadKeybinds();
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Play();
    }


}
