using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public Vector3 StartDrag;
	public Vector3 EndDrag;
	public float StartTime;
	public float EndTime;
	public float DeltaTime;
	public Vector2 Direction;
	public BasketballManager Manager;

	void SwipeHandler() {
		if (Input.touchCount == 1) {
			Touch fing = Input.GetTouch(0);

			if (fing.phase == TouchPhase.Began) {
				StartTime = Time.time;
				StartDrag = fing.position;
			} else if (fing.phase == TouchPhase.Ended) {
				EndTime = Time.time;
				EndDrag = fing.position;
				DeltaTime = EndTime = StartTime;
			} else if (fing.phase == TouchPhase.Moved) { Direction = fing.deltaPosition; }

			Manager.OnSwipe(StartDrag, EndDrag, DeltaTime);
		}
	}

	void FixedUpdate() { SwipeHandler(); }

}
