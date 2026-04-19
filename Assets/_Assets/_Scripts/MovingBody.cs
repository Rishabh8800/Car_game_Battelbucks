using UnityEngine;

public class MovingBody : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
    
    private void Update()
    {
        if (GameManager.isGameOver) return;
        
        transform.position -= Vector3.forward * speed * Time.deltaTime;
    }
}
