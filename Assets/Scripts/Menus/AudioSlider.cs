using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
	public TextMeshProUGUI numberText;
	public AudioMixer mixer;
	public float startValue;
	public string volumeName;

	void Start()
	{
		// keeps sound levels persistent between scenes
		float existingSoundLevel;
		if (staticVariables.soundLevels.TryGetValue(gameObject.name, out existingSoundLevel))
		{ 
			startValue = existingSoundLevel; 
		}

		SetSliderNumberText(startValue);
		SetVolume(startValue);
	}

	public void SetSliderNumberText(float value) 
	{
		int percent = (int)Math.Round(value * 100, 0);
		numberText.text = percent.ToString();
	}

	public void SetVolume(float value)
	{
		staticVariables.soundLevels[gameObject.name] = value;
		mixer.SetFloat(volumeName, Mathf.Log10(value) * 20);
	}
}
