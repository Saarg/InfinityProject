using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {
	
	public class Generator : MonoBehaviour {
		[SerializeField]
		private GameObject[] _Xrooms;
		[SerializeField]
		private GameObject[] _Trooms;
		[SerializeField]
		private GameObject[] _Lrooms;
		[SerializeField]
		private GameObject[] _Irooms;

		[SerializeField]
		private const int _levelSize = 6;
		private GameObject[,] _grid = new GameObject[_levelSize, _levelSize];

		void Start () {
			Random.InitState ((int)(Random.value*100));

			for (int x = 0 ; x < _levelSize ; x++)
				Debug.DrawLine (new Vector3 (-_levelSize/2 * 9f + (x + 0.5f) * 9f, 0, -_levelSize/2 * 9f + 4.5f), new Vector3 (-_levelSize/2 * 9f + (x + 0.5f) * 9f, 0, _levelSize/2 * 9f - 4.5f), Color.red, 9999);

			for (int y = 0 ; y < _levelSize ; y++)
				Debug.DrawLine (new Vector3 (-_levelSize/2 * 9f + 4.5f, 0, -_levelSize/2 * 9f + (y + 0.5f) * 9f), new Vector3 (_levelSize/2 * 9f - 4.5f, 0, -_levelSize/2 * 9f + (y + 0.5f) * 9f), Color.red, 9999);


			_grid [_levelSize / 2, _levelSize / 2] = Instantiate (_Xrooms [Random.Range(0, _Xrooms.Length)], new Vector3(0, 0, 0), Quaternion.identity);

			Generate (_grid [_levelSize / 2, _levelSize / 2]);
		}

		void Update () {
			
		}

		void Generate(GameObject s) {
			foreach (Transform t in s.gameObject.transform.Find("EntyPoints")) {
				Vector3 newPos = t.position + t.forward * 4.5f;
				
				int gridCoordX = (int)(newPos.x / 9f + _levelSize / 2f);
				int gridCoordZ = (int)(newPos.z / 9f + _levelSize / 2f);

				Destroy (t.gameObject);
				Debug.Log (newPos + " " + gridCoordX + ", " + gridCoordZ + " " + _grid [gridCoordX, gridCoordZ]);

				if (_grid [gridCoordX, gridCoordZ] == null) {

					if (
						(gridCoordX <= 1 || gridCoordX >=  _levelSize - 1 || gridCoordZ <= 1 || gridCoordZ >=  _levelSize - 1) ||
						_grid [gridCoordX - 1, gridCoordZ] != null && 
						_grid [gridCoordX + 1, gridCoordZ] != null && 
						_grid [gridCoordX, gridCoordZ - 1] != null && 
						_grid [gridCoordX, gridCoordZ + 1] != null
					)
						_grid [gridCoordX, gridCoordZ] = Instantiate (_Irooms [Random.Range (0, _Irooms.Length)], newPos, t.rotation);
					else if (
						_grid [gridCoordX + (int)t.forward.x, gridCoordZ + (int)t.forward.z] != null && 
						_grid [gridCoordX + (int)t.right.x, gridCoordZ + (int)t.right.z] != null && 
						_grid [gridCoordX - (int)t.right.x, gridCoordZ - (int)t.right.z] != null
					)
						_grid [gridCoordX, gridCoordZ] = Instantiate (_Trooms [Random.Range (0, _Trooms.Length)], newPos, t.rotation);
					else if (
						_grid [gridCoordX + (int)t.forward.x, gridCoordZ + (int)t.forward.z] != null && 
						_grid [gridCoordX + (int)t.right.x, gridCoordZ + (int)t.right.z] != null && 
						_grid [gridCoordX - (int)t.right.x, gridCoordZ - (int)t.right.z] == null
					)
						_grid [gridCoordX, gridCoordZ] = Instantiate (_Lrooms [Random.Range (0, _Lrooms.Length)], newPos, t.rotation);
					else if (
						_grid [gridCoordX + (int)t.forward.x, gridCoordZ + (int)t.forward.z] != null && 
						_grid [gridCoordX + (int)t.right.x, gridCoordZ + (int)t.right.z] == null && 
						_grid [gridCoordX - (int)t.right.x, gridCoordZ - (int)t.right.z] != null
					)
						_grid [gridCoordX, gridCoordZ] = Instantiate (_Lrooms [Random.Range (0, _Lrooms.Length)], newPos, t.rotation);
					else if (Random.Range (0, 10) > 5)
						_grid [gridCoordX, gridCoordZ] = Instantiate (_Xrooms [Random.Range (0, _Xrooms.Length)], newPos, t.rotation);
					else 
						_grid [gridCoordX, gridCoordZ] = Instantiate (_Irooms [Random.Range (0, _Irooms.Length)], newPos, t.rotation);


					Generate (_grid [gridCoordX, gridCoordZ]);
				}
			}
		}
	}
}
