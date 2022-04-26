using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
        
        private float _mineElapsedTime;
        private float _mineSeconds;
        private float _mineDelayElapsed;
        private float _mineDelayMs;
        
        private int _layerMask;
        private float _yShipBound;
        private float _xShipBound;
        
        private Vector3 _shipPos;
        private Block _block;

        private Vector3 _surfacePos;

        private RaycastHit2D _blockHit;
        private RaycastHit2D _groundedHit;
        private bool _mineDown;
        
        // Spawn text when mining block showing block name/value
        public GameObject blockTextObject;
        private TextMeshProUGUI _blockText;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _layerMask = LayerMask.GetMask("blocks");
            _yShipBound = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
            _xShipBound = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            _surfacePos = FindObjectOfType<FuelStation>().transform.position;
        }
        
        private void Update()
        {
            // TODO: Based on ship velocity add hull damage on fall.
            CalculateDepth();
            ShipFlying();
            
            // Only mine when ship is grounded
            _groundedHit = Physics2D.Raycast(_shipPos, Vector2.down, _yShipBound, _layerMask);
            
            if (_groundedHit)
            {
                if (Input.GetButton("Horizontal") && !ShipFlying() && !_blockHit)
                {
                    _mineDown = false;
                    var rayDirection = new Vector3(_movement, 0f, 0f);
                    _blockHit = Physics2D.Raycast(_shipPos, rayDirection, _xShipBound, _layerMask);
                }
                else if (Input.GetKey(KeyCode.S) && !ShipFlying() && !Input.GetButton("Horizontal") && !_blockHit)
                {
                    _mineDown = true;
                    _blockHit = _groundedHit;
                }
            }
            
            if (_blockHit)
            {
                MineBlock(_shipPos, _mineDown, _blockHit);
            }
            else
            {
                _movement = Input.GetAxis("Horizontal");
                transform.position += new Vector3(_movement * speed * Time.deltaTime, 0, 0);
                _shipPos = transform.position;
            }
        }

        private void FixedUpdate()
        {
            if (ShipFlying())
            {
                _rb.AddForce(new Vector2(0f, upForce), ForceMode2D.Impulse);
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
            return Input.GetButton("Jump") && !_blockHit;
        }

        private void MineTimer()
        {
            _mineElapsedTime += Time.deltaTime;
                
            if (_mineElapsedTime > 0.01f)
            {
                _mineSeconds += 0.01f;
                _mineElapsedTime = 0f;
            }
        }

        private void ResetMineTimer()
        {
            _mineElapsedTime = 0f;
            _mineSeconds = 0f;
        }

        private void MineBlock(Vector3 playerPos, bool mineDown, RaycastHit2D hit)
        {
            var hitScale = hit.transform.localScale;
            var hitPos = hit.transform.position;

            if (mineDown)
            {
                var hitObstacle = hit.collider.CompareTag("obstacle");

                if (hitObstacle)
                {
                    _blockHit = new RaycastHit2D();
                    return;
                }
                
                if (hit)
                {
                    _block = hit.transform.gameObject.GetComponent<Block>();
                    MineTimer();
                    if (_mineSeconds >= _block.timeToMineBlock)
                    {
                        hit.transform.position = new Vector3(hitPos.x, hitPos.y - 0.075f, hitPos.z);
                        hit.transform.localScale = new Vector3(hitScale.x, hitScale.y - 0.1f, hitScale.z);
                        transform.position = new Vector3(hitPos.x, hit.collider.bounds.max.y + 0.3f, _shipPos.z);
                        ResetMineTimer();
                        ProcessMinedBlock(_block);
                    }
                }
            }

            if (hit && !mineDown)
            {
                _block = hit.transform.gameObject.GetComponent<Block>();
                MineTimer();
                if (_mineSeconds >= _block.timeToMineBlock)
                {
                    hit.transform.position = new Vector3(hitPos.x + (0.085f * _movement), hitPos.y, hitPos.z);
                    hit.transform.localScale = new Vector3(hitScale.x - 0.1f, hitScale.y, hitScale.z);
                    ResetMineTimer(); 

                    // If movement -1 (left) get bounds.extents.max.x
                    // Else movement 1 (right) get bounds.extents.min.x
                    if (_movement < 0)
                    {
                        transform.position = new Vector3(hit.collider.bounds.max.x + 0.35f, _shipPos.y, _shipPos.z);
                    }
                    else
                    {
                        transform.position = new Vector3(hit.collider.bounds.min.x - 0.35f, _shipPos.y, _shipPos.z);
                    }
                    
                    ProcessMinedBlock(_block);
                }
            }
        }

        private void ProcessMinedBlock(Block block)
        {
            if (_block.transform.localScale.y <= 0.1f || _block.transform.localScale.x <= 0.1f)
            {
                var blockValue = Instantiate(blockTextObject, _shipPos, Quaternion.identity, transform);
                _blockText = blockValue.GetComponentInChildren<TextMeshProUGUI>();
                _blockText.text = $"{_block.value}";
                _blockText.color = Color.green;
                Destroy(blockValue, 0.5f);

                if (_block.value > 25)
                {
                    var namePos = new Vector3(_shipPos.x, _shipPos.y - 0.5f, _shipPos.z);
                    var blockName = Instantiate(blockTextObject, namePos, Quaternion.identity, transform);
                    _blockText = blockName.GetComponentInChildren<TextMeshProUGUI>();
                    _blockText.text = $"+1 {_block.blockName}";
                    _blockText.color = Color.yellow;
                    Destroy(blockName, 0.5f);
                }

                score += _block.value;
                _blockHit = new RaycastHit2D();
                Destroy(_block.transform.gameObject);
            }
        }
    }
}
