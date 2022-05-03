using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject PowerMeter;
    public float HitForce;
    public float powerMeterSpeed;

    private Rigidbody2D rb;
    private bool arrowActive = false;
    private bool meterActive = false;
    private Vector2 dir;
    private int powerMeterDir = 1;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (rb.velocity.magnitude < 0.025f) {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            // if (arrowActive && !meterActive) {
            //     Arrow.SetActive(false);
            //     arrowActive = false;
            // } else if (rb.velocity.x == 0f && rb.velocity.y == 0f) {
            //     Arrow.SetActive(true);
            //     arrowActive = true;
            // }
            if (rb.velocity.x == 0f && rb.velocity.y == 0f) {
                Arrow.SetActive(true);
                arrowActive = true;
            }
        }

        if (arrowActive && !meterActive) {
            Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldMousePosition = new Vector3();
                worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            dir = new Vector2(worldMousePosition.x-transform.position.x, worldMousePosition.y-transform.position.y);
            dir.Normalize();
            transform.up = dir;

            // if (Input.GetMouseButtonDown(0)) {
            //     rb.AddForce(dir*HitForce, ForceMode2D.Impulse);
            //     arrowActive = false;
            //     Arrow.SetActive(false);
            //     FindObjectOfType<Scoring>().addStroke();
            // }

            if (Input.GetMouseButtonDown(0)) {
                meterActive = true;
                PowerMeter.SetActive(true);
            }
        }

        if (meterActive) {
            Slider slide = PowerMeter.GetComponent<Slider>();
            if (slide.value > 0.95f) {
                powerMeterDir *= -1;
                slide.value = 0.95f;
            } else if (slide.value < 0.05f) {
                powerMeterDir *= -1;
                slide.value = 0.05f;
            }
            slide.value += powerMeterDir*(Time.deltaTime * powerMeterSpeed);
            //Debug.Log(slide.value);

            if (Input.GetMouseButtonDown(1)) {
                FindObjectOfType<GolfAudio>().playSound("swing");
                
                float powerMult = slide.value;
                rb.AddForce(dir*HitForce*powerMult, ForceMode2D.Impulse);
                arrowActive = false;
                Arrow.SetActive(false);
                meterActive = false;
                PowerMeter.SetActive(false);
                FindObjectOfType<Scoring>().addStroke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Flag")) {
            FindObjectOfType<GolfAudio>().playSound("ball-in-hole");
            Debug.Log("Goal!");
            FindObjectOfType<Scoring>().completeRound();
        }
    }
}
