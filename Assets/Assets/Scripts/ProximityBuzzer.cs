using UnityEngine;
using System.Collections;
using Kudan.AR;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ProximityBuzzer : MonoBehaviour
{
    public AudioSource buzzer;
    public AudioClip buzz;
    public GameObject target;
    public Transform cam;
    public Text warning;
    public KudanTracker tracker;

    //    float GetDistance() {
    //        RaycastHit hit = new RaycastHit();
    //
    //        if (Physics.Linecast(cam.position, target.transform.position, out hit)) {
    //            //Debug.Log("Object hit: " + hit.collider);
    //            return hit.distance;
    //        }
    //
    //        //Debug.Log("No RB hit");
    //        return 0;
    //    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        //Debug.Log("Distance: " + dist);
        if (dist < 100 && tracker.ArbiTrackIsTracking())
        {
            //buzzer.PlayOneShot(buzz);
            warning.text = "WARNING: TOO CLOSE";
        }
        else { warning.text = ""; }
    }
}
