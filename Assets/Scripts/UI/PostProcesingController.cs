using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcesingController : MonoBehaviour {

	public PostProcessingProfile profile;

	public void EnableAntialiasing(bool value) {
		profile.antialiasing.enabled = value;
	}

	public void AntialiasingQuality(int value) {
		AntialiasingModel.Settings s = profile.antialiasing.settings;

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

		profile.antialiasing.settings = s;
	}

	public void EnableMotionBlur(bool value) {
		profile.motionBlur.enabled = value;
	}

	public void EnableAO(bool value) {
		profile.ambientOcclusion.enabled = value;
	}
}
