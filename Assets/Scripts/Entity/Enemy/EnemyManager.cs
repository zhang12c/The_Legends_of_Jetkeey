using System.Collections.Generic;
using UnityEngine;
namespace Entity.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private Dictionary<string, GameObject> _enemyData = new Dictionary<string,GameObject>();
        public void AddEnemy(string key, GameObject self)
        {
            if (!_enemyData.ContainsKey(key))
            {
                _enemyData.Add(key,self);
            }
            else
            {
                _enemyData[key] = self;
            }
        }

        public GameObject GetEnemy(string key)
        {
            return _enemyData[key] != null ? _enemyData[key] : null;
        }

        public bool RemoveEnemy(string key)
        {
            if (_enemyData.ContainsKey(key))
            {
                _enemyData.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetEnemyCount()
        {
            return _enemyData.Count;
        }
    }
}