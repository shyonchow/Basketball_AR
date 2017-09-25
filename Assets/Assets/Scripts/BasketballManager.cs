using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BasketballManager : MonoBehaviour
{
    private float _dist;
    private Vector3 _point;
    public Ball Ball;
    public GameObject Hoop;
    public float LogXFactor = 20;
    public float LogYFactor = 60;
    private Transform _origin;
    private Rigidbody _rb;
    public float Tolerance = 10f;
    public Text Info;

    void Start()
    {
        _origin = Ball.transform;
        _rb = Ball.GetComponent<Rigidbody>();
        Inputinit();
    }

    private void Inputinit()
    {

        var recognizerTap = new TKTapRecognizer();

        // we can limit recognition to a specific Rect, in this case the bottom-left corner of the screen
        recognizerTap.boundaryFrame = new TKRect(0, 0, Screen.width, Screen.height);

        // we can also set the number of touches required for the gesture
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            recognizerTap.numberOfTouchesRequired = 2;

        recognizerTap.gestureRecognizedEvent += Tap_gestureRecognizedEvent;
        TouchKit.addGestureRecognizer(recognizerTap);




        var recognizerSwipe = new TKSwipeRecognizer();
        recognizerSwipe.timeToSwipe = 0;
        recognizerSwipe.SetMinimumDistance(0.75f);
        //recognizerTap.addAngleRecogizedEvents (Recognizer_gestureRecognizedEvent, new Vector2(1,1), 45);

        recognizerSwipe.gestureRecognizedEvent += Swipe_gestureRecognizedEvent;
        TouchKit.addGestureRecognizer(recognizerSwipe);
    }


    void Swipe_gestureRecognizedEvent(TKSwipeRecognizer obj)
    {

        Debug.Log(obj.ToString());

        Vector3 start = obj.startPoint;
        Vector3 end = obj.endPoint;

        OnSwipe(start, end, obj.swipeTime);
    }

    void Tap_gestureRecognizedEvent(TKTapRecognizer obj)
    {

        Debug.Log(obj.ToString());


    }

    //    private Vector3 GetDistance() {
    //        RaycastHit hit = new RaycastHit();
    //
    //        if (Physics.Linecast(Ball.transform.position, Hoop.transform.position, out hit)) {
    //            _dist = hit.distance;
    //            return hit.point;
    //        }
    //
    //        return Vector3.zero;
    //    }


    public void OnSwipe(Vector3 startPos, Vector3 endPos, float time)
    {
        Ray ray = Camera.main.ScreenPointToRay(startPos);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit) && hit.collider == Ball.GetComponent<Collider>())
        {

            _point = Hoop.transform.position;
            _dist = Vector3.Distance(_origin.position, _point);

            float maxX = LogYFactor * Mathf.Log(LogXFactor * _dist) - 15.5f;
            float maxY = LogYFactor * Mathf.Log(LogXFactor * _dist) - 4.5f;
            //float maxZ = 5.5f;
            float minX = LogYFactor * Mathf.Log(LogXFactor * _dist) - 18f;
            float minY = LogYFactor * Mathf.Log(LogXFactor * _dist) - 7f;
            //float minZ = -5.5f;

            float speedX = 0.01f * Mathf.Lerp(_point.x - _origin.position.x, endPos.y - startPos.y, time) / time;
            float speedY = 0.03f * Mathf.Lerp(_point.y - _origin.position.y, endPos.y - startPos.y, time) / time;
            float speedZ = 0.075f * (endPos.x - startPos.x) / time;

            if (speedX > maxX) speedX = maxX;
            if (speedY > maxY) speedY = maxY;
            if (speedX < minX) speedX = minX;
            if (speedY < minY) speedY = minY;

            Vector3 velocity = new Vector3(-speedY, speedZ, speedX);

            _rb.useGravity = true;
            Ball.Launch(1);
            _rb.AddForce(velocity, ForceMode.VelocityChange);
        }

    }

        void Update() {
        	_dist = Vector3.Distance(_origin.position, Hoop.transform.position);
            Info.text = "Dist:" + _dist.ToString();
        }
}
