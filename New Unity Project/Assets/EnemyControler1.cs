using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler1 : MonoBehaviour
{

    public bool estoyenSuelo;

    enum tipoComportamiento { pasivo, persecución, ataque}

    tipoComportamiento comportamiento = tipoComportamiento.pasivo;

    float entradaZonaActiva = 10f;
    float salidaZonaActiva = 15f;
    float distanciaAtaque = 4.1f;
    CircleCollider2D attackCollider;
    Vector2 mov;

    float distanciadelJugador;
    public Transform jugador;
    Animator anim;

    private Rigidbody2D rb2d;
    private Animator animet;
    float limiteCaminataIzq;
    float limiteCaminataDer;

    public float velCaminata = 4f;
    int direccion = 1;

    private float espera = 4f;
    private float actualespera;

    public int maxhp = 1000;
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        rb2d = GetComponent <Rigidbody2D>();
        anim = GetComponent<Animator>();
        limiteCaminataDer = transform.position.x + GetComponent<CircleCollider2D>().radius;
        limiteCaminataIzq = transform.position.x - GetComponent<CircleCollider2D>().radius;

        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }
     void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Muerte") && stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
        actualespera -= Time.deltaTime;
        anim.SetFloat("Velocidad", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("EstoyEnElSuelo", estoyenSuelo);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.88f;
        if (estoyenSuelo)
        {
            rb2d.velocity = fixedVelocity;
        }
        distanciadelJugador = Mathf.Abs(jugador.position.x - transform.position.x);
     switch (comportamiento)
        {
            case tipoComportamiento.pasivo:
       

        rb2d.velocity = new Vector2(velCaminata * direccion, rb2d.velocity.y);
                if (transform.position.x < limiteCaminataIzq) direccion = 1;
                if (transform.position.x > limiteCaminataDer) direccion = -1;

                transform.localScale = new Vector3(1 * direccion, 1, 1);
                //entrar en zona de persecución
                if (distanciadelJugador < entradaZonaActiva) comportamiento = tipoComportamiento.persecución;
                break;
            case tipoComportamiento.persecución:
                
                if (jugador.position.x > transform.position.x) direccion = 1;
                if (jugador.position.x < transform.position.x) direccion = -1;
                AnimatorStateInfo stateinf = anim.GetCurrentAnimatorStateInfo(0);
                bool atacanding = stateinf.IsName("Ataque");
                if (!atacanding)
                {
                    attackCollider.enabled = false;
                    velCaminata = 4f;
                    rb2d.velocity = new Vector2(velCaminata * direccion, rb2d.velocity.y);
                    transform.localScale = new Vector3(1 * direccion, 1, 1);
                }
                if (atacanding) velCaminata = 0;
                // volver a la zona pasiva
                if (distanciadelJugador > salidaZonaActiva) comportamiento = tipoComportamiento.persecución;
                //entre en la zona de ataque
                if (distanciadelJugador < distanciaAtaque) comportamiento = tipoComportamiento.ataque;
                break;
            case tipoComportamiento.ataque:
                
                AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                bool atacando = stateInfo.IsName("Ataque");

                if (!atacando)
                {
                    attackCollider.enabled = false;
                    transform.localScale = new Vector3(1 * direccion, 1, 1);
                }
                if (!atacando && actualespera < 0)
                {
                    atacando = true;
                    anim.SetTrigger("atacar");
                    actualespera = espera;
                    
                }
                if (atacando)
                {
                    float playbackTime = stateInfo.normalizedTime;
                    if (playbackTime > 0.562 && playbackTime < 0.80) attackCollider.enabled = true;
                    
                    else attackCollider.enabled = false;
                }
                if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);

                if (jugador.position.x > transform.position.x) direccion = 1;
                if (jugador.position.x < transform.position.x) direccion = -1;

                //entre en la zona de persecucion
                if (distanciadelJugador > distanciaAtaque) {
                    comportamiento = tipoComportamiento.persecución;
                    anim.ResetTrigger("atacar");

                }
                break;
        }

        

    }
    void OnCollisionStay2D(Collision2D colision)
    {
        if (colision.gameObject.tag == "Suelo")
        {
            estoyenSuelo = true;
        }
        if (colision.gameObject.tag == "Plataforma")
        {
            transform.parent = colision.transform;
            estoyenSuelo = true;
        }
    }
    void OnCollisionExit2D(Collision2D colision1)
    {
        if (colision1.gameObject.tag == "Suelo")
        {
            estoyenSuelo = false;
        }
        if (colision1.gameObject.tag == "Plataforma")
        {
            transform.parent = null;
            estoyenSuelo = false;
        }
    }
   // public void Atacado()
  //  {
   //     AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
    //    bool atacando = stateInfo.IsName("Ataque1");
    //    if (--hp <= 0)
    //    {
    //        anim.Play("Muerte");
    //    }
    //    else if (atacando == false)
    //    {
    //        actualespera = espera - 3;
    //        anim.Play("recibeDaño");
    //    }
    // }
    public void TakeDamage(int damage)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool atacando = stateInfo.IsName("Ataque");
        hp -= damage;
        if(hp <= 0 )
        {
            anim.Play("Muerte");
        }
        else if (atacando == false)
        {
            actualespera = espera - 4;
            anim.Play("recibeDaño");
        }
    }
    private void OnGUI()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(
            new Rect(
                pos.x,
                Screen.height - pos.y - 150,
                80,
                48
            ), hp + "/" + maxhp
            );
    }
}
