using UnityEngine;

namespace Main
{
    public class Spike : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player") == false)
                return;

            if (other.collider.TryGetComponent(out Player.Player player))
            {
                player.Die();
            }
        }
    }
}
