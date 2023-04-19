using System;
using System.Collections;
using UnityEngine;
namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        private float _inputX;
        private float _inputY;
        private bool _turnLeft;
        private bool _isMoving;

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        // xy 合成的一个向量，人物朝向
        private Vector2 _movementInput;
        public float moveSpeed;
        
        
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int InputX = Animator.StringToHash("inputX");
        private static readonly int InputY = Animator.StringToHash("inputY");
        private static readonly int TurnLeft = Animator.StringToHash("turnLeft");
        private static readonly int Atk = Animator.StringToHash("atk");
        private bool _inputDisable;

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
            SwitchAnimator();
        }
        
        private void FixedUpdate()
        {
            if (!_inputDisable)
            {
                Movement();
            }
        }

        private void PlayerInput()
        {
            _inputX = Input.GetAxis("Horizontal");
            _inputY = Input.GetAxis("Vertical");
            
            // 按下shift 的时候慢走
            if (!Input.GetKey(KeyCode.LeftShift))
                _inputX *= 0.5f;
            
            _movementInput = new Vector2(_inputX, 0);
            _isMoving = _movementInput != Vector2.zero;
            
            if (_inputX != 0)
            {
                _turnLeft = _inputX < 0;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                _animator.SetTrigger(Atk);
                _inputDisable = false;
            }
            
        }
        private void Movement()
        {
            _rigidbody2D.velocity = _movementInput * moveSpeed;
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
    }
}
