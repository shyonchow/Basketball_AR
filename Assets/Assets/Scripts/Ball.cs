using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour {
    private Rigidbody rb;
    public int count;
    private int shotFlag;
    public Text score;
    public AudioClip rim;
    public AudioClip board;
    public AudioClip goal;
    public AudioClip miss;
    public AudioClip swoosh;
    //    public GameObject fireworks;
    private AudioSource ballAudioSource;
    private int k;
    //    public Transform startPos;
    public Transform Target;
    public Transform bBall;
    public float theta = 45.0f;
    public float grav = 9.8f;
    public Detector detector1;
    public Detector detector2;
    public Detector detector3;
    public Detector detector4;
    private float success;
    private float total;
    public Transform origin;
    //    public ParticleSystem firework;
    private Camera cam;
    //    public BasketballManager Manager;
    public GameObject plus_one;
    private IEnumerator coroutine;
//    public Animation head;

    private void Start() {
//        head.playAutomatically = false;
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        count = 0;
        shotFlag = 0;
        origin.position = gameObject.transform.position;
        rb.useGravity = false;
        ballAudioSource = GetComponent<AudioSource>();
    }

    public void Launch(int launched) {
        if (launched == 1) {
            ++total;
            ballAudioSource.PlayOneShot(swoosh);
            //Debug.Log("GRAVITY:" + rb.useGravity);
            shotFlag = 0;
        }
    }


    //    void InstantiateFireworks() {
    //        // if(!firework.isPlaying)
    //        firework.Play();
    //
    //        //Instantiate(fireworks, startPos.position, startPos.rotation);
    //    }

    public void SetText() {
        k = 0;
        detector1.entered = false;
        detector2.entered = false;
        detector3.entered = false;
        detector4.entered = false;
        float accuracy = 100 * (success / total);
        if (shotFlag == 0) {
            ballAudioSource.PlayOneShot(miss);
            count = 0;
            score.text = "SCORE: 0" + "    " + accuracy + "%";
        } else { score.text = "SCORE:" + " " + count + "    " + accuracy + "%"; }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Rim")) { ballAudioSource.PlayOneShot(rim); }
        if (other.gameObject.CompareTag("Board")) { ballAudioSource.PlayOneShot(board); }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("BasketScore")) {
            k++;
            //            Debug.Log("k:" + k);
            //            Debug.Log(detector1.entered);
            //            Debug.Log(detector2.entered);
        }
    }

    //    void Update() {
    //        if (Input.GetKeyDown(KeyCode.A)) { plus_one.SetActive(true); }
    //    }

    private IEnumerator WaitAndDeactivate(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        plus_one.SetActive(false);
    }

    //    void OnCollisionEnter(Collision other) {
    //        if (other.gameObject.CompareTag("BasketScore")) {
    //            ++count;
    //            shotFlag = 1;
    //            Debug.Log(count);
    //            ballAudioSource.PlayOneShot(goal);
    //            InstantiateFireworks();
    //            SetText();
    //            k = 0;
    //        }
    //    }


    private void FixedUpdate() {
        //Debug.Log("GRAVITY: " + rb.useGravity);
        //Debug.Log(rb.transform.position.x);
        Vector3 screenPos = cam.WorldToScreenPoint(rb.transform.position);
        if (screenPos.y <= 0) {
            rb.transform.position = origin.position; //new Vector3(-8.96f, 1.8f, 0f);
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            SetText();
        }
    }

    private void Update() {
        if ((detector1.entered && detector2.entered) && k >= 1) {
            //Debug.Log(detector2.entered);
            ++count;
            ++success;
            shotFlag = 1;
            k = 0;
            detector1.entered = false;
            detector2.entered = false;
            detector3.entered = false;
            detector4.entered = false;
            //Debug.Log(count);
            ballAudioSource.PlayOneShot(goal);
            //            InstantiateFireworks();
            plus_one.SetActive(true);
            coroutine = WaitAndDeactivate(1.0f);
            StartCoroutine(coroutine);
            SetText();
        }


        //shotCount = 0;
    }
}
