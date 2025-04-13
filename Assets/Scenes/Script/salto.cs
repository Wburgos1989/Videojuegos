using UnityEngine;

public class salto : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 5;

    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool recibiendoDanio;
    private bool atacando;
    public bool muerto;
    private Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto) {
            if (!atacando)
            {
                Movimiento();

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
                enSuelo = hit.collider != null;

                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDanio)
                {
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) && !atacando && enSuelo)
            {
                Atacando();
            }

            animator.SetBool("ensuelo", enSuelo);
            animator.SetBool("recibeDanio", recibiendoDanio);
            animator.SetBool("atacando", atacando);
            
        }
    }

    public void Movimiento()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        animator.SetFloat("movement", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        Vector3 posicion = transform.position;

        if (!recibiendoDanio)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
    }

    public void recibeDanio(Vector2 direccion, int cantDanio)
    {   
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            vida -= cantDanio;
            if (vida <= 0)
            {
                muerto = true;
                animator.SetBool("muerto", muerto);
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
            
        }
        
    }

    void desactivaDanio()
    {
        recibiendoDanio = false;
        Vector2 nuevaPosicion = rb.position + Vector2.zero;
        rb.MovePosition(nuevaPosicion);
    }

    public void Atacando()
    {
        atacando = true;
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }



}
