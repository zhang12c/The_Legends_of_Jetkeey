using System;
using UnityEngine;
using UnityEngine.Serialization;
namespace UnitTool
{
    public class PhysicsCheckLayer : MonoBehaviour
    {
        [Header("检测地面的")]
        public bool isGround;
        public Transform groundCheck;
        public float groundCheckRadius;
        public LayerMask whatIsGround;
        [Header("检测玩家")]
        public bool canSmellPlayer;
        public Transform warningTrs;
        public float warningCircleRadius;
        public LayerMask whatIsPlayer;
        
        private void FixedUpdate()
        {
            CheckSurroundings();
        }
        private void CheckSurroundings()
        {
            isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            
            canSmellPlayer = Physics2D.OverlapCircle(warningTrs.position, warningCircleRadius, whatIsPlayer);
        }
    }
}