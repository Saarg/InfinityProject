using UnityEngine;
using System.Collections;

public enum XboxControllerAxis { Mouse_X, Mouse_Y, Left_X, Left_Y, Right_X, Right_Y, Left_Trigger, Right_Trigger, Cross_X, Cross_Y }
public enum XboxControllerButtons { A, B, X, Y, Left_Bumper, Right_Bumper, Back, Start, Left_Stick, Right_Stick, Up, Down, Left, Right }

[System.Serializable]
public struct InputDefinition {
	public string name;
	public string[] posKeys;
	public string[] negKeys;
	public XboxControllerAxis[] axis;
	public XboxControllerButtons[] buttons;
	public float deadzone;
	public float value;
}

public class MultiOSControls : MonoBehaviour {

	public static MultiOSControls instance;

	public InputDefinition[] _inputs = new InputDefinition[]{};

	#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
	private string[] WindowsAxisNames = new string[]{ "mouse x", "mouse y", "joystick1 axis x", "joystick1 axis y", "joystick1 axis 4", "joystick1 axis 5", "joystick1 axis 9", "joystick1 axis 10", "joystick1 axis 6", "joystick1 axis 7"};
	private KeyCode[] WindowsButtonsCode = new KeyCode[]{ KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3, KeyCode.Joystick1Button4,
														  KeyCode.Joystick1Button5, KeyCode.Joystick1Button6, KeyCode.Joystick1Button7, KeyCode.Joystick1Button8, KeyCode.Joystick1Button9,
														  KeyCode.Joystick1Button13, KeyCode.Joystick1Button14, KeyCode.Joystick1Button11, KeyCode.Joystick1Button12};// no D-Pad on windows
	#elif (UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX)
	// No Dpad axis on mac
	private string[] MacAxisNames = new string[]{ "mouse x", "mouse y", "joystick1 axis x", "joystick1 axis y", "joystick1 axis 3", "joystick1 axis 4", "joystick1 axis 5", "joystick1 axis 6", "joystick1 axis 5", "joystick1 axis 6"};
	private KeyCode[] MacButtonsCode = new KeyCode[]{ KeyCode.Joystick1Button16, KeyCode.Joystick1Button17, KeyCode.Joystick1Button18, KeyCode.Joystick1Button19, KeyCode.Joystick1Button13,
													  KeyCode.Joystick1Button14, KeyCode.Joystick1Button10, KeyCode.Joystick1Button9, KeyCode.Joystick1Button11, KeyCode.Joystick1Button12,
													  KeyCode.Joystick1Button5, KeyCode.Joystick1Button6, KeyCode.Joystick1Button7, KeyCode.Joystick1Button8};
	#else
	private string[] LinuxAxisNames = new string[]{ "mouse x", "mouse y", "joystick1 axis x", "joystick1 axis y", "joystick1 axis 4", "joystick1 axis 5", "joystick1 axis 3", "joystick1 axis 6", "joystick1 axis 7", "joystick1 axis 8"};
	private KeyCode[] LinuxButtonsCode = new KeyCode[]{ KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3, KeyCode.Joystick1Button4,
														KeyCode.Joystick1Button5, KeyCode.Joystick1Button6, KeyCode.Joystick1Button7, KeyCode.Joystick1Button9, KeyCode.Joystick1Button10,
														KeyCode.Joystick1Button13, KeyCode.Joystick1Button14, KeyCode.Joystick1Button11, KeyCode.Joystick1Button12};
	#endif

	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0 ; i < _inputs.Length ; i++) {
			_inputs[i].value = 0;

			// get keyboad values
			foreach (string key in _inputs[i].posKeys) {
				if (Input.GetKey (key)) {
					_inputs [i].value = 1;
				}
			}
			foreach (string key in _inputs[i].negKeys) {
				if (Input.GetKey (key)) {
					_inputs [i].value = -1;
				}
			}

			// Manage controller depending on the os (you should use linux!)
			#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
			// get controller axis values
			foreach (XboxControllerAxis axis in _inputs[i].axis) {
				if (Input.GetAxis (WindowsAxisNames [(int)axis]) > _inputs [i].deadzone) {
					_inputs [i].value = (Input.GetAxis (WindowsAxisNames [(int)axis]) - _inputs [i].deadzone) * (1/(1-_inputs [i].deadzone));
				} else if (Input.GetAxis (WindowsAxisNames [(int)axis]) < -_inputs [i].deadzone) {
					_inputs [i].value = (Input.GetAxis (WindowsAxisNames [(int)axis]) + _inputs [i].deadzone) * (1/(1-_inputs [i].deadzone));
				}
			}

			// get controller buttons values
			foreach (XboxControllerButtons button in _inputs[i].buttons) {
				if (Input.GetKey (WindowsButtonsCode [(int)button])) {
					_inputs [i].value = 1;
				}
			}
			#elif (UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX)
			// get controller axis values
			foreach (XboxControllerAxis axis in _inputs[i].axis) {
				if (Input.GetAxis (MacAxisNames [(int)axis]) > _inputs [i].deadzone) {
					_inputs [i].value = (Input.GetAxis (MacAxisNames [(int)axis]) - _inputs [i].deadzone) * (1/(1-_inputs [i].deadzone));
				} else if (Input.GetAxis (MacAxisNames [(int)axis]) < -_inputs [i].deadzone) {
					_inputs [i].value = (Input.GetAxis (MacAxisNames [(int)axis]) + _inputs [i].deadzone) * (1/(1-_inputs [i].deadzone));
				}
			}

			// get controller buttons values
			foreach (XboxControllerButtons button in _inputs[i].buttons) {
				if (Input.GetKey (MacButtonsCode [(int)button])) {
					_inputs [i].value = 1;
				}
			}
			#else
			// get controller axis values
			foreach (XboxControllerAxis axis in _inputs[i].axis) {
				if (Input.GetAxis (LinuxAxisNames [(int)axis]) > _inputs [i].deadzone) {
					_inputs [i].value = (Input.GetAxis (LinuxAxisNames [(int)axis]) - _inputs [i].deadzone) * (1/(1-_inputs [i].deadzone));
				} else if (Input.GetAxis (LinuxAxisNames [(int)axis]) < -_inputs [i].deadzone) {
					_inputs [i].value = (Input.GetAxis (LinuxAxisNames [(int)axis]) + _inputs [i].deadzone) * (1/(1-_inputs [i].deadzone));
				}
			}

			// get controller buttons values
			foreach (XboxControllerButtons button in _inputs[i].buttons) {
				if (Input.GetKey (LinuxButtonsCode [(int)button])) {
					_inputs [i].value = 1;
				}
			}
			#endif
		}
	}

	static public float GetValue(string name) {
		foreach (InputDefinition i in instance._inputs) {
			if (i.name == name) {
				return Mathf.Clamp(i.value, -1.0f, 1.0f);
			}
		}
		return 0;
	}
}
