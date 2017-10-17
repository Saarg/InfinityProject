using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {
	public class Room : MonoBehaviour {

		[SerializeField]
		protected Wave[] _waves;

		// Use this for initialization
		void Start () {
			_waves [0].StartWave ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
