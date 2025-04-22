using UnityEngine;

public class PlayAudio_Event : MonoBehaviour
{

   public AudioSource audioSource;

   public void PlayAudio()
    {
        audioSource.Play();
    }
    public void StopAudio()
    {
        audioSource.Stop();
    }
}