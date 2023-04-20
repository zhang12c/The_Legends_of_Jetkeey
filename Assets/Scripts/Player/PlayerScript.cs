using System;
using System.Collections;
using UnityEngine;
namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int InputX = Animator.StringToHash("inputX");
        private static readonly int InputY = Animator.StringToHash("inputY");
        private static readonly int TurnLeft = Animator.StringToHash("turnLeft");
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
        public int amountOfJumps = 1;
        private int _amountOfJumpsLeft;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if (!_inputDisable)
            {
                PlayerInput();
            }
            else
            {
                // 不可输入的时候就不要跑步了
                _isMoving = false;
            }
            CheckIfCanJump();
            SwitchAnimator();
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
            _inputX = Input.GetAxis("Horizontal");
            _inputY = Input.GetAxis("Vertical");
            
            // 按下shift 的时候慢走
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

            if (Input.GetKeyDown(KeyCode.J))
            {
                _animator.SetTrigger(Atk);
                _inputDisable = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            
        }
        private void Jump()
        {
            if (_canJump)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,jumpForce);
            }
        }
        private void Movement()
        {
            _rigidbody2D.velocity = new Vector2(_inputX * moveSpeed,_rigidbody2D.velocity.y);
        }
        
        void SwitchAnimator()
        {
            if (_isMoving)
            {
                _animator.SetFloat(InputX,_inputX);
                _animator.SetFloat(InputY,_inputY);
            }
            _animator.SetBool(IsMoving,_isMoving);
            _animator.SetBool(TurnLeft,_turnLeft);
            
        }

        private void CheckSurroundings()
        {
            _isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }
    }
}
