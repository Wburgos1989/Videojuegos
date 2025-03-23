using UnityEngine;

public class gemas : MonoBehaviour
{
    [SerializeField] private Puntaje scoreManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(gameObject.tag); 
            }

            Destroy(gameObject);
        }
    }
}
