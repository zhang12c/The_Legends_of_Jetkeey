using System;
using System.Collections;
using Entity.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int InputX = Animator.StringToHash("inputX");
        private static readonly int InputY = Animator.StringToHash("inputY");
        private static readonly int Atk = Animator.StringToHash("atk");
        
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        
        private float _inputX;
        private float _inputY;
        private bool _turnLeft;
        private bool _isMoving;
        
        private bool _inputDisable;
        public float moveSpeed;
        public float jumpForce;

        public Transform groundCheck;
        private bool _isGround;
        public LayerMask whatIsGround;
        public float groundCheckRadius;
        private bool _canJump;

        private static readonly int VelocityY = Animator.StringToHash("velocityY");
        private static readonly int IsGround = Animator.StringToHash("isGround");
        private static readonly int Hurt = Animator.StringToHash("hurt");
        
        private Attack _weaponAttack;
        
        
        [Header("技能冷却")]
        private bool _canAttack;
        public float attackCd;
        private float _currentCdCounter;
        public Image skillPic;
        
        [Header("怪物剩余的判断")]
        public UnityEvent OnGameEnd;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _weaponAttack = GetComponentInChildren<Attack>();
        }
        private void Update()
        {
            if (!_inputDisable)
            {
                PlayerInput();
                CheckIfCanJump();
                SwitchAnimator();
            }
            else
            {
                // 不可输入的时候就不要跑步了
                _isMoving = false;
            }
            
            if (!_canAttack)
            {
                skillPic.fillAmount = attackCd - _currentCdCounter;
                _currentCdCounter -= Time.deltaTime;
                if (_currentCdCounter <= 0)
                {
                    _canAttack = true;
                }
            }
        }

        private void LateUpdate()
        {
            var masters = GameObject.FindGameObjectsWithTag("Moster");
            if (masters.Length <= 0)
            {
                OnGameEnd?.Invoke();
            }
        }
        private void CheckIfCanJump()
        {
            if (_isGround )
            {
                _canJump = true;
            }
            else
            {
                _canJump = false;
            }
        }

        private void FixedUpdate()
        {
            if (!_inputDisable)
            {
                Movement();
            }
            CheckSurroundings();
        }

        private void PlayerInput()
        {
            //#if UNITY_EDITOR || UNITY_WEBGL || UNITY_64
            _inputX = Input.GetAxis("Horizontal");
            _inputY = Input.GetAxis("Vertical");
            //#endif
            // 按下shift 的时候跑步
            if (!Input.GetKey(KeyCode.LeftShift))
                _inputX *= 0.5f;
            
            if (_inputX != 0)
            {
                _isMoving = true;
                _turnLeft = _inputX < 0;
            }
            else
            {
                _isMoving = false;
            }

            // 攻击的判断
            if (Input.GetKeyDown(KeyCode.J) && _canAttack)
            {
                _animator.SetTrigger(Atk);
                UpdateAttackCd();
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            
            Fclip();

        }

        private void UpdateAttackCd()
        {
            if (_canAttack)
            {
                _canAttack = false;
                _currentCdCounter = attackCd;
            }
        }

        /// <summary>
        /// 左右切换一下
        /// </summary>
        private void Fclip()
        {
            if (_turnLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
        private void Jump()
        {
            if (_canJump)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,jumpForce);
            }
        }
        
        private void CheckSurroundings()
        {
            _isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }
        private void Movement()
        {
            _rigidbody2D.velocity = new Vector2(_inputX * moveSpeed,_rigidbody2D.velocity.y);
        }
        
        // 动画相关
        
        private void SwitchAnimator()
        {
            if (_isMoving)
            {
                _animator.SetFloat(InputX,_inputX);
                _animator.SetFloat(InputY,_inputY);
            }
            _animator.SetBool(IsMoving,_isMoving);
            _animator.SetFloat(VelocityY,_rigidbody2D.velocity.y);
            _animator.SetBool(IsGround,_isGround);
            
        }

        public void PlayHurt()
        {
            _animator.SetTrigger(Hurt);
        }

        public void PlayJump()
        {
            Jump();
        }
        
        public void PlayAttack()
        {
            if (_canAttack)
            {
                _animator.SetTrigger(Atk);
                UpdateAttackCd();
            }
            else
            {
                Debug.Log("冷却ing");
            }
            
        }
    }
}
