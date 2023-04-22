using System;
using UnitTool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
        
        private PhysicsCheckLayer _physicsCheckLayer;
        
        // 玩家
        public Transform attackTrans;
        
        private static readonly int Hurt = Animator.StringToHash("hurt");
        private static readonly int Dead = Animator.StringToHash("dead");
        
        [Header("警戒间隔")]
        private bool _canFollow;
        public float followTime;
        private float _currentFollowTimeCounter;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            _physicsCheckLayer = GetComponent<PhysicsCheckLayer>();
        }

        private void Start()
        {
            currentSpeed = normalSpeed;
        }
        private void Update()
        {
            faceDir = new Vector3(-transform.localScale.x, 0, 0);
            if (_physicsCheckLayer.isGround)
            {
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
            
            if (!_canFollow)
            {
                _currentFollowTimeCounter -= Time.deltaTime;
                if (_currentFollowTimeCounter <= 0)
                {
                    _canFollow = true;
                    currentSpeed = normalSpeed;
                }
            }
            
            if (_physicsCheckLayer.canSmellPlayer && _canFollow)
            {
                //Debug.Log("我发现了玩家");
                currentSpeed = followSpeed;
                if ((attackTrans.transform.position.x - transform.position.x) > 0)
                {
                    // 在右边
                    faceDir = new Vector3(-1, 0, 0);
                }
                else
                {
                    faceDir = new Vector3(1, 0, 0);
                }
                transform.localScale = new Vector3(faceDir.x, 1, 1);
                UpdateFollowCd();

            }
        }

        private void FixedUpdate()
        {
            Movement();
        }

        protected virtual void Movement()
        {
            Rb.velocity = new Vector2(currentSpeed * faceDir.x , Rb.velocity.y);
        }

        public void OnTakeDamage(Transform attackTrans)
        {
            // 
            Animator.SetTrigger(Hurt);
        }

        public void OnDeath()
        {
            Animator.SetTrigger(Dead);
            //DestroyMyself();
        }

        public void DestroyMyself()
        {
            Destroy(gameObject);
        }

        private void UpdateFollowCd()
        {
            if (_canFollow)
            {
                _canFollow = false;
                _currentFollowTimeCounter = followTime;
            }
        }
        
        
    }
}