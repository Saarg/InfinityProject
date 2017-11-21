using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Dungeon {

	public class IRoom {
		public GameObject gameObject;
		public IRoom top;
		public IRoom down;
		public IRoom left;
		public IRoom right;

		public bool generated = false;
		public bool validated = false;
	}

	public class Generator : MonoBehaviour {

		[SerializeField]
		private GameObject _start;
		[SerializeField]
		private GameObject _finish;

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
	    private const float _roomSize = 9f*1.3f;
		private const float _HroomSize = _roomSize/2;
		private IRoom[,] _grid = new IRoom[_levelSize, _levelSize];

		[SerializeField]
		private Canvas loadingCanvas;
		[SerializeField]
		private Slider loadingBar;

		void Start () {
			Random.InitState (System.Environment.TickCount);
			_perlinScale = Random.Range (0.9f, 1.1f);

			loadingBar.maxValue = 3*_levelSize + _levelSize*_levelSize;
			loadingBar.value = 0;

			StartCoroutine (Generate());

			#if UNITY_EDITOR
			StartCoroutine (DrawLines());
			#endif
		}

		IEnumerator DrawLines() {
			yield return new WaitForSeconds (0.1f);

			while (true) {
				for (int x = 0; x < _levelSize; x++) {
					for (int y = 0; y < _levelSize; y++) {
						if(_grid[x, y].top != null)
							Debug.DrawLine (new Vector3 (x * _roomSize + _HroomSize, 0.1f, y * _roomSize + _HroomSize), new Vector3 (x * _roomSize + _HroomSize, 0.1f, y * _roomSize + _roomSize), Color.blue, 0.1f);

						if(_grid[x, y].down != null)
							Debug.DrawLine (new Vector3 (x * _roomSize + _HroomSize, 0.1f, y * _roomSize + _HroomSize), new Vector3 (x * _roomSize + _HroomSize, 0.1f, y * _roomSize), Color.blue, 0.1f);

						if(_grid[x, y].left != null)
							Debug.DrawLine (new Vector3 (x * _roomSize + _HroomSize, 0.1f, y * _roomSize + _HroomSize), new Vector3 (x * _roomSize, 0.1f, y * _roomSize + _HroomSize), Color.blue, 0.1f);

						if(_grid[x, y].right != null)
							Debug.DrawLine (new Vector3 (x * _roomSize + _HroomSize, 0.1f, y * _roomSize + _HroomSize), new Vector3 (x * _roomSize + _roomSize, 0.1f, y * _roomSize + _HroomSize), Color.blue, 0.1f);
					}
				}
				yield return new WaitForSeconds (0.1f);
			}
		}

		IEnumerator Generate() {
			Time.timeScale = 0f;

			for (int x = 0; x < _levelSize; x++) {
				for (int y = 0; y < _levelSize; y++) {
					_grid [x, y] = new IRoom ();
				}
				yield return null;
				loadingBar.value++;
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
			}

			for (int x = 0; x < _levelSize; x++) {
				for (int y = 0; y < _levelSize; y++) {
					GenerateIRoom (x, y);
				}
				yield return null;
				loadingBar.value++;
			}

			_grid [_levelSize - 1, _levelSize - 1].top = new IRoom();
			_grid [_levelSize - 1, _levelSize - 1].top.gameObject = _finish;

			_grid [0, 0].left = new IRoom();
			_grid [0, 0].left.gameObject = _start;

			ValidateIRooms (_grid [0, 0]);

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
						_grid [x, y].gameObject = Instantiate (_Xrooms [Random.Range(0, _Xrooms.Length)], new Vector3 (x * _roomSize + _HroomSize, 0, y * _roomSize + _HroomSize), Quaternion.identity);
					} else if (countExits == 3) {
						_grid [x, y].gameObject = Instantiate (_Trooms [Random.Range (0, _Trooms.Length)], new Vector3 (x * _roomSize + _HroomSize, 0, y * _roomSize + _HroomSize), Quaternion.identity);

						if (horizontal && _grid [x, y].top != null)
							_grid [x, y].gameObject.transform.Rotate (0, 180f, 0);
						else if(vertical && _grid [x, y].right != null)
							_grid [x, y].gameObject.transform.Rotate(0, -90f, 0);
						else if(vertical && _grid [x, y].left != null)
							_grid [x, y].gameObject.transform.Rotate(0, 90f, 0);
					} else if (countExits == 2 && !vertical && !horizontal) {
						_grid [x, y].gameObject = Instantiate (_Lrooms [Random.Range (0, _Lrooms.Length)], new Vector3 (x * _roomSize + _HroomSize, 0, y * _roomSize + _HroomSize), Quaternion.identity);


						if (_grid [x, y].down != null && _grid [x, y].right != null)
							_grid [x, y].gameObject.transform.Rotate (0, -90f, 0);
						else if (_grid [x, y].top != null && _grid [x, y].left != null)
							_grid [x, y].gameObject.transform.Rotate (0, 90f, 0);
						else if (_grid [x, y].top != null && _grid [x, y].right != null)
							_grid [x, y].gameObject.transform.Rotate (0, 180f, 0);
					} else if (countExits == 2 && vertical) {
						_grid [x, y].gameObject = Instantiate (_Srooms [Random.Range (0, _Srooms.Length)], new Vector3 (x * _roomSize + _HroomSize, 0, y * _roomSize + _HroomSize), Quaternion.identity);
					} else if (countExits == 2 && horizontal) {
						_grid [x, y].gameObject = Instantiate (_Srooms [Random.Range (0, _Srooms.Length)], new Vector3 (x * _roomSize + _HroomSize, 0, y * _roomSize + _HroomSize), Quaternion.identity);
						_grid [x, y].gameObject.transform.Rotate (0, 90f, 0);
					} else if (countExits == 1) {
						_grid [x, y].gameObject = Instantiate (_Irooms [Random.Range (0, _Irooms.Length)], new Vector3 (x * _roomSize + _HroomSize, 0, y * _roomSize + _HroomSize), Quaternion.identity);

						if (_grid [x, y].left != null)
							_grid [x, y].gameObject.transform.Rotate (0, 90f, 0);
						else if (_grid [x, y].top != null)
							_grid [x, y].gameObject.transform.Rotate (0, 180f, 0);
						else if (_grid [x, y].right != null)
							_grid [x, y].gameObject.transform.Rotate (0, -90f, 0);
					}
				}
				yield return null;
				loadingBar.value++;
			}

			#if UNITY_EDITOR
			for (int x = 0; x < _levelSize; x++) {
				for (int y = 0; y < _levelSize; y++) {
					if (_grid[x, y].gameObject != null)
						_grid[x, y].gameObject.transform.SetParent(transform);
				}
			}
			#endif

			for (int x = 0; x < _levelSize; x++) {
				for (int y = 0; y < _levelSize; y++) {
					if (_grid [x, y].gameObject != null) {
						_grid [x, y].gameObject.GetComponent<NavMeshSurface> ().BuildNavMesh ();
						yield return null;
					}

					loadingBar.value++;
				}
			}

			Time.timeScale = 1f;

			loadingCanvas.gameObject.SetActive (false);
			yield return null;
		}

		void GenerateIRoom(int x, int y) {
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

		void ValidateIRooms(IRoom r) {
			if (r.validated)
				return;

			r.validated = true;

			if (r.top != null)
				ValidateIRooms(r.top);

			if (r.down != null)
				ValidateIRooms(r.down);

			if (r.left != null)
				ValidateIRooms(r.left);

			if (r.right != null)
				ValidateIRooms(r.right);
		}

		void OnDrawGizmos() {
			Gizmos.color = Color.red;

			for (int x = 0 ; x <= _levelSize ; x++)
				Gizmos.DrawLine (new Vector3 (x * _roomSize, 0, 0), new Vector3 (x * _roomSize, 0, _levelSize * _roomSize));

			for (int y = 0 ; y <= _levelSize ; y++)
				Gizmos.DrawLine (new Vector3 (0, 0, y * _roomSize), new Vector3 (_levelSize * _roomSize, 0, y * _roomSize));
		}
	}
}
