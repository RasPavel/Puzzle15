using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {
	public GameObject tile;
	private int[,] grid;
	private GameObject[] tiles;

	// Use this for initialization
	void Start () {
		createGrid ();
		createTiles ();

	}
	
	// Update is called once per frame
	void Update () {
		//comment
	
	}

	private void createGrid() {
		this.grid = new int[4,4];

		//generating solvable permutation
		int[] ar = new int[] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
		shuffle(ar);

		int N = 0;
		for (var i = 0; i < ar.Length; i++) {
			int invers_number = 0;
			if (ar[i] == 0) {
				N += i+1;
				continue;
			}
			for (var j = i + 1; j < ar.Length; j++) {
				if ((ar[i] < ar[j]) && (ar[j] != 0)) 	invers_number++;
			}
			N += invers_number;
		}
		if (N % 2 == 1) {
			//permutation is unsolvable; we must swap any two numbers
			if (ar[0] == 0 || ar[1] == 0) {
				var temp = ar[2];
				ar[2] = ar[3];
				ar[3] = temp;
			} else {
				var temp = ar[0];
				ar[0] = ar[1];
				ar[1] = temp;
			}
		}


		for (var i = 0; i < 4; i++) {
			for (var j = 0; j < 4; j++) {
				grid [i, j] = ar [i * 4 + j];		
			}
		}
		Debug.Log ("Grid: " + gridToString ());
	}

	private void createTiles() {
		tiles = new GameObject[15];
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				if (grid[i,j] != 0)	{
					var x =  (0.5 + i);
					var y = 0.1;
					var z = -0.5 - j;
					Vector3 position = new Vector3(	(float) x, (float) y, (float) z );	
					GameObject new_tile =  (GameObject) Instantiate(tile, position, Quaternion.identity);
					TextMesh textMesh = new_tile.GetComponentInChildren<TextMesh>();
					textMesh.text = grid[i,j].ToString();

					tiles[i] = new_tile;
				}
			}
		}

	}

	private string gridToString() {
		int rows = grid.GetLength(0);
		int cols = grid.GetLength (1);
		string result = "[";
		for (var i = 0; i < rows; i++) {
			result += "{";
			for (var j = 0; j < cols; j++) {
				result += grid[i,j] + " ";
			}
			result += "}";
		}
		return result + "]";

	}

	private void shuffle(int[] a) {
		for (var i = a.Length - 1; i > 0; i--) {
			var r = Random.Range (0, i);
			var temp = a [i];
			a [i] = a [r];
			a [r] = temp;
		}
	}
}
