using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;
    public float volume=1.0F; 
    void Start()
    {
        AudioListener.volume = volumeSlider.value* AudioListener.volume;
        volume = AudioListener.volume;
    }

    void Update()
    {
        AudioListener.volume = volumeSlider.value * AudioListener.volume;
        volume = AudioListener.volume;
    }

    void OnValueChanged()
    {
        AudioListener.volume = volumeSlider.value * AudioListener.volume;
        volume = AudioListener.volume;
    }
}