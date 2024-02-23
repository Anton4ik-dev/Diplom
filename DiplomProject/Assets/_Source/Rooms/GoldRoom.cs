using RogueHelper.Loaders;
using System.Collections.Generic;
using UnityEngine;

namespace RogueHelper.Rooms
{
    public class GoldRoom : Room
    {
        [SerializeField] private List<Transform> _placesForSpawnItem;

        private List<GameObject> _items;

        public void Construct(List<GameObject> items)
        {
            _items = items;
            SpawnItems();
        }

        private void SpawnItems()
        {
            for (int i = 0; i < _placesForSpawnItem.Count; i++)
            {
                Instantiate(_items[Random.Range(0, _items.Count)], _placesForSpawnItem[i]);
            }
        }
    }
}