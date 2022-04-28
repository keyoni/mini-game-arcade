using TMPro;
using UnityEngine;

namespace Digging_Game.Scripts
{
    public class ShipController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        
        public float fuel = 100f;
        public int depth;
        public float health = 100f;
        public float score;
        public float maxFuel = 200f;
        public float speed = 3f;
        public float flyForce = 0.22f;
        public float maxVelocity = 8f;
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
        
        private RaycastHit2D _blockHit;
        private bool _mineDown;

        private bool _facingLeft;
        private SpriteRenderer _spriteRenderer;
        
        public delegate void BlockMined(Block block, Vector3 shipPos);
        public event BlockMined OnBlockMined;

        // TODO: Can use coroutine instead instead of timer
        // TODO: Give visual/sound feedback when health is lost

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _layerMask = LayerMask.GetMask("blocks");
            _yShipBound = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
            _xShipBound = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Update()
        {
            // Raycast to check if ship is not floating before mining
            if (Physics2D.Raycast(_shipPos, Vector2.down, _yShipBound, _layerMask) && !_blockHit)
            {
                if (_rb.velocity.magnitude >= maxVelocity)
                {
                    // Fall damage
                    health -= 1.2f * _rb.velocity.magnitude;
                }
                
                if (Input.GetButton("Horizontal"))
                {
                    _mineDown = false;
                    _blockHit = Physics2D.Raycast(_shipPos, new Vector2(_movement, 0f), _xShipBound, _layerMask);
                }
                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    _mineDown = true;
                    _blockHit = Physics2D.Raycast(_shipPos, Vector2.down, _yShipBound, _layerMask);
                }
            }
            
            if (_blockHit)
            {
                MineBlock(_mineDown, _blockHit);
            }
            else
            {
                _movement = Input.GetAxis("Horizontal");
                transform.position += new Vector3(_movement, 0, 0) * (speed * Time.deltaTime);
                _shipPos = transform.position;
                UpdateShipDirection();
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetButton("Jump"))
            {
                _rb.AddForce(new Vector2(0f, flyForce), ForceMode2D.Impulse);
            }

            if (_rb.velocity.y > maxVelocity)
            {
                // Clamp the max velocity (useful for not moving too fast)
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxVelocity);
            }
        }

        // Update the direction of ship's sprite
        private void UpdateShipDirection()
        {
            if (_movement > 0 && _facingLeft)
            {
                _facingLeft = false;
                _spriteRenderer.flipX = true;
            } else if (_movement < 0 && !_facingLeft)
            {
                _facingLeft = true;
                _spriteRenderer.flipX = false;
            }
        }

        private void StartMiningTimer()
        {
            _mineElapsedTime += Time.deltaTime;
                
            if (_mineElapsedTime > 0.01f)
            {
                _mineSeconds += 0.01f;
                _mineElapsedTime = 0f;
            }
        }

        private void ResetMiningTimer()
        {
            _mineElapsedTime = 0f;
            _mineSeconds = 0f;
        }

        private void MineBlock(bool mineDown, RaycastHit2D hit)
        {
            var hitScale = hit.transform.localScale;
            var hitPos = hit.transform.position;

            if (hit && mineDown) // Vertical mining
            {
                if (hit.collider.CompareTag("obstacle"))
                {
                    _blockHit = new RaycastHit2D();
                    return;
                }
                
                StartMiningTimer();
                _block = hit.transform.gameObject.GetComponent<Block>();
                if (_mineSeconds >= _block.timeToMineBlock)
                {
                    hit.transform.position = new Vector3(hitPos.x, hitPos.y - 0.075f, hitPos.z);
                    hit.transform.localScale = new Vector3(hitScale.x, hitScale.y - 0.1f, hitScale.z);
                    transform.position = new Vector3(hitPos.x, hit.collider.bounds.max.y + 0.3f, _shipPos.z);
                    ResetMiningTimer();
                    ProcessMinedBlock();
                }
            } 
            else // Horizontal mining
            {
                StartMiningTimer();
                _block = hit.transform.gameObject.GetComponent<Block>();
                if (_mineSeconds >= _block.timeToMineBlock)
                {
                    // TODO: Block should move in opposite position from where it's being mined
                    hit.transform.position = new Vector3(hitPos.x + (0.015f * _movement), hitPos.y, hitPos.z);
                    hit.transform.localScale = new Vector3(hitScale.x - 0.1f, hitScale.y, hitScale.z);
                    ResetMiningTimer();
                    ProcessMinedBlock();

                    // If movement -1 (left) get bounds.extents.max.x
                    // Else movement 1 (right) get bounds.extents.min.x
                    if (_movement < 0)
                    {
                        transform.position = new Vector3(hit.collider.bounds.max.x + 0.40f, _shipPos.y, _shipPos.z);
                    }
                    else
                    {
                        transform.position = new Vector3(hit.collider.bounds.min.x - 0.40f, _shipPos.y, _shipPos.z);
                    }
                }
            }
        }
        
        private void ProcessMinedBlock()
        {
            if (_block.transform.localScale.y <= 0.1f || _block.transform.localScale.x <= 0.1f)
            {
                score += _block.value;
                _blockHit = new RaycastHit2D();
                OnBlockMined?.Invoke(_block, _shipPos);
                Destroy(_block.transform.gameObject);
            }
        }
    }
}