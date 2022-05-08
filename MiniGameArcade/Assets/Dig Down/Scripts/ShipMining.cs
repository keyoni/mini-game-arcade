using Dig_Down.Scripts.Level_Generation;
using Dig_Down.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Process mined blocks. Determine which block should be mined using raycasts.
 */

namespace Dig_Down.Scripts
{
    public class ShipMining : MonoBehaviour
    {
        private int _layerMask;
        private float _yShipBound;
        private float _xShipBound;

        public RaycastHit2D XRayHit;
        public RaycastHit2D YRayHit;
        
        private ShipController _ship;
        private Vector3 _shipPos;
        private Block _block;
    
        public delegate void BlockMined(Block block, Vector3 shipPos);
        public event BlockMined OnBlockMined;

        private Animator _minedBlockAnimator;

        public int drillDamageMultiplier = 1;

        private GameManager _gameManager;

        private void Start()
        {
            _ship = GetComponent<ShipController>();
            _layerMask = LayerMask.GetMask("blocks");
            _yShipBound = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
            _xShipBound = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }

        private void Update()
        {
            _shipPos = transform.position;
            YRayHit = Physics2D.Raycast(_shipPos, Vector2.down, _yShipBound, _layerMask);
            XRayHit = Physics2D.Raycast(_shipPos, new Vector2(_ship.movement, 0f), _xShipBound, _layerMask);

            if (XRayHit && Input.GetButton("Horizontal") && YRayHit && !Input.GetButton("VerticalDownOnly"))
            {
                StartMiningBlock(XRayHit, false);
            } else if (YRayHit && Input.GetButton("VerticalDownOnly") && !Input.GetButton("Horizontal"))
            {
                StartMiningBlock(YRayHit, true);
            }
        }

        private void StartMiningBlock(RaycastHit2D hit, bool mineDown)
        {
            if (hit)
            {
                if (hit.collider.CompareTag("obstacle"))
                {
                    XRayHit = new RaycastHit2D();
                    YRayHit = new RaycastHit2D();
                    return;
                }
                //Debug.Log("HIT BLOCK");
                DamageBlock(hit, mineDown);
            }
        }

        private void DamageBlock(RaycastHit2D hit, bool mineDown)
        {
            var hitPos = hit.transform.position;
            _block = hit.transform.gameObject.GetComponent<Block>();
            if (_block.health > 0f)
            {
                _block.health -= (drillDamageMultiplier * _block.damageMultiplier) * Time.deltaTime;
            }
            else
            {
                _block.health = 0f;
            }
            
            if (mineDown)
            {
                // Center the ship over the block when mining down.
                transform.position = new Vector3(hitPos.x, hit.collider.bounds.max.y + 0.3f, _shipPos.z);
            }
            else
            {
                // Position horizontally so that ship doesn't clip against block when mining.
                var bounds = hit.collider.bounds;
                var left = new Vector3(bounds.max.x + 0.40f, _shipPos.y, _shipPos.z);
                var right = new Vector3(bounds.min.x - 0.40f, _shipPos.y, _shipPos.z);
                transform.position = _ship.movement < 0 ? left : right;
            }
            
            // Only add animator component to blocks being mined (increases performance)
            if (hit.transform.GetComponent<Animator>() == null)
            {
                hit.transform.AddComponent<Animator>();
                _minedBlockAnimator = hit.transform.GetComponent<Animator>();
                _minedBlockAnimator.runtimeAnimatorController = (RuntimeAnimatorController)
                    Resources.Load("DigDown/Animations/Blocks/BlockAnimator", typeof(RuntimeAnimatorController));
                hit.transform.AddComponent<BlockAnimation>();
            }
            
            DestroyBlock();
        }

        private void DestroyBlock()
        {
            if (_block.health > 0) return;
            OnBlockMined?.Invoke(_block, _shipPos);
            _gameManager.timeLeft += _block.timeValue;
            _ship.score += _block.value;
            Dig_Down.Scripts.Managers.AudioManager.StopSound("shipDrill");
            Destroy(_block.gameObject); // Added a time delay to makeup for coroutine wait
        }
    }
}
