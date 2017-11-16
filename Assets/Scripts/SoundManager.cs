using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]

public class SoundManager : MonoBehaviour
{
	public string param;
	public AudioMixer AudioMixer;

    void Start()
    {
		AudioMixer.SetFloat(param, GetComponent<Slider>().value);

		GetComponent<Slider>().onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

	public void ValueChangeCheck()
    {
		AudioMixer.SetFloat(param, GetComponent<Slider>().value);
    }
}