using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Description: Controls ship movement and fall damage
 */

namespace Dig_Down.Scripts
{
    public class ShipController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public float fuel = 100f;
        public int depth;
        public float health = 100f;
        public float maxHealth = 150f;
        public float score;
        public float maxFuel = 200f;
        public float speed = 3f;
        public float flyForce = 0.22f;
        public float maxVelocity = 8f;
        public float fuelLostPerTick = 2f;
        public float movement;

        public bool isGrounded;

        private ShipMining _shipMining;

        public float fallDamageMultiplier = 0.95f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _shipMining = GetComponent<ShipMining>();
            Dig_Down.Scripts.Managers.AudioManager.StopSound("shipIdle");
        }

        private void Update()
        {
            isGrounded = _shipMining.YRayHit;

            if (isGrounded)
            {
                if (_rb.velocity.magnitude >= maxVelocity)
                {
                    health -= fallDamageMultiplier * _rb.velocity.magnitude;
                }
            }
            
            movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * (speed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                _rb.AddForce(new Vector2(0f, flyForce), ForceMode2D.Impulse);
            }

            if (_rb.velocity.y > maxVelocity)
            {
                // Clamp the max velocity (useful for not moving too fast)
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxVelocity);
            }
        }
    }
}