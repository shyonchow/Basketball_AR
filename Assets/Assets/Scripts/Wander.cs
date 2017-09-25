using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {

	public int score;
	public Ball ball;
	public float speed = 100000000000f;
	private Camera cam;

	void Start () {
	    cam = Camera.main;
	}
	
	void Update() {
		score = ball.count;
		if (score % 5 == 0 && score > 0) {
//			int LR = Random.Range(1, 10);
//			if (LR >= 5) { speed *= -1; }
			Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
			//Debug.Log("pos: " + screenPos.x);
			if (screenPos.x > 0 && screenPos.x < cam.pixelWidth) {
				transform.Translate(speed * Time.deltaTime, 0, 0);
			}
		}
	}
}
