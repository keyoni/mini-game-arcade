using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Controls ship animations and sprite direction.
 */

namespace Dig_Down.Scripts
{
    public class ShipAnimation : MonoBehaviour
    {
        public Animator animator;
        private ShipController _ship;
        private ShipMining _shipMining;
        private static readonly int IsFlying = Animator.StringToHash("IsFlying");
        private static readonly int IsDigDown = Animator.StringToHash("IsDigDown");
        private static readonly int IsDigHorizontal = Animator.StringToHash("IsDigHorizontal");
        
        private bool _facingLeft;
        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;
        
        private bool _playIdle;
        private bool _playMining;
        private static readonly int IsHovering = Animator.StringToHash("isHovering");

        private void Start()
        {
            animator = GetComponent<Animator>();
            _ship = GetComponent<ShipController>();
            _shipMining = _ship.GetComponent<ShipMining>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            animator.SetBool(IsFlying, !_ship.isGrounded && !Input.GetButton("VerticalDownOnly"));
            animator.SetBool(IsDigHorizontal, Input.GetButton("Horizontal") && !Input.GetButton("VerticalDownOnly") && _shipMining.XRayHit);
            animator.SetBool(IsDigDown, _ship.isGrounded && Input.GetButton("VerticalDownOnly") && !Input.GetButton("Horizontal"));
            animator.SetBool(IsHovering, !_ship.isGrounded && Input.GetButton("VerticalDownOnly") ||
                                         !_ship.isGrounded && !Input.GetButton("VerticalDownOnly") && !Input.GetButton("Horizontal"));
            UpdateSpriteDirection();
        }

        private void UpdateSpriteDirection()
        {
            if (_ship.movement > 0 && _facingLeft)
            {
                _facingLeft = false;
                _spriteRenderer.flipX = true;
            }
            else if (_ship.movement < 0 && !_facingLeft)
            {
                _facingLeft = true;
                _spriteRenderer.flipX = false;
            }
        }
    }
}
