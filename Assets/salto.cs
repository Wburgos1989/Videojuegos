using UnityEngine;

public class salto : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fuerza; // Fuerza de salto
    public float velocidad; // Velocidad de movimiento horizontal

    private float movimiento;

    // Update is called once per frame
    void Update()
    {
        // Movimiento horizontal
        movimiento = Input.GetAxis("Horizontal"); // Obtiene la entrada horizontal (teclas de flecha o A/D)

        // Aplica el movimiento horizontal
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
        }
    }
}
