using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public GameObject Arrow;
    public float HitForce;

    private Rigidbody2D rb;
    private bool arrowActive = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (arrowActive) {
                Arrow.SetActive(false);
                arrowActive = false;
            } else if (rb.velocity.x <= 0.001f && rb.velocity.y <= 0.001f) {
                Arrow.SetActive(true);
                arrowActive = true;
            }
        }

        if (arrowActive) {
            Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldMousePosition = new Vector3();
                worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 dir = new Vector2(worldMousePosition.x-transform.position.x, worldMousePosition.y-transform.position.y);
            transform.up = dir;

            if (Input.GetMouseButtonDown(0)) {
                rb.AddForce(dir*HitForce, ForceMode2D.Impulse);
                arrowActive = false;
                Arrow.SetActive(false);
                FindObjectOfType<Scoring>().addStroke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Flag")) {
            Debug.Log("Goal!");
            FindObjectOfType<Scoring>().completeRound();
        }
    }
}
