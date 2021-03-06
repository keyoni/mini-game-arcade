using Dig_Down.Scripts.Level_Generation;
using UnityEngine;

/*  Author: Alfredo Hernandez
 *  Description: Control animation state for blocks.
 */

namespace Dig_Down.Scripts
{
    public class BlockAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Block _block;
        private static readonly int Health = Animator.StringToHash("health");
        private static readonly int IsDirt = Animator.StringToHash("isDirt");
        private static readonly int IsStone = Animator.StringToHash("isStone");
        private static readonly int IsCopper = Animator.StringToHash("isCopper");
        private static readonly int IsIron = Animator.StringToHash("isIron");
        private static readonly int IsGold = Animator.StringToHash("isGold");
        private static readonly int IsDiamond = Animator.StringToHash("isDiamond");
        private static readonly int IsObsidian = Animator.StringToHash("isObsidian");
        private static readonly int IsCobalt = Animator.StringToHash("isCobalt");
        private static readonly int IsEmerald = Animator.StringToHash("isEmerald");
        private static readonly int IsMythril = Animator.StringToHash("isMythril");
        private static readonly int IsRuby = Animator.StringToHash("isRuby");
        private static readonly int IsSapphire = Animator.StringToHash("isSapphire");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _block = GetComponent<Block>();
        }

        private void Update()
        {
            _animator.SetFloat(Health, _block.health);

            switch (_block.blockName)
            {
                case "Dirt":
                    _animator.SetBool(IsDirt, true);
                    break;
                case "Stone":
                    _animator.SetBool(IsStone, true);
                    break;
                case "Copper":
                    _animator.SetBool(IsCopper, true);
                    break;
                case "Iron":
                    _animator.SetBool(IsIron, true);
                    break;
                case "Gold":
                    _animator.SetBool(IsGold, true);
                    break;
                case "Diamond":
                    _animator.SetBool(IsDiamond, true);
                    break;
                case "Obsidian":
                    _animator.SetBool(IsObsidian, true);
                    break;
                case "Cobalt":
                    _animator.SetBool(IsCobalt, true);
                    break;
                case "Emerald":
                    _animator.SetBool(IsEmerald, true);
                    break;
                case "Mythril":
                    _animator.SetBool(IsMythril, true);
                    break;
                
                case "Ruby":
                    _animator.SetBool(IsRuby, true);
                    break;
                case "Sapphire":
                    _animator.SetBool(IsSapphire, true);
                    break;
            }
        }
    }
}
