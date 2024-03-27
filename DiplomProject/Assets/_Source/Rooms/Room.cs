using RogueHelper.Core;
using System.Collections.Generic;
using UnityEngine;

namespace RogueHelper.Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _doors;

        public int Index { get; private set; }

        private LevelGeneration _levelGeneration;
        protected int[] _doorsToActivate;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BossRoom bossRoom)
                || collision.gameObject.TryGetComponent(out GoldRoom goldRoom))
            {
                if(!gameObject.TryGetComponent(out BaseRoom baseRoom))
                {
                    Destroy(gameObject);
                    _levelGeneration.RemoveRoom(this);
                }
            }
        }

        public void Construct(int index, LevelGeneration levelGeneration)
        {
            Index = index;
            _levelGeneration = levelGeneration;
        }

        public void SetDoors(int[] doors, bool isActivate = true)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i] == 1)
                {
                    _doors[i].SetActive(isActivate);
                }
            }
            _doorsToActivate = doors;
        }
    }
}