using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioSource playerAudioSource; // AudioSource para tocar os sons do jogador
    AudioSource musicAudioSource; // AudioSource para tocar a música de fundo
    AudioSource enemyAudioSource; // AudioSource para tocar os sons dos inimigos
    AudioSource uiAudioSource; // AudioSource para tocar os sons da interface do usuário

    // Singleton instance
    private void Awake()
    {
        playerAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        enemyAudioSource = gameObject.AddComponent<AudioSource>();
        uiAudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playBackgroundMusic(); // Inicia a música de fundo ao iniciar o jogo

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para tocar um som específico
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning("AudioSource component not found on this GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("AudioClip is null, cannot play sound.");
        }
    }

    void playBackgroundMusic()
    {
        musicAudioSource.clip = Resources.Load<AudioClip>("Audio/Music/BackgroundMusic"); // Carrega a música de fundo do diretório Resources
        musicAudioSource.loop = true; // Configura a música de fundo para tocar em loop
        musicAudioSource.volume = 0.2f; // Define o volume da música de fundo (0.0 a 1.0)
        musicAudioSource.Play(); // Inicia a reprodução da música de fundo
    }
}
