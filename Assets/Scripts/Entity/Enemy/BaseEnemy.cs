using System;
using UnityEngine;
namespace Entity.Enemy
{
    public class BaseEnemy : MonoBehaviour
    {
        // 巡逻的速度
        public float normalSpeed;
        // 追杀的速度
        public float followSpeed;
        // 现在的速度保留一下
        public float currentSpeed;

        protected Rigidbody2D Rb;
        protected Animator Animator;
        
        public Vector3 faceDir;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentSpeed = normalSpeed;
        }
        private void Update()
        {
            faceDir = new Vector3(-transform.localScale.x, 0, 0);
        }

        private void FixedUpdate()
        {
            Movement();
        }

        protected virtual void Movement()
        {
            Rb.velocity = new Vector2(currentSpeed * faceDir.x , Rb.velocity.y);
        }
    }
}