using RogueHelper.Loaders;
using RogueHelper.Loaders.Repositories;
using RogueHelper.Rooms;
using UnityEngine;

namespace RogueHelper.Core
{
    public class LevelSettings : MonoBehaviour
    {
        [SerializeField] private string _bossRoomPrefabsPath;
        [SerializeField] private string _goldRoomPrefabsPath;
        [SerializeField] private string _baseRoomPrefabsPath;
        [SerializeField] private string _starRoomPrefabsPath;

        [SerializeField] private int _minAmountOfRooms;
        [SerializeField] private int _maxAmountOfRooms;

        private void Awake()
        {
            IResourceLoader resourceLoader = new ResourceLoader();
            IRepository<GameObject> roomsRepository = new RoomsRepository();
            LevelGeneration levelGeneration = new LevelGeneration();

            resourceLoader.LoadResource(_bossRoomPrefabsPath, typeof(BossRoom), roomsRepository);
            resourceLoader.LoadResource(_goldRoomPrefabsPath, typeof(GoldRoom), roomsRepository);
            resourceLoader.LoadResource(_baseRoomPrefabsPath, typeof(Room), roomsRepository);
            resourceLoader.LoadResource(_starRoomPrefabsPath, typeof(StartRoom), roomsRepository);

            levelGeneration.Construct(roomsRepository, _minAmountOfRooms, _maxAmountOfRooms);
        }
    }
}
