using UnitTool;
using Unity.VisualScripting;
using UnityEngine;
namespace Entity.Enemy
{
    public class Pig : BaseEnemy
    {
        private static readonly int Walk = Animator.StringToHash("walk");
        private static readonly int Run = Animator.StringToHash("run");
        
        protected override void Movement()
        {
            base.Movement();
            Animator.SetBool(Walk,true);
            //Animator.SetBool(Run,true);
        }
        
        
    }
}