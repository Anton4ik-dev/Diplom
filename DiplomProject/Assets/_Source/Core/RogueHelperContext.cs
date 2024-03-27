using RogueHelper.Characters.ICharacterBase;
using RogueHelper.Loaders;
using RogueHelper.Loaders.Repositories;
using RogueHelper.Rooms;
using RogueHelper.Services;
using UnityEngine;

namespace RogueHelper.Core
{
    public class RogueHelperContext : MonoBehaviour
    {
        [SerializeField] private FolderReference _bossRoomPrefabsPath;
        [SerializeField] private FolderReference _goldRoomPrefabsPath;
        [SerializeField] private FolderReference _baseRoomPrefabsPath;
        [SerializeField] private FolderReference _startRoomPrefabsPath;
        [SerializeField] private FolderReference _itemsPrefabsPath;
        [SerializeField] private GameObject _chosenCharacter;

        [Range(10, 31)]
        [SerializeField] private int _minAmountOfRooms = 10;
        [Range(11, 32)]
        [SerializeField] private int _maxAmountOfRooms = 32;

        private LevelGeneration _levelGeneration;

        private void Awake()
        {
            SpawnCharacter();
            GenerateLevel(false);
        }

        private void SpawnCharacter()
        {
            GameObject characterBody = Instantiate(_chosenCharacter, Vector3.zero, Quaternion.identity);
            for (int i = 0; i < characterBody.transform.childCount; i++)
            {
                if (characterBody.transform.GetChild(i).TryGetComponent(out ICharacter character))
                {
                    character.Initialize(GetComponent<IInputListener>());
                }
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

            resourceLoader.LoadResource(_bossRoomPrefabsPath.Path, typeof(BossRoom), gameObjectRepository);
            resourceLoader.LoadResource(_goldRoomPrefabsPath.Path, typeof(GoldRoom), gameObjectRepository);
            resourceLoader.LoadResource(_baseRoomPrefabsPath.Path, typeof(Room), gameObjectRepository);
            resourceLoader.LoadResource(_startRoomPrefabsPath.Path, typeof(StartRoom), gameObjectRepository);
            resourceLoader.LoadResource(_itemsPrefabsPath.Path, typeof(Item), gameObjectRepository);

            _levelGeneration.GenerateFromEditor(isEditor);
            _levelGeneration.Construct(gameObjectRepository, _minAmountOfRooms, _maxAmountOfRooms);
        }
    }
}
