using UnityEngine;

public class MusiquinhasPaNois : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource
    public AudioClip[] playlist;     // Array of AudioClips for the playlist
    private int currentTrack = 0;    // Index to track the current song

    void Start()
    {
        if (playlist.Length > 0)
        {
            PlayTrack(currentTrack);
        }
    }

    void PlayTrack(int index)
    {
        if (index >= 0 && index < playlist.Length)
        {
            audioSource.clip = playlist[index];
            audioSource.Play();
            Invoke(nameof(PlayNextTrack), playlist[index].length);
        }
    }

    void PlayNextTrack()
    {
        currentTrack = (currentTrack + 1) % playlist.Length; // Loop through the playlist
        PlayTrack(currentTrack);
    }

    void StopTrack()
    {
        audioSource.Stop();
        CancelInvoke(nameof(PlayNextTrack));
    }
}
