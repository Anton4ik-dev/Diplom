using RogueHelper.Characters.ICharacterBase;
using RogueHelper.Loaders;
using RogueHelper.Loaders.Repositories;
using RogueHelper.Rooms;
using UnityEngine;

namespace RogueHelper.Core
{
    public class RogueHelperContext : MonoBehaviour
    {
        [SerializeField] private string _bossRoomPrefabsPath = "TestLevelBossRooms";
        [SerializeField] private string _goldRoomPrefabsPath = "TestLevelGoldRooms";
        [SerializeField] private string _baseRoomPrefabsPath = "TestLevelBaseRooms";
        [SerializeField] private string _startRoomPrefabsPath = "TestLevelStartRooms";
        [SerializeField] private string _itemsPrefabsPath = "TestLevelItems";
        [SerializeField] private GameObject _chosenCharacter;

        [Range(10, 100)]
        [SerializeField] private int _minAmountOfRooms = 10;
        [Range(10, 100)]
        [SerializeField] private int _maxAmountOfRooms = 15;

        private LevelGeneration _levelGeneration;

        private void Awake()
        {
            SpawnCharacter();
            GenerateLevel(false);
        }

        private void SpawnCharacter()
        {
            GameObject characterBody = Instantiate(_chosenCharacter, Vector3.zero, Quaternion.identity);
            if(characterBody.TryGetComponent(out ICharacter character))
            {
                character.Initialize(GetComponent<IInputListener>());
            }
        }

        public void GenerateLevel(bool isEditor)
        {
            if (isEditor)
                DestroyImmediate(GameObject.Find("Level"));
            else
                Destroy(GameObject.Find("Level"));
            IResourceLoader resourceLoader = new ResourceLoader();
            IRepository<GameObject> gameObjectRepository = new RoomsRepository();
            if (_levelGeneration is null)
                _levelGeneration = new LevelGeneration();
            else
                _levelGeneration.DestroyRooms();

            resourceLoader.LoadResource(_bossRoomPrefabsPath, typeof(BossRoom), gameObjectRepository);
            resourceLoader.LoadResource(_goldRoomPrefabsPath, typeof(GoldRoom), gameObjectRepository);
            resourceLoader.LoadResource(_baseRoomPrefabsPath, typeof(Room), gameObjectRepository);
            resourceLoader.LoadResource(_startRoomPrefabsPath, typeof(StartRoom), gameObjectRepository);
            resourceLoader.LoadResource(_itemsPrefabsPath, typeof(Item), gameObjectRepository);

            _levelGeneration.GenerateFromEditor(isEditor);
            _levelGeneration.Construct(gameObjectRepository, _minAmountOfRooms, _maxAmountOfRooms);
        }
    }
}
