using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambientalSound;
    [SerializeField] private AudioSource effectSound;

    public void Initialize()
    {
        int audioValue = PlayerPrefs.GetInt("AudioOn", 1);
        SetupAudio(audioValue == 1);
    }

    public void SetupAudio(bool audioOn)
    {
        ambientalSound.volume = audioOn ? 0.3f : 0;
        effectSound.volume = audioOn ? 0.3f : 0;
    }
    public void StartMusicSound(AudioClip clip)
    {
        if (clip == null) return;
        ambientalSound.clip = clip;
        ambientalSound.Play();
    }

    public void StartEffectSound(AudioClip clip)
    {
        if (clip == null) return;
        effectSound.clip = clip;
        effectSound.Play();
    }

    public void StopAllOsts()
    {
        ambientalSound.Stop();
        effectSound.Stop();
    }

}