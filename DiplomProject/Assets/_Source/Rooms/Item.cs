using UnityEngine;

namespace RogueHelper.Rooms
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private LayerMask _characterLayer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerService.CheckLayersEquality(collision.gameObject.layer, _characterLayer))
            {
                DoEffect();
                gameObject.SetActive(false);
            }
        }

        private void DoEffect()
        {
            // do your item effect
        }
    }
}