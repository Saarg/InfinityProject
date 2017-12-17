using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]

/*
 * UI script used to control mixer params with a slider
 */
public class SoundManager : MonoBehaviour
{
	public string param;
	public AudioMixer AudioMixer;

	/*
	 * On Start set slider value to mixer value
	 */
    void Start()
    {
		AudioMixer.SetFloat(param, GetComponent<Slider>().value);

		GetComponent<Slider>().onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

	/*
	 * Update param on slider change
	 */
	public void ValueChangeCheck()
    {
		AudioMixer.SetFloat(param, GetComponent<Slider>().value);
    }
}