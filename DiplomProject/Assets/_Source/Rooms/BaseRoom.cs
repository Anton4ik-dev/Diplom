using RogueHelper.Enemies.IEnemyBase;
using RogueHelper.Services;
using System.Collections.Generic;
using UnityEngine;

namespace RogueHelper.Rooms
{
    public class BaseRoom : Room
    {
        [SerializeField] private LayerMask _characterLayer;
        [SerializeField] private Transform _enemiesParent;

        private bool _isFirstTime = true;
        private List<IEnemy> _enemies = new List<IEnemy>();

        private void Start()
        {
            for (int i = 0; i < _enemiesParent.childCount; i++)
            {
                if(_enemiesParent.GetChild(i).TryGetComponent(out IEnemy enemy))
                {
                    _enemies.Add(enemy);
                }
            }
            enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerService.CheckLayersEquality(collision.gameObject.layer, _characterLayer))
            {
                if (_isFirstTime)
                {
                    _isFirstTime = false;
                    SetDoors(_doorsToActivate, false);
                    enabled = true;
                }
            }
        }

        private void Update()
        {
            bool isEnd = true;

            for (int i = 0; i < _enemies.Count; i++)
            {
                if(!_enemies[i].IsDead)
                {
                    isEnd = false;
                    break;
                }
            }

            if(isEnd)
            {
                SetDoors(_doorsToActivate);
                enabled = false;
            }
        }
    }
}