using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource backgroundAudioSource;     // Vai trò phát nhạc nền
    [SerializeField] private AudioSource effectAudioSource;
 
    [SerializeField] private AudioClip backGroundClip;              // Biến chứa nhạc nền
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private AudioClip keyClip;
    [SerializeField] private AudioClip killClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBackGroundMusic();
    }

    public void PlayBackGroundMusic()
    {
        backgroundAudioSource.clip = backGroundClip;    // Gán dữ liệu cho biết tệp âm thanh cần phát background
        backgroundAudioSource.Play();
    }
    public void PlayCoinSound()
    {
        effectAudioSource.PlayOneShot(coinClip);
    }
    public void PlayJumpSound()
    {
        effectAudioSource.PlayOneShot(jumpClip);
    }
    public void PlayKeySound()
    {
        effectAudioSource.PlayOneShot(keyClip);
    }
    public void PlayGameOverSound()
    {
        effectAudioSource.PlayOneShot(loseClip);
    }
    public void PlayKillSound()     // Âm thanh lần va chạm đầu với tag Killer
    {
        effectAudioSource.PlayOneShot(killClip);
    }
}
