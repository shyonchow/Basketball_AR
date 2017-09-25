using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Kudan.AR.Samples
{
    /// <summary>
    /// Script used in the Kudan Samples. Provides functions that switch between different tracking methods and start abitrary tracking.
    /// </summary>
    public class SampleApp : MonoBehaviour
    {
        public KudanTracker _kudanTracker;	// The tracker to be referenced in the inspector. This is the Kudan Camera object.
        public TrackingMethodMarker _markerTracking;	// The reference to the marker tracking method that lets the tracker know which method it is using
        public TrackingMethodMarkerless _markerlessTracking;    // The reference to the markerless tracking method that lets the tracker know which method it is using

        public Text buttonText;

        //public Transform hoop;
        //private Transform hoopStart;
        //private Camera cam;

        //public void Start()
        //{
        //    cam = Camera.main;
        //    hoopStart = hoop;
        //}

        public void MarkerClicked()
        {
            _kudanTracker.ChangeTrackingMethod(_markerTracking);	// Change the current tracking method to marker tracking
        }

        public void MarkerlessClicked()
        {
            _kudanTracker.ChangeTrackingMethod(_markerlessTracking);	// Change the current tracking method to markerless tracking
        }

        public void StartClicked()
        {
            if (!_kudanTracker.ArbiTrackIsTracking())
            {
                // from the floor placer.
                Vector3 floorPosition;          // The current position in 3D space of the floor
                Quaternion floorOrientation;    // The current orientation of the floor in 3D space, relative to the device

                _kudanTracker.FloorPlaceGetPose(out floorPosition, out floorOrientation);   // Gets the position and orientation of the floor and assigns the referenced Vector3 and Quaternion those values
                _kudanTracker.ArbiTrackStart(floorPosition, floorOrientation);             // Starts markerless tracking based upon the given floor position and orientations
                //Vector3 targetPos = new Vector3(cam.transform.position.x, hoop.position.x, cam.transform.position.z);
                //hoop.LookAt(targetPos);
            }

            else
            {
                _kudanTracker.ArbiTrackStop();
                //hoop = hoopStart;
            }
        }

        void Update()
        {
            if (!_kudanTracker.ArbiTrackIsTracking())
            {
                buttonText.text = "Start Markerless Tracking";
            }
            else
            {
                buttonText.text = "Stop Markerless Tracking";
            }
        }
    }
}
