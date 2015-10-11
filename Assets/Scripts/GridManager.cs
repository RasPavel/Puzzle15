using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {
	public GameObject tile;
	private int[,] grid;
	private GameObject[,] tiles;



	// Use this for initialization
	void Start () {
		createGrid ();
		createTiles ();

	}
	
	// Update is called once per frame
	void Update () {

	
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
				if (grid[i,j] != 0) {
				}
				grid [i, j] = ar [i * 4 + j];		
			}
		}
		Debug.Log ("Grid: " + gridToString ());
	}

	private void createTiles() {
		tiles = new GameObject[4,4];
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				if (grid[i,j] != 0)	{
					var x =  (-1.5 + j);
					var y = 0.1;
					var z = 1.5 - i;
					Vector3 position = new Vector3(	(float) x, (float) y, (float) z );	
					GameObject new_tile =  (GameObject) Instantiate(tile, position, Quaternion.identity);
					TextMesh textMesh = new_tile.GetComponentInChildren<TextMesh>();
					textMesh.text = grid[i,j].ToString();

					tiles[i,j] = new_tile;
				}
			}
		}

	}

	public void moveRight(int row, int col) {
		Debug.Log ("moveright" + row.ToString() + " " + col.ToString());
		Debug.Log (gridToString());
		for (int i = 3; i > col; i--) {
			if (grid[row,i] == 0 && i > 0) {
				swap(row, i, row, i-1);
				updateTiles();
			}
		}
		Debug.Log (gridToString());
	}

	public void moveLeft(int row, int col) {
		for (int i = 0; i < col; i++) {
			if (grid[row,i] == 0 && i < 3) {
				swap (row, i, row, i+1);
				updateTiles();
			}
		}
	}

	public void moveUp(int row, int col) {
		for (int j = 0; j < row; j++) {
			if (grid[j,col] == 0 && j < 3) {
				swap (j, col, j+1, col);
				updateTiles();

			}
		}
	}

	public void moveDown(int row, int col) {
		for (int j = 3; j > row; j--) {
			if (grid[j, col] == 0 && j > 0) {
				swap (j, col, j-1, col);
				updateTiles();

			}
		}

	}

	private void swap(int rowA, int colA, int rowB, int colB) {
		//swap ints in grid
		int temp = grid [rowA, colA];
		grid [rowA, colA] = grid [rowB, colB];
		grid [rowB, colB] = temp;
		//swap corresponding tiles 
		GameObject tempOb = tiles [rowA, colA];
		tiles [rowA, colA] = tiles [rowB, colB];
		tiles [rowB, colB] = tempOb;
	}

	private void updateTiles() {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				if (grid[i,j] != 0)	{
					var x =  (-1.5 + j);
					var y = 0.1;
					var z = 1.5 - i;
					tiles[i,j].transform.position = new Vector3((float) x, (float) y, (float) z );
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
