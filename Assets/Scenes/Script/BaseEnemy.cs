using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float leftLimit = -5.0f;
    public float rightLimit = 5.0f;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool enMovimiento = false;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Obtener la dirección de movimiento
        float moveDirection = movingRight ? 1 : -1;

        // Calcular la nueva posición
        Vector2 newPosition = rb.position + new Vector2(moveDirection * speed * Time.fixedDeltaTime, 0);

        // Mover el enemigo
        rb.MovePosition(newPosition);

        enMovimiento = true;

        animator.SetBool("EnMovimiento", enMovimiento);

        // Cambiar dirección si alcanza los límites
        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x <= leftLimit)
        {
            movingRight = true;
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);

            collision.gameObject.GetComponent<salto>().recibeDanio(direccionDanio, 1);
        }
    }

    void Flip()
    {
        // Invertir la escala en X para voltear el sprite
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftLimit, transform.position.y, 0), new Vector3(rightLimit, transform.position.y, 0));
    }
}
