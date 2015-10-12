using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public GameObject gridObject;

	private Vector3 dragBeginPos;
	private const float MoveThreshold = 0.5f;

	void OnMouseDown() {
		Vector3 mousePos = Input.mousePosition;
		dragBeginPos = Camera.main.ScreenToWorldPoint(mousePos);
	}

	void OnMouseUp() {
		Vector3 curWorldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dragVector = curWorldMousePos - dragBeginPos;
		processDrag(dragVector);
	}


	private void processDrag(Vector3 dragVector) {
		int col = (int) (dragBeginPos.x + 2);
		int row = (int) (2 - dragBeginPos.z);
		Debug.Log (" row: " + row.ToString() + "col: " + col.ToString());

		bool isDragHorizontal = Mathf.Abs(dragVector.x) > Mathf.Abs(dragVector.z);
		if (isDragHorizontal && (dragVector.x > MoveThreshold)) {
			gridObject.GetComponent<GridManager>().moveRight(row, col);
		} else if (isDragHorizontal && (dragVector.x < - MoveThreshold)) {
			gridObject.GetComponent<GridManager>().moveLeft(row, col);
		} else if (!isDragHorizontal && (dragVector.z > MoveThreshold)) {
			gridObject.GetComponent<GridManager>().moveUp(row, col);
		} else if (!isDragHorizontal && (dragVector.z < - MoveThreshold)) {
			gridObject.GetComponent<GridManager>().moveDown(row, col);
		}
	}
}
