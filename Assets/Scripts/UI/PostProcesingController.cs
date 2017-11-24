using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

[Serializable]
class VideoParam
{
	public bool AAenabled = true;
	public int AApreset = 2;
	public bool MBenabled = true;
	public bool AOenabled = true;
}

public class PostProcesingController : MonoBehaviour {

	private PostProcessingProfile _profile;
	private VideoParam _params = new VideoParam();

	public Toggle AAToggle;
	public Dropdown AAQuality;
	public Toggle MBToggle;
	public Toggle AOToggle;

	void Start() {
		_profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;

		if (File.Exists (Application.persistentDataPath + "/videoSettings.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/videoSettings.dat", FileMode.Open);
			_params = (VideoParam)bf.Deserialize (file);

			file.Close (); 

			AAToggle.isOn = _params.AAenabled;
			AAQuality.value = _params.AApreset;
			MBToggle.isOn = _params.MBenabled;
			AOToggle.isOn = _params.AOenabled;
		}
	}

	void OnDestroy() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/videoSettings.dat");

		bf.Serialize(file, _params);
		file.Close();

		_profile.antialiasing.enabled = true;

		AntialiasingModel.Settings s = _profile.antialiasing.settings;
		s.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Default;

		_profile.antialiasing.settings = s;

		_profile.motionBlur.enabled = true;
		_profile.ambientOcclusion.enabled = true;
	}

	public void EnableAntialiasing(bool value) {
		_profile.antialiasing.enabled = value;
		_params.AAenabled = value;
	}

	public void AntialiasingQuality(int value) {
		AntialiasingModel.Settings s = _profile.antialiasing.settings;

		_params.AApreset = value;

		switch (value) {
		case 0:
			s.fxaaSettings.preset = AntialiasingModel.FxaaPreset.ExtremePerformance;
			break;
		case 1:
			s.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Performance;
			break;
		case 2:
			s.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Default;
			break;
		case 3:
			s.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Quality;
			break;
		case 4:
			s.fxaaSettings.preset = AntialiasingModel.FxaaPreset.ExtremeQuality;
			break;
		}

		_profile.antialiasing.settings = s;
	}

	public void EnableMotionBlur(bool value) {
		_profile.motionBlur.enabled = value;
		_params.MBenabled = value;
	}

	public void EnableAO(bool value) {
		_profile.ambientOcclusion.enabled = value;
		_params.AOenabled = value;
	}
}
