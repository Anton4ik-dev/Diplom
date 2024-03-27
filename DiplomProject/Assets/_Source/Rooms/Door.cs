using RogueHelper.Services;
using UnityEngine;

namespace RogueHelper.Rooms
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private DoorDirection _direction;
        [SerializeField] private LayerMask _characterLayer;

        private const float DISTANCE = 4f;
        private const float DISTANCE_FOR_CAMERA_X = 17.76396f;
        private const float DISTANCE_FOR_CAMERA_Y = 10f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerService.CheckLayersEquality(collision.gameObject.layer, _characterLayer))
            {
                float characterX = transform.position.x;
                float characterY = transform.position.y;
                float cameraX = Camera.main.transform.position.x;
                float cameraY = Camera.main.transform.position.y;

                if (_direction == DoorDirection.UP)
                {
                    characterY += DISTANCE;
                    cameraY += DISTANCE_FOR_CAMERA_Y;
                }
                else if (_direction == DoorDirection.RIGHT)
                {
                    characterX += DISTANCE;
                    cameraX += DISTANCE_FOR_CAMERA_X;
                }
                else if (_direction == DoorDirection.DOWN)
                {
                    characterY -= DISTANCE;
                    cameraY -= DISTANCE_FOR_CAMERA_Y;
                }
                else if (_direction == DoorDirection.LEFT)
                {
                    characterX -= DISTANCE;
                    cameraX -= DISTANCE_FOR_CAMERA_X;
                }

                collision.gameObject.transform.position = new Vector2(characterX, characterY);
                Camera.main.transform.position = new Vector3(cameraX, cameraY, -10);
            }
        }
    }
}