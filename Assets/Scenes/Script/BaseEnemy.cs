using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float leftLimit = -5.0f;
    public float rightLimit = 5.0f;
    public float fuerzaRebote = 10f;
    public int vida = 3;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool enMovimiento = false;
    private bool recibiendoDanio;
    private bool playerVivo;
    private bool muerto;
    public Animator animator;

    void Start()
    {
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(playerVivo && !muerto)
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Obtener la direcci�n de movimiento
        float moveDirection = movingRight ? 1 : -1;

        // Calcular la nueva posici�n
        Vector2 newPosition = rb.position + new Vector2(moveDirection * speed * Time.fixedDeltaTime, 0);

        // Mover el enemigo
        if (!recibiendoDanio)
        rb.MovePosition(newPosition);

        enMovimiento = true;

        animator.SetBool("EnMovimiento", enMovimiento);

        // Cambiar direcci�n si alcanza los l�mites
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

            salto playerScript = collision.gameObject.GetComponent<salto>();

            playerScript.recibeDanio(direccionDanio, 1);

            playerVivo = !playerScript.muerto;

            if (!playerVivo)
            {
                enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ataque"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);

            recibeDanio(direccionDanio, 1);

        }
    }

    public void recibeDanio(Vector2 direccion, int cantDanio)
    {
            if (!recibiendoDanio)
            {
                vida -= cantDanio;
                recibiendoDanio = true;
                if (vida <= 0)
                {
                    muerto = true;
                    enMovimiento = false;
                    animator.SetBool("muerto", muerto);
                }
                else
                {
                    Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                    rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                    StartCoroutine(DesactivaDanio());
                }

            }

    }

    IEnumerator DesactivaDanio()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
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
