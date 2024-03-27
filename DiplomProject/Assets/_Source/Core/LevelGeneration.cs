using RogueHelper.Loaders;
using RogueHelper.Rooms;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RogueHelper.Core
{
    public class LevelGeneration
    {
        private const int STARTED_ROOM_INDEX = 55;

        private int _minAmountOfRooms;
        private int _maxAmountOfRooms;
        private int _currentAmountOfRooms;

        private float _prefabsHeight;
        private float _prefabsWidth;

        private bool _isEditor;

        private List<GameObject> _bossRoomPrefabs;
        private List<GameObject> _goldRoomPrefabs;
        private List<GameObject> _baseRoomPrefabs;
        private List<GameObject> _startRoomPrefabs;
        private List<GameObject> _itemPrefabs;

        private GameObject _levelGameObject;

        private List<Room> _roomsObjects = new();
        private List<int> _endRooms = new();
        private Queue<int> _roomQueue = new();
        private int[] _level = new int[100];

        public void Construct(IRepository<GameObject> gameobjectsRepository, int minAmount, int maxAmount)
        {
            _bossRoomPrefabs = gameobjectsRepository.GetItem<GameObject>(typeof(BossRoom));
            _goldRoomPrefabs = gameobjectsRepository.GetItem<GameObject>(typeof(GoldRoom));
            _baseRoomPrefabs = gameobjectsRepository.GetItem<GameObject>(typeof(Room));
            _startRoomPrefabs = gameobjectsRepository.GetItem<GameObject>(typeof(StartRoom));
            _itemPrefabs = gameobjectsRepository.GetItem<GameObject>(typeof(Item));

            _prefabsWidth = _baseRoomPrefabs[Random.Range(0, _baseRoomPrefabs.Count)].transform.localScale.x + 1f;
            _prefabsHeight = _baseRoomPrefabs[Random.Range(0, _baseRoomPrefabs.Count)].transform.localScale.y + 1f;

            _minAmountOfRooms = minAmount;
            _maxAmountOfRooms = maxAmount;

            _levelGameObject = new GameObject("Level");

            StartGeneration();
        }

        private void StartGeneration()
        {
            _currentAmountOfRooms = 0;
            _level = new int[100];
            _endRooms = new List<int>();
            _roomQueue = new Queue<int>();
            _roomsObjects = new List<Room>();

            CreateRoom(STARTED_ROOM_INDEX, _startRoomPrefabs);
            Generate();
        }

        private bool Visit(int roomIndex)
        {
            if (_level[roomIndex] == 1)
                return false;

            if (NeighbourCount(roomIndex) > 1)
                return false;

            if (_currentAmountOfRooms >= _maxAmountOfRooms)
                return false;

            System.Random rnd = new System.Random();
            if (rnd.NextDouble() < 0.5 && roomIndex != STARTED_ROOM_INDEX)
                return false;

            CreateRoom(roomIndex, _baseRoomPrefabs);
            return true;
        }

        private int NeighbourCount(int roomIndex) =>
            _level[roomIndex + 1] + _level[roomIndex - 1] + _level[roomIndex + 10] + _level[roomIndex - 10];

        private int RandomRoom()
        {
            int index = Random.Range(0, _endRooms.Count);
            int x = _endRooms[index];

            for (int i = 0; i < _roomsObjects.Count; i++)
            {
                if(_roomsObjects[i].Index == _endRooms[index])
                {
                    if (_isEditor)
                        GameObject.DestroyImmediate(_roomsObjects[i].gameObject);
                    else
                        GameObject.Destroy(_roomsObjects[i].gameObject);
                }
            }

            _endRooms.RemoveAt(index);
            return x;
        }

        private void CreateRoom(int roomIndex, List<GameObject> prefabs)
        {
            _roomQueue.Enqueue(roomIndex);
            _level[roomIndex] = 1;
            _currentAmountOfRooms++;

            float x = (roomIndex % 10 - _level.Length / 20) * _prefabsWidth;
            float y = (roomIndex / 10 - _level.Length / 20) * _prefabsHeight;

            GameObject currentRoom = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count)],
                new Vector2(x, y), Quaternion.identity, _levelGameObject.transform);
            if(currentRoom.TryGetComponent(out Room room))
                room.Construct(roomIndex, this);
            if (currentRoom.TryGetComponent(out GoldRoom goldRoom))
                goldRoom.Construct(_itemPrefabs);
            _roomsObjects.Add(room);
        }

        private void CheckRoomNeighbour(int roomIndex, out int[] doors)
        {
            doors = new int[4];
            if (_level[roomIndex - 1] > 0)
                doors[0] = 1;
            if (_level[roomIndex + 1] > 0)
                doors[1] = 1;
            if (_level[roomIndex + 10] > 0)
                doors[2] = 1;
            if (_level[roomIndex - 10] > 0)
                doors[3] = 1;
        }

        public void Generate()
        {
            while(_roomQueue.Count > 0)
            {
                int i = _roomQueue.Dequeue();
                int x = i % 10;

                bool created = false;

                if (x > 1)
                    created = Visit(i - 1);
                if (x < 9)
                    created = Visit(i + 1);
                if (i > 20)
                    created = Visit(i - 10);
                if (i < 70)
                    created = Visit(i + 10);

                if (!created)
                {
                    if (NeighbourCount(i) <= 1
                        && i != 55
                        && i != 56
                        && i != 54
                        && i != 65
                        && i != 45)
                        _endRooms.Add(i);
                }
            }

            if (_minAmountOfRooms > _currentAmountOfRooms)
            {
                for (int i = 0; i < _roomsObjects.Count; i++)
                {
                    if (_isEditor)
                        GameObject.DestroyImmediate(_roomsObjects[i].gameObject);
                    else
                        GameObject.Destroy(_roomsObjects[i].gameObject);
                }
                StartGeneration();
                return;
            }

            int bossRoom = RandomRoom();
            bool isBadSpawn = true;
            while (isBadSpawn)
            {
                if (bossRoom == 44
                   || bossRoom == 45
                   || bossRoom == 46
                   || bossRoom == 54
                   || bossRoom == 56
                   || bossRoom == 64
                   || bossRoom == 65
                   || bossRoom == 66)
                {
                    bossRoom = RandomRoom();
                }
                else
                    isBadSpawn = false;
            }
            CreateRoom(bossRoom, _bossRoomPrefabs);

            int goldenRoom = RandomRoom();
            CreateRoom(goldenRoom, _goldRoomPrefabs);

            foreach (Room item in _roomsObjects)
            {
                if (item == null)
                    continue;
                CheckRoomNeighbour(item.Index, out int[] doors);
                item.SetDoors(doors);
            }
        }

        public void DestroyRooms()
        {
            for (int i = 0; i < _roomsObjects.Count; i++)
            {
                if(_roomsObjects[i] == null)
                {
                    _roomsObjects.RemoveAt(i);
                    continue;
                }
                if (_isEditor)
                    GameObject.DestroyImmediate(_roomsObjects[i].gameObject);
                else
                    GameObject.Destroy(_roomsObjects[i].gameObject);
            }

            if (_isEditor)
                GameObject.DestroyImmediate(_levelGameObject);
            else
                GameObject.Destroy(_levelGameObject);
        }

        public void RemoveRoom(Room room)
        {
            _currentAmountOfRooms--;
            _roomsObjects.Remove(room);
        }

        public void GenerateFromEditor(bool isEditor)
        {
            _isEditor = isEditor;
        }
    }
}
