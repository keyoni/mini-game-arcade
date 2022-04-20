using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Digging_Game.Scripts
{
    public class ShipController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public float fuel = 100f;
        public int depth = 0;
        public float score = 0f;
        public float maxFuel = 200f;
        public float speed = 3f;
        public float upForce = 0.22f;
        public float fuelLostPerTick = 2f;

        private float _movement;
        
        private float _keyHeldTime;
        private float _keyHeldSeconds;
        
        private bool _blockRayCollision;
        private int _layerMask;
        private float _yShipBound;
        private float _xShipBound;
        
        private Vector3 _shipPos;
        private Block _block;

        private Vector3 _surfacePos;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _layerMask = LayerMask.GetMask("blocks");
            _yShipBound = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
            _xShipBound = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            _surfacePos = FindObjectOfType<FuelStation>().transform.position;
        }
        
        private void Update()
        {
            // TODO: Based on ship velocity add hull damage on fall.
            CalculateDepth();
            ShipFlying();

            if (!_blockRayCollision)
            {
                _movement = Input.GetAxis("Horizontal");
                transform.position += new Vector3(_movement * speed * Time.deltaTime, 0, 0);
                _shipPos = transform.position;
            }
            
            // New digging mechanic
            // Ship cannot dig and fly at the same time
            // TODO: Cannot dig block below if there is half a block above it.
            if (Input.GetButton("Horizontal") && !ShipFlying())
            {
                var rayDirection = new Vector3(_movement, 0f, 0f);
                Debug.DrawRay(_shipPos, rayDirection, Color.black);
                RaycastHit2D hit = Physics2D.Raycast(_shipPos, rayDirection, _xShipBound, _layerMask);
                MineBlock(_shipPos, false, hit);
            }
            else if (Input.GetKey(KeyCode.S) && !ShipFlying() && !Input.GetButton("Horizontal"))
            {
                var hit = Physics2D.Raycast(_shipPos, Vector2.down, _yShipBound, _layerMask);
                MineBlock(_shipPos, true, hit);
            }
        }

        private void CalculateDepth()
        {
            if (_shipPos.y < _surfacePos.y)
            {
                var difference = _surfacePos.y - transform.position.y;
                depth = (int) (difference * 12f);
            } else if (_shipPos.y >= _surfacePos.y)
            {
                depth = 0;
            }
        }

        private bool ShipFlying()
        {
            if (Input.GetButton("Jump"))
            {
                _blockRayCollision = false;
                _rb.AddForce(new Vector2(0f, upForce), ForceMode2D.Impulse);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void KeyHeldTimer()
        {
            _keyHeldTime += Time.deltaTime;
                
            if (_keyHeldTime > 0.01f)
            {
                _keyHeldSeconds += 0.01f;
                _keyHeldTime = 0f;
            }
        }

        private void ResetKeyTimer()
        {
            _keyHeldTime = 0f;
            _keyHeldSeconds = 0f;
        }

        private void MineBlock(Vector3 playerPos, bool mineDown, RaycastHit2D hit)
        {
            // TODO: Remove duplicate code.
            _blockRayCollision = false;
            if (mineDown)
            {
                Debug.DrawRay(playerPos, Vector3.down, Color.black);
                if (hit)
                {
                    _blockRayCollision = true;
                    _block = hit.transform.gameObject.GetComponent<Block>();
                    KeyHeldTimer();
                    if (_keyHeldSeconds >= _block.timeToMineBlock)
                    {
                        var hitScale = hit.transform.localScale;
                        var hitPos = hit.transform.position;
                        hit.transform.position = new Vector3(hitPos.x, hitPos.y - 0.05f, hitPos.z);
                        hit.transform.localScale = new Vector3(hitScale.x, hitScale.y - 0.1f, hitScale.z);
                        transform.position = new Vector3(hitPos.x, _shipPos.y - 0.1f, _shipPos.z);
                        ResetKeyTimer();
                        
                        if (_block.transform.localScale.y <= 0.1f)
                        {
                            score += _block.value;
                            Destroy(_block.transform.gameObject);
                        }
                    }
                }
                return;
            }

            if (hit)
            {
                _blockRayCollision = true;
                _block = hit.transform.gameObject.GetComponent<Block>();
                KeyHeldTimer();
                if (_keyHeldSeconds >= _block.timeToMineBlock)
                {
                    var hitScale = hit.transform.localScale;
                    var hitPos = hit.transform.position;
                    hit.transform.position = new Vector3(hitPos.x + (0.05f * _movement), hitPos.y, hitPos.z);
                    hit.transform.localScale = new Vector3(hitScale.x - 0.1f, hitScale.y, hitScale.z);
                    transform.position = new Vector3(_shipPos.x + (0.05f * _movement), _shipPos.y, _shipPos.z);
                    ResetKeyTimer();
                        
                    if (_block.transform.localScale.x <= 0.1f)
                    {
                        score += _block.value;
                        Destroy(_block.transform.gameObject);
                    }
                }
            }
        }
    }
}
