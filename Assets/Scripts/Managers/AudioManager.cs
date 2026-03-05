using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambientalSound;
    [SerializeField] private AudioSource effectSound;

    public void StartMusicSound(AudioClip clip)
    {
        ambientalSound.clip = clip;
        ambientalSound.Play();
    }

    public void StartEffectSound(AudioClip clip)
    {
        effectSound.clip = clip;
        effectSound.Play();
    }

    public void StopAllOsts()
    {
        ambientalSound.Stop();
        effectSound.Stop();
    }

}