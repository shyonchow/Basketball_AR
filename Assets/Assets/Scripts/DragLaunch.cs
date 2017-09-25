using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {
    private Vector3 startDrag, endDrag;
    private float startTime, endTime;
    private Ball ball;
    public GameObject target;
    public Vector3 h_point;
    private float h_dist;
    public Text info;
    private float roughDiff;


    void Start() {
        roughDiff = 10f;
        ball = GetComponent<Ball>();
    }


    public void DragEnd() {
        float deltaTime = endTime - startTime;
        h_point = GetDistance();
        Vector3 origin = ball.origin.position;
        //Debug.Log("time delta= " + deltaTime);

        float launchSpeed_z = 0.003f * (endDrag.x - startDrag.x) / deltaTime;
        //Debug.Log("Pre alter z:" + launchSpeed_z);
        float launchSpeed_x = 0.01f *
                              Mathf.Lerp(h_point.x - origin.x,
                                         endDrag.y - startDrag.y,
                                         deltaTime) /
                              deltaTime;
        float launchSpeed_y = 0.03f *
                              Mathf.Lerp(h_point.y - origin.y,
                                         endDrag.y - startDrag.y,
                                         deltaTime) /
                              deltaTime;
        //2.5f*Mathf.Sqrt(launchSpeed_x * launchSpeed_x+launchSpeed_x); //+ 3.5f * launchSpeed_x;
        //Debug.Log("X: " + h_point.x);
        //Debug.Log("Y: " + h_point.y);


        if (launchSpeed_y > 60 * Mathf.Log(20f * h_dist) - 4.5f) {
            launchSpeed_y = 60 * Mathf.Log(20f * h_dist) - 4.5f;
        }

        if (launchSpeed_y < 60 * Mathf.Log(20f * h_dist) - 7f) { launchSpeed_y = 60 * Mathf.Log(20f * h_dist) - 7f; }

        if (launchSpeed_x > 60 * Mathf.Log(20f * h_dist) - 15.5f) {
            launchSpeed_x = 60 * Mathf.Log(20f * h_dist) - 15.5f;
        }

        if (launchSpeed_x < 60 * Mathf.Log(20f * h_dist) - 18f) { launchSpeed_x = 60 * Mathf.Log(20f * h_dist) - 18f; }

        if (60 * Mathf.Log(20 * h_dist) < 15.5f) { launchSpeed_x = 60 * Mathf.Log(20 * h_dist); }

        if (launchSpeed_z > 5.5f) { launchSpeed_z = 5.2f; }

        if (launchSpeed_z < -5.5f) { launchSpeed_z = -5.2f; }

        if (h_point.x > -16) { launchSpeed_x += 1; }

        if (h_point.y < 2) { launchSpeed_x += 2; }


        //Debug.Log("x speed: " + launchSpeed_x);
        //Debug.Log("y speed: " + launchSpeed_y);
        //Debug.Log("z speed: " + launchSpeed_z);

        Vector3 launchVelocity = new Vector3(-launchSpeed_y,
                                             launchSpeed_z,
                                             launchSpeed_x);

        //ball.Launch(launchVelocity);
    }

    Vector3 GetDistance() {
        RaycastHit hit = new RaycastHit();

        if (Physics.Linecast(ball.transform.position,
                             target.transform.position,
                             out hit)) {
            Debug.Log("Object hit: " + hit.collider);
            h_dist = hit.distance;
            return hit.point;
        }

        Debug.Log("No rb hit");

        return Vector3.zero;
    }

//    void OnMouseDown() {
//        startDrag = Input.mousePosition;
//        startTime = Time.time;
//    }
//
//    void OnMouseUp() {
//        //Debug.Log("DELTA: " + (endDrag.y - startDrag.y).ToString());
//        endDrag = Input.mousePosition;
//        //Debug.Log("event drag" + endDrag);
//        endTime = Time.time;
//        if (endDrag.y != startDrag.y) { DragEnd(); }
//    }

    void Update() {
        //Debug.DrawLine(ball.transform.position, target.transform.position, Color.red);
        GetDistance();
        SwipeHandler();
        info.text = h_dist.ToString();
    }

    void SwipeHandler() {
        if (Input.touchCount == 1) {
            //Store input

            Touch fing = Input.GetTouch(0);
            Vector3 startPoint = fing.position;
            Ray ray = Camera.main.ScreenPointToRay(startPoint);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo)) {
                Debug.Log("hitInfo on launch: " + hitInfo.collider);
                if (hitInfo.collider == ball.GetComponent<Collider>()) {
                    if (fing.phase == TouchPhase.Began
                    ) //If the finger started touching the screen this frame
                    {
                        if (EventSystem.current.IsPointerOverGameObject(fing.fingerId)
                        ) {
                            startTime = Time.time;
                            startDrag =
                                fing
                                    .position;
                        } //Get the screen position of the finger when it hit the screen
                    } else if (fing.phase == TouchPhase.Ended
                    ) //If the finger stopped touching the screen this frame
                    {
                        endTime = Time.time;
                        endDrag =
                            fing
                                .position; //Get the screen position of the finger when it left the screen

                        if (Mathf.Abs(endDrag.magnitude - startDrag.magnitude) > roughDiff
                        ) //Calculate how far away the finger was from its starting point when it left the screen
                        {
                            DragEnd();
                        }
                    }
                }
            }
        }
    }

/*void OnMouseDrag ()
{
    float launch_x = 4.9f;
    float launch_y = 17.6f;
    float launch_z = 0;

    launch_z = 0.002f* / Time.deltaTime;

    Vector3 launchVelocity = new Vector3 (-launch_x, launch_y, launch_z);

    ball.Launch(launchVelocity);
}*/
}
