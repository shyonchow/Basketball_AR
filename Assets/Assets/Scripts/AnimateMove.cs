using UnityEngine;
using System.Collections;
using Kudan.AR;

public class AnimateMove : MonoBehaviour
{
    private Transform hoop;
    //private float speed = 125f;
    int k = 0;
    private int j;
    public Ball ball;
    private Camera cam;
    //private Vector3 temp; 
    private Vector3 temp2;
    private Vector3 startPos;
    private Vector3 endPos;
    float MoveDistance = 20f;
    float LerpTime = 1f;
    float CurrentLerpTime;

    //	public GameObject hoop1;
    //	public GameObject hoop2;


    void Start()
    {
        hoop = GetComponent<Transform>();
        cam = Camera.main;
        Debug.Log("Screen width: " + cam.pixelWidth);
        startPos = hoop.position;

    }

    //	void Rotate ()
    //	{
    //		// z == -x
    //		// y == -z
    //		// x == y
    //		Vector3 targetPos = new Vector3(cam.transform.position.x, transform.position.x, cam.transform.position.z);
    //        transform.LookAt(targetPos);
    //        hoop.position = new Vector3(temp.x, hoop.position.y, hoop.position.z);
    //	}

    void MoveLerp(int m)
    {
        CurrentLerpTime += Time.deltaTime;
        if (CurrentLerpTime > LerpTime)
        {
            CurrentLerpTime = LerpTime;
        }

        float t = CurrentLerpTime / LerpTime;
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        if (m == 1)
        {
            startPos = transform.position;
            endPos = transform.position + transform.up * MoveDistance;
            //cam.ScreenToWorldPoint (new Vector3 (startPos.x + 400, startPos.y, startPos.z));
            transform.position = Vector3.Lerp(startPos, endPos, t);
        }
        else if (m == 2)
        {
            startPos = transform.position;
            endPos = transform.position + transform.up * -MoveDistance;
            //cam.ScreenToWorldPoint (new Vector3 (startPos.x - 400, startPos.y, startPos.z));
            transform.position = Vector3.Lerp(startPos, endPos, t);
        }
        else if (m == 3)
        {
            startPos = transform.position;
            endPos = transform.position + transform.forward * -MoveDistance;
            //cam.ScreenToWorldPoint (new Vector3 (startPos.x, startPos.y, startPos.z-10));
            transform.position = Vector3.Lerp(startPos, endPos, t);
        }
    }

    void MoveBehaviour()
    {
        if (k * j == 1)
        {
            MoveLerp(k);
            //				transform.RotateAround(cam.transform.position, Vector3.up, 90);
            //				transform.Translate (0, speed * Time.deltaTime, 0);
            //				Rotate ();
            //				hoop1.SetActive(false);
            //				hoop2.SetActive(true);
        }
        else if (k * j == 2)
        {
            MoveLerp(k);
            //				transform.RotateAround(cam.transform.position, Vector3.up, -90);
            //				transform.Translate (0, -speed * Time.deltaTime, 0); 
            //				Rotate ();
            //				hoop1.SetActive(true);
            //				hoop2.SetActive(false);
        }
        else if (k * j == 3)
        {
            MoveLerp(k);
            //				if (temp.magnitude - transform.position.magnitude < 5) {
            //					transform.Translate (0, 0, -speed * Time.deltaTime);
            //				}
        }
    }

    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(hoop.position);
        if (screenPos.x <= 0.2f * cam.pixelWidth || screenPos.x >= cam.pixelWidth - 0.2f * cam.pixelWidth || screenPos.z >= 600)
        {
            Debug.Log("Off Screen at: " + screenPos);
            k = 0;
            j = 0;
        }
        else if (ball.count > 0 && ball.count % 2 == 0 && ball.count % 4 != 0 && ball.count % 6 != 0)
        {
            k = 1;
        }
        else if (ball.count > 0 && ball.count % 4 == 0 && ball.count % 6 != 0)
        {
            k = 2;
        }
        else if (ball.count > 0 && ball.count % 6 == 0)
        {
            k = 3;
        }

        MoveBehaviour();
        //			if (k == 1 && j == 0) {
        //				MoveLerp(1);
        //				transform.RotateAround(cam.transform.position, Vector3.up, 90);
        //				transform.Translate (0, speed * Time.deltaTime, 0);
        //				Rotate ();
        //				hoop1.SetActive(false);
        //				hoop2.SetActive(true);
        //			} else if (k == 2 && j == 0) {
        //				MoveLerp(2);
        //				transform.RotateAround(cam.transform.position, Vector3.up, -90);
        //				transform.Translate (0, -speed * Time.deltaTime, 0); 
        //				Rotate ();
        //				hoop1.SetActive(true);
        //				hoop2.SetActive(false);
        //			} else if (k == 3 && j == 0) {
        //				MoveLerp(3);
        //				if (temp.magnitude - transform.position.magnitude < 5) {
        //					transform.Translate (0, 0, -speed * Time.deltaTime);
        //				}
        //			}

        if (ball.count % 2 != 0 || ball.count == 0)
        {
            j = 1;
            k = 0;
        }
    }
}
