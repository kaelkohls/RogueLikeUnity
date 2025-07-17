using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMIGO : MonoBehaviour
{
    // Start is called before the first frame update
    public bool estoyenSuelo;

    public enum tipoComportamiento { pasivo, persecuci�n, ataque }

    public tipoComportamiento comportamiento = tipoComportamiento.pasivo;

   public float entradaZonaActiva = 10f;
    public float salidaZonaActiva = 15f;
    public float distanciaAtaque;

    public Vector2 mov;

    public float distanciadelJugador;
    public Transform jugador;
    public Animator anim;

    public Rigidbody2D rb2d;
    public Animator animet;
    public float limiteCaminataIzq;
    public float limiteCaminataDer;

    public float velCaminata = 4f;
    public int direccion =1;

    public float espera;
    public float actualespera;

    public int maxhp;
    public int hp;

    public virtual void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
       hp = maxhp;
       rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        limiteCaminataDer = transform.position.x + GetComponent<CircleCollider2D>().radius;
        limiteCaminataIzq = transform.position.x - GetComponent<CircleCollider2D>().radius;

    }

    // Update is called once per frame
    public virtual void Update()
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
                //entrar en zona de persecuci�n
                if (distanciadelJugador < entradaZonaActiva) comportamiento = tipoComportamiento.persecuci�n;
                break;
            case tipoComportamiento.persecuci�n:
                if (jugador.position.x > transform.position.x) direccion = 1;
                if (jugador.position.x < transform.position.x) direccion = -1;
                AnimatorStateInfo stateinf = anim.GetCurrentAnimatorStateInfo(0);
                bool atacanding = stateinf.IsName("Ataque");
                if (!atacanding)
                {
                   // attackCollider.enabled = false;
                    velCaminata = 4f;
                    rb2d.velocity = new Vector2(velCaminata * direccion, rb2d.velocity.y);
                    transform.localScale = new Vector3(1 * direccion, 1, 1);
                }
                if (atacanding) velCaminata = 0;
                // volver a la zona pasiva
                if (distanciadelJugador > salidaZonaActiva) comportamiento = tipoComportamiento.persecuci�n;
                //entre en la zona de ataque
                if (distanciadelJugador < distanciaAtaque) comportamiento = tipoComportamiento.ataque;
                break;
            case tipoComportamiento.ataque:

                AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                bool atacando = stateInfo.IsName("Ataque");
                if (!atacando)
                {
                   // attackCollider.enabled = false;
                    transform.localScale = new Vector3(1 * direccion, 1, 1);
                }
                if (!atacando && actualespera < 0)
                {
                    atacando = true;
                   // Invoke("LanzarFuxego", 0.1f);
                    anim.SetTrigger("atacar");
                    actualespera = espera;
                }
                if (atacando)
                {
                   // float playbackTime = stateInfo.normalizedTime;
                 //   if (playbackTime > 0 && playbackTime < 0.80) attackCollider.enabled = true;
                   // else attackCollider.enabled = false;
                }
                //if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);

                if (jugador.position.x > transform.position.x) direccion = 1;
                if (jugador.position.x < transform.position.x) direccion = -1;

                //entre en la zona de persecucion
                if (distanciadelJugador > distanciaAtaque)
                {
                    comportamiento = tipoComportamiento.persecuci�n;
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
    public void TakeDamage(int damage)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool atacando = stateInfo.IsName("Ataque");
        hp -= damage;
        if (hp <= 0)
        {
            anim.Play("Muerte");
        }
        else if (atacando == false)
        {
            actualespera = espera - 3;
            anim.Play("recibeDa�o");
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
