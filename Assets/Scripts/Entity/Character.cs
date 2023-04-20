using System;
using Entity.Player;
using UnityEngine;
using UnityEngine.Events;
namespace Entity
{
    public class Character : MonoBehaviour
    {

        [Header("基本属性")]
        public float maxHealth;
        public float currentHealth;

        public UnityEvent<Transform> OnTakeDamage;
        public UnityEvent OnDie;

        [Header("受伤无敌")]
        // 被打击的时候有一小段无敌时间
        private bool _canBeAttack;
        public float invulnerableDuration;
        private float _invulnerableCounter;

        private void Start()
        {
            currentHealth = maxHealth;
        }
        private void Update()
        {
            if (!_canBeAttack)
            {
                _invulnerableCounter -= Time.deltaTime;
                if (_invulnerableCounter <= 0)
                {
                    _canBeAttack = true;
                }
            }
        }
        
        /// <summary>
        /// 扣血伤害
        /// </summary>
        /// <param name="subHp"></param>
        public void TakeDamage(Attack attacker)
        {
            if (!_canBeAttack)
            {
                Debug.Log("无敌保护中，无法被攻击！");
                return;
            }
            // 这里是扣血
            if (currentHealth - attacker.damage > 0)
            {
                currentHealth -= attacker.damage;
                Debug.Log("执行受伤-" + attacker.damage);
                //执行受伤
                OnTakeDamage?.Invoke(attacker.transform.parent);
                TriggerInvulnerable();
            }
            else
            {
                currentHealth = 0;
                //触发死亡
                OnDie?.Invoke();
            }
            
        }

        /// <summary>
        /// 触发受伤无敌
        /// </summary>
        private void TriggerInvulnerable()
        {
            if (_canBeAttack)
            {
                _canBeAttack = false;
                _invulnerableCounter = invulnerableDuration;
            }
        }
        
    }
}