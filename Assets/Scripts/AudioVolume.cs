using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    public void Awake() => AudioListener.volume = PlayerPrefs.GetFloat("AudioVolume", 0.5f);

    // public void Update() => Debug.Log(AudioListener.volume);
}
