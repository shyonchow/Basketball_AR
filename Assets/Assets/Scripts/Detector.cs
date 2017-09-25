using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {
	public bool entered = false;

	public void OnTriggerEnter(Collider _collider) { entered = true; }

}