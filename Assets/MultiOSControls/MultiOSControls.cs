using UnityEngine;
using System.Collections;

public enum PlayerNumber { Player1, Player2, Player3, Player4, All }
public enum ControllerNumber { Controller1, Controller2, Controller3, Controller4, None }

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

[System.Serializable]
public class PlayerInput {
	public string name;

	public ControllerNumber controller = ControllerNumber.Controller1;
	public bool keyboard = true;

	public InputDefinition[] inputs;

	public string[] AxisNames { get { return _AxisNames [(int)controller]; } }
	public KeyCode[] ButtonsCode { get { return _ButtonsCode [(int)controller]; } }


	#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
	private string[][] _AxisNames = new string[][] {
		new string[]{ "mouse x", "mouse y", "joystick1 axis x", "joystick1 axis y", "joystick1 axis 4", "joystick1 axis 5", "joystick1 axis 9", "joystick1 axis 10", "joystick1 axis 6", "joystick1 axis 7"}, 
		new string[]{ "mouse x", "mouse y", "joystick2 axis x", "joystick2 axis y", "joystick2 axis 4", "joystick2 axis 5", "joystick2 axis 9", "joystick2 axis 10", "joystick2 axis 6", "joystick2 axis 7"},
		new string[]{ "mouse x", "mouse y", "joystick3 axis x", "joystick3 axis y", "joystick3 axis 4", "joystick3 axis 5", "joystick3 axis 9", "joystick3 axis 10", "joystick3 axis 6", "joystick3 axis 7"},
		new string[]{ "mouse x", "mouse y", "joystick4 axis x", "joystick4 axis y", "joystick4 axis 4", "joystick4 axis 5", "joystick4 axis 9", "joystick4 axis 10", "joystick4 axis 6", "joystick4 axis 7"}
	};


	private KeyCode[][] _ButtonsCode = new KeyCode[][]{
		new KeyCode[]{ KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3, KeyCode.Joystick1Button4,
			KeyCode.Joystick1Button5, KeyCode.Joystick1Button6, KeyCode.Joystick1Button7, KeyCode.Joystick1Button8, KeyCode.Joystick1Button9,
			KeyCode.Joystick1Button13, KeyCode.Joystick1Button14, KeyCode.Joystick1Button11, KeyCode.Joystick1Button12},
		new KeyCode[]{ KeyCode.Joystick2Button0, KeyCode.Joystick2Button1, KeyCode.Joystick2Button2, KeyCode.Joystick2Button3, KeyCode.Joystick2Button4,
			KeyCode.Joystick2Button5, KeyCode.Joystick2Button6, KeyCode.Joystick2Button7, KeyCode.Joystick2Button8, KeyCode.Joystick2Button9,
			KeyCode.Joystick2Button13, KeyCode.Joystick2Button14, KeyCode.Joystick2Button11, KeyCode.Joystick2Button12},
		new KeyCode[]{ KeyCode.Joystick3Button0, KeyCode.Joystick3Button1, KeyCode.Joystick3Button2, KeyCode.Joystick3Button3, KeyCode.Joystick3Button4,
			KeyCode.Joystick3Button5, KeyCode.Joystick3Button6, KeyCode.Joystick3Button7, KeyCode.Joystick3Button8, KeyCode.Joystick3Button9,
			KeyCode.Joystick3Button13, KeyCode.Joystick3Button14, KeyCode.Joystick3Button11, KeyCode.Joystick3Button12},
		new KeyCode[]{ KeyCode.Joystick4Button0, KeyCode.Joystick4Button1, KeyCode.Joystick4Button2, KeyCode.Joystick4Button3, KeyCode.Joystick4Button4,
			KeyCode.Joystick4Button5, KeyCode.Joystick4Button6, KeyCode.Joystick4Button7, KeyCode.Joystick4Button8, KeyCode.Joystick4Button9,
			KeyCode.Joystick4Button13, KeyCode.Joystick4Button14, KeyCode.Joystick4Button11, KeyCode.Joystick4Button12}
	};
	#elif (UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX)
	private string[][] _AxisNames = new string[][] {
		new string[]{ "mouse x", "mouse y", "joystick1 axis x", "joystick1 axis y", "joystick1 axis 3", "joystick1 axis 4", "joystick1 axis 5", "joystick1 axis 6", "joystick1 axis 5", "joystick1 axis 6"}, 
		new string[]{ "mouse x", "mouse y", "joystick2 axis x", "joystick2 axis y", "joystick2 axis 3", "joystick2 axis 4", "joystick2 axis 5", "joystick2 axis 6", "joystick2 axis 5", "joystick2 axis 6"},
		new string[]{ "mouse x", "mouse y", "joystick3 axis x", "joystick3 axis y", "joystick3 axis 3", "joystick3 axis 4", "joystick3 axis 5", "joystick3 axis 6", "joystick3 axis 5", "joystick3 axis 6"},
		new string[]{ "mouse x", "mouse y", "joystick4 axis x", "joystick4 axis y", "joystick4 axis 3", "joystick4 axis 4", "joystick4 axis 5", "joystick4 axis 6", "joystick4 axis 5", "joystick4 axis 6"}
	};


	private KeyCode[][] _ButtonsCode = new KeyCode[][]{
		new KeyCode[]{ KeyCode.Joystick1Button16, KeyCode.Joystick1Button17, KeyCode.Joystick1Button18, KeyCode.Joystick1Button19, KeyCode.Joystick1Button13,
			KeyCode.Joystick1Button14, KeyCode.Joystick1Button10, KeyCode.Joystick1Button9, KeyCode.Joystick1Button11, KeyCode.Joystick1Button12,
			KeyCode.Joystick1Button5, KeyCode.Joystick1Button6, KeyCode.Joystick1Button7, KeyCode.Joystick1Button8},
		new KeyCode[]{ KeyCode.Joystick2Button16, KeyCode.Joystick2Button17, KeyCode.Joystick2Button18, KeyCode.Joystick2Button19, KeyCode.Joystick2Button13,
			KeyCode.Joystick2Button14, KeyCode.Joystick2Button10, KeyCode.Joystick2Button9, KeyCode.Joystick2Button11, KeyCode.Joystick2Button12,
			KeyCode.Joystick2Button5, KeyCode.Joystick2Button6, KeyCode.Joystick2Button7, KeyCode.Joystick2Button8},
		new KeyCode[]{ KeyCode.Joystick3Button16, KeyCode.Joystick3Button17, KeyCode.Joystick3Button18, KeyCode.Joystick3Button19, KeyCode.Joystick3Button13,
			KeyCode.Joystick3Button14, KeyCode.Joystick3Button10, KeyCode.Joystick3Button9, KeyCode.Joystick3Button11, KeyCode.Joystick3Button12,
			KeyCode.Joystick3Button5, KeyCode.Joystick3Button6, KeyCode.Joystick3Button7, KeyCode.Joystick3Button8},
		new KeyCode[]{ KeyCode.Joystick4Button16, KeyCode.Joystick4Button17, KeyCode.Joystick4Button18, KeyCode.Joystick4Button19, KeyCode.Joystick4Button13,
			KeyCode.Joystick4Button14, KeyCode.Joystick4Button10, KeyCode.Joystick4Button9, KeyCode.Joystick4Button11, KeyCode.Joystick4Button12,
			KeyCode.Joystick4Button5, KeyCode.Joystick4Button6, KeyCode.Joystick4Button7, KeyCode.Joystick4Button8}
	};
	#else
	private string[][] _AxisNames = new string[][] {
		new string[]{ "mouse x", "mouse y", "joystick1 axis x", "joystick1 axis y", "joystick1 axis 4", "joystick1 axis 5", "joystick1 axis 3", "joystick1 axis 6", "joystick1 axis 7", "joystick1 axis 8"}, 
		new string[]{ "mouse x", "mouse y", "joystick2 axis x", "joystick2 axis y", "joystick2 axis 4", "joystick2 axis 5", "joystick2 axis 3", "joystick2 axis 6", "joystick2 axis 7", "joystick2 axis 8"},
		new string[]{ "mouse x", "mouse y", "joystick3 axis x", "joystick3 axis y", "joystick3 axis 4", "joystick3 axis 5", "joystick3 axis 3", "joystick3 axis 6", "joystick3 axis 7", "joystick3 axis 8"},
		new string[]{ "mouse x", "mouse y", "joystick4 axis x", "joystick4 axis y", "joystick4 axis 4", "joystick4 axis 5", "joystick4 axis 3", "joystick4 axis 6", "joystick4 axis 7", "joystick4 axis 8"}
	};


	private KeyCode[][] _ButtonsCode = new KeyCode[][]{
		new KeyCode[]{ KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3, KeyCode.Joystick1Button4,
			KeyCode.Joystick1Button5, KeyCode.Joystick1Button6, KeyCode.Joystick1Button7, KeyCode.Joystick1Button9, KeyCode.Joystick1Button10,
			KeyCode.Joystick1Button13, KeyCode.Joystick1Button14, KeyCode.Joystick1Button11, KeyCode.Joystick1Button12},
		new KeyCode[]{ KeyCode.Joystick2Button0, KeyCode.Joystick2Button1, KeyCode.Joystick2Button2, KeyCode.Joystick2Button3, KeyCode.Joystick2Button4,
			KeyCode.Joystick2Button5, KeyCode.Joystick2Button6, KeyCode.Joystick2Button7, KeyCode.Joystick2Button9, KeyCode.Joystick2Button10,
			KeyCode.Joystick2Button13, KeyCode.Joystick2Button14, KeyCode.Joystick2Button11, KeyCode.Joystick2Button12},
		new KeyCode[]{ KeyCode.Joystick3Button0, KeyCode.Joystick3Button1, KeyCode.Joystick3Button2, KeyCode.Joystick3Button3, KeyCode.Joystick3Button4,
			KeyCode.Joystick3Button5, KeyCode.Joystick3Button6, KeyCode.Joystick3Button7, KeyCode.Joystick3Button9, KeyCode.Joystick3Button10,
			KeyCode.Joystick3Button13, KeyCode.Joystick3Button14, KeyCode.Joystick3Button11, KeyCode.Joystick3Button12},
		new KeyCode[]{ KeyCode.Joystick4Button0, KeyCode.Joystick4Button1, KeyCode.Joystick4Button2, KeyCode.Joystick4Button3, KeyCode.Joystick4Button4,
			KeyCode.Joystick4Button5, KeyCode.Joystick4Button6, KeyCode.Joystick4Button7, KeyCode.Joystick4Button9, KeyCode.Joystick4Button10,
			KeyCode.Joystick4Button13, KeyCode.Joystick4Button14, KeyCode.Joystick4Button11, KeyCode.Joystick4Button12}
	};
	#endif
}

public class MultiOSControls : MonoBehaviour {

	public static MultiOSControls instance;

	public PlayerInput[] players;

	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		// update players inputs
		for (int j = 0; j < players.Length; j++) {
			for (int i = 0; i < players [j].inputs.Length; i++) {
				players [j].inputs [i].value = 0;

				if (players [j].keyboard) {
					// get keyboad values
					foreach (string key in players[j].inputs[i].posKeys) {
						if (Input.GetKey (key)) {
							players [j].inputs [i].value = 1;
						}
					}
					foreach (string key in players[j].inputs[i].negKeys) {
						if (Input.GetKey (key)) {
							players [j].inputs [i].value = -1;
						}
					}
				}

				if (players [j].controller != ControllerNumber.None) {
					// get controller axis values
					foreach (XboxControllerAxis axis in players[j].inputs[i].axis) {
						if (Input.GetAxis (players [j].AxisNames [(int)axis]) > players [j].inputs [i].deadzone) {
							players [j].inputs [i].value = (Input.GetAxis (players [j].AxisNames [(int)axis]) - players [j].inputs [i].deadzone) * (1 / (1 - players [j].inputs [i].deadzone));
						} else if (Input.GetAxis (players [j].AxisNames [(int)axis]) < -players [j].inputs [i].deadzone) {
							players [j].inputs [i].value = (Input.GetAxis (players [j].AxisNames [(int)axis]) + players [j].inputs [i].deadzone) * (1 / (1 - players [j].inputs [i].deadzone));
						}
					}

					// get controller buttons values
					foreach (XboxControllerButtons button in players[j].inputs[i].buttons) {
						if (Input.GetKey (players [j].ButtonsCode [(int)button])) {
							players [j].inputs [i].value = 1;
						}
					}
				}
			}
		}
	}
		
	static public float GetValue(string name, PlayerNumber player = PlayerNumber.Player1) {
		if (player == PlayerNumber.All) { // Read input from all players
			foreach (PlayerNumber n in System.Enum.GetValues(typeof(PlayerNumber))) {
				if (n == PlayerNumber.All) { // avoid infinite loop
					continue;
				}

				float value = GetValue (name, n);

				if (value != 0) {
					return value;
				}
			}
		} else { // Read input from one player
			foreach (InputDefinition i in instance.players[(int)player].inputs) {
				if (i.name == name) {
					return Mathf.Clamp (i.value, -1.0f, 1.0f);
				}
			}
		}
		return 0;
	}

	static public bool HasKeyboard(PlayerNumber player = PlayerNumber.Player1) {
		return instance.players [(int)player].keyboard;
	}

	static public ControllerNumber GetControllerNumber(PlayerNumber player = PlayerNumber.Player1) {
		return instance.players [(int)player].controller;
	}

	static public void ChooseController(int c, PlayerNumber player = PlayerNumber.Player1) {
		instance.players [(int)player].controller = (ControllerNumber)c;

		instance.players [(int)player].keyboard = (c == 4);

		// if no one is using keyboard allow player 1 to have keyboard as backup
		if (!instance.players [1].keyboard && !instance.players [2].keyboard && !instance.players [3].keyboard) {
			instance.players [0].keyboard = true;
		}
	}

	static public void BindPosKey(string name, string key, PlayerNumber player = PlayerNumber.All) {
		if (player == PlayerNumber.All) { // Read input from all players
			foreach (PlayerNumber n in System.Enum.GetValues(typeof(PlayerNumber))) {
				if (n == PlayerNumber.All) { // avoid infinite loop
					continue;
				}

				BindPosKey (name, key, n);
			}

			return;
		}

		foreach (InputDefinition i in instance.players[(int)player].inputs) {
			if (i.name == name) {
				i.posKeys [0] = key;
			}
		}
	}

	static public void BindNegKey(string name, string key, PlayerNumber player = PlayerNumber.All) {
		if (player == PlayerNumber.All) { // Read input from all players
			foreach (PlayerNumber n in System.Enum.GetValues(typeof(PlayerNumber))) {
				if (n == PlayerNumber.All) { // avoid infinite loop
					continue;
				}

				BindNegKey (name, key, n);
			}

			return;
		}

		foreach (InputDefinition i in instance.players[(int)player].inputs) {
			if (i.name == name) {
				i.negKeys [0] = key;
			}
		}
	}

}
