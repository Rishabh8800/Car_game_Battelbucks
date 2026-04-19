using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.isGameOver)
        {
            // Particle
            if (hitParticle != null)
            {
                Instantiate(hitParticle, transform.position, Quaternion.identity);
            }

            // Trigger Game Over
            FindObjectOfType<GameManager>().GameOver();

            // Optional: disable obstacle
            gameObject.SetActive(false);
        }
    }
}