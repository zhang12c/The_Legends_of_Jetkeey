using UnityEngine;
namespace Entity.Player
{
    public class Attack : MonoBehaviour
    {
        public int damage;
        public float attackRange;
        public float attackRate;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer != gameObject.layer)
            {
                other.GetComponent<Character>()?.TakeDamage(this);
            }
        }
    }
}