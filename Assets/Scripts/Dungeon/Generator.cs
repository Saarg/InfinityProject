using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {

	public class Room {
		public GameObject gameobject;
		public Room top;
		public Room down;
		public Room left;
		public Room right;

		public bool generated = false;
		public bool validated = false;
	}
	
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
		private GameObject[] _Srooms;

		[SerializeField]
		[Range(0f, 1f)]
		private float _connectionFactor = 0.7f;
		private float _perlinScale;

		[SerializeField]
		private const int _levelSize = 6;
		private Room[,] _grid = new Room[_levelSize, _levelSize];

		void Start () {
			Random.InitState (System.Environment.TickCount);
			_perlinScale = Random.Range (0.9f, 1.1f);

			StartCoroutine (Generate());
			StartCoroutine (DrawLines());
		}

		IEnumerator DrawLines() {
			yield return new WaitForSeconds (0.1f);

			while (true) {
				for (int x = 0 ; x <= _levelSize ; x++)
					Debug.DrawLine (new Vector3 (x * 9f, 0, 0), new Vector3 (x * 9f, 0, _levelSize * 9f), Color.red, 0.1f);

				for (int y = 0 ; y <= _levelSize ; y++)
					Debug.DrawLine (new Vector3 (0, 0, y * 9f), new Vector3 (_levelSize * 9f, 0, y * 9f), Color.red, 0.1f);

				for (int x = 0; x < _levelSize; x++) {
					for (int y = 0; y < _levelSize; y++) {
						if(_grid[x, y].top != null)
							Debug.DrawLine (new Vector3 (x * 9f + 4.5f, 0.1f, y * 9f + 4.5f), new Vector3 (x * 9f + 4.5f, 0.1f, y * 9f + 9f), Color.blue, 0.1f);

						if(_grid[x, y].down != null)
							Debug.DrawLine (new Vector3 (x * 9f + 4.5f, 0.1f, y * 9f + 4.5f), new Vector3 (x * 9f + 4.5f, 0.1f, y * 9f), Color.blue, 0.1f);

						if(_grid[x, y].left != null)
							Debug.DrawLine (new Vector3 (x * 9f + 4.5f, 0.1f, y * 9f + 4.5f), new Vector3 (x * 9f, 0.1f, y * 9f + 4.5f), Color.blue, 0.1f);

						if(_grid[x, y].right != null)
							Debug.DrawLine (new Vector3 (x * 9f + 4.5f, 0.1f, y * 9f + 4.5f), new Vector3 (x * 9f + 9f, 0.1f, y * 9f + 4.5f), Color.blue, 0.1f);
					}
				}
				yield return new WaitForSeconds (0.1f);
			}
		}

		IEnumerator Generate() {
			Time.timeScale = 0f;

			for (int x = 0; x < _levelSize; x++) {
				for (int y = 0; y < _levelSize; y++) {
					_grid [x, y] = new Room ();
				}
			}

			int xi=0;
			int yi=0;

			while (xi < _levelSize - 1 || yi < _levelSize - 1) {
				if ((xi >= _levelSize - 1 || Random.Range (0, 100)%2 == 1) && yi < _levelSize - 1) {
					_grid [xi, yi].top = _grid [xi, yi + 1];
					_grid [xi, yi + 1].down = _grid [xi, yi];

					yi++;
				} else {
					_grid[xi, yi].right = _grid[xi + 1, yi];
					_grid[xi + 1, yi].left = _grid[xi, yi];

					xi++;
				}

				yield return new WaitForEndOfFrame();

			}

			for (int x = 0; x < _levelSize; x++) {
				for (int y = 0; y < _levelSize; y++) {
					GenerateRoom (x, y);
				}
				yield return new WaitForEndOfFrame ();
			}

			ValidateRooms (_grid [0, 0]);

			for (int x = 0; x < _levelSize; x++) {
				for (int y = 0; y < _levelSize; y++) {
					if (!_grid [x, y].validated) {
						_grid [x, y].top = null;
						_grid [x, y].down = null;
						_grid [x, y].left = null;
						_grid [x, y].right = null;
					}

					int countExits = (_grid [x, y].top != null ? 1 : 0) + (_grid [x, y].down != null ? 1 : 0) + (_grid [x, y].left != null ? 1 : 0) + (_grid [x, y].right != null ? 1 : 0);
					bool vertical = _grid [x, y].top != null && _grid [x, y].down != null;
					bool horizontal = _grid [x, y].left != null && _grid [x, y].right != null;
						
					if (countExits > 3) {
						Instantiate (_Xrooms [Random.Range(0, _Xrooms.Length)], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity);
					} else if (countExits == 3) {
						int r = Random.Range (0, _Trooms.Length);

						if(horizontal && _grid [x, y].top != null)
							Instantiate (_Trooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate(0, 180f, 0);
						else if(horizontal && _grid [x, y].down != null)
							Instantiate (_Trooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity);
						else if(vertical && _grid [x, y].right != null)
							Instantiate (_Trooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate(0, -90f, 0);
						else if(vertical && _grid [x, y].left != null)
							Instantiate (_Trooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate(0, 90f, 0);
					} else if (countExits == 2 && !vertical && !horizontal) {
						int r = Random.Range (0, _Lrooms.Length);

						if (_grid [x, y].down != null && _grid [x, y].left != null)
							Instantiate (_Lrooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity);
						else if (_grid [x, y].down != null && _grid [x, y].right != null)
							Instantiate (_Lrooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate (0, -90f, 0);
						else if (_grid [x, y].top != null && _grid [x, y].left != null)
							Instantiate (_Lrooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate (0, 90f, 0);
						else if (_grid [x, y].top != null && _grid [x, y].right != null)
							Instantiate (_Lrooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate (0, 180f, 0);
					} else if (countExits == 2 && vertical) {
						Instantiate (_Srooms [Random.Range (0, _Srooms.Length)], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity);
					} else if (countExits == 2 && horizontal) {
						Instantiate (_Srooms [Random.Range (0, _Srooms.Length)], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate (0, 90f, 0);
					} else if (countExits == 1) {
						int r = Random.Range (0, _Irooms.Length);

						if (_grid [x, y].down != null)
							Instantiate (_Irooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity);
						else if (_grid [x, y].left != null)
							Instantiate (_Irooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate (0, 90f, 0);
						else if (_grid [x, y].top != null)
							Instantiate (_Irooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate (0, 180f, 0);
						else if (_grid [x, y].right != null)
							Instantiate (_Irooms [r], new Vector3 (x * 9f + 4.5f, 0, y * 9f + 4.5f), Quaternion.identity).transform.Rotate (0, -90f, 0);
					}
				}
				yield return new WaitForEndOfFrame ();
			}

			Time.timeScale = 1f;
			yield return null;
		}

		void GenerateRoom(int x, int y) {
			if (_grid [x, y].generated)
				return;

			if (y < _levelSize-1 && _grid[x, y].top == null && Mathf.PerlinNoise (_perlinScale*x, _perlinScale*(y+0.5f)) < _connectionFactor) {
				_grid[x, y].top = _grid[x, y+1];
				_grid[x, y+1].down = _grid[x, y];
			}

			if (y > 0 && _grid[x, y].down == null && Mathf.PerlinNoise (_perlinScale*x, _perlinScale*(y-0.5f)) < _connectionFactor) {
				_grid[x, y].down = _grid[x, y-1];
				_grid[x, y-1].top = _grid[x, y];
			}

			if (x < _levelSize-1 && _grid[x, y].right == null && Mathf.PerlinNoise (_perlinScale*(x+0.5f), _perlinScale*y) < _connectionFactor) {
				_grid[x, y].right = _grid[x+1, y];
				_grid[x+1, y].left = _grid[x, y];
			}

			if (x > 0 && _grid[x, y].right == null && Mathf.PerlinNoise (_perlinScale*(x-0.5f), _perlinScale*y) < _connectionFactor) {
				_grid[x, y].left = _grid[x-1, y];
				_grid[x-1, y].right = _grid[x, y];
			}

			_grid [x, y].generated = true;
		}

		void ValidateRooms(Room r) {
			if (r.validated)
				return;

			r.validated = true;

			if (r.top != null)
				ValidateRooms(r.top);

			if (r.down != null)
				ValidateRooms(r.down);

			if (r.left != null)
				ValidateRooms(r.left);

			if (r.right != null)
				ValidateRooms(r.right);
		}
	}
}
