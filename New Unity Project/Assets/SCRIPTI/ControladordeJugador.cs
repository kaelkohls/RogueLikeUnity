using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladordeJugador : MonoBehaviour
{
    public bool estela2 = false;
    private float espera2 = 0.5f;
    private float actualespera2;

    public TrailRenderer trailR;
    public EdgeCollider2D edgeC;

    public int ultimaMaxHP;
    public int ultimaHP;
    public int proximaMaxHP;
    public int proximaHP;


    public string animacion;
    public string animacion2;
    public string animacion3;
    public string animacion4;

    public float tiempoIniciale2;
    public float tiempoFinale2;

    public float tiempoIniciale3;
    public float tiempoFinale3;

    public float tiempoIniciale4;
    public float tiempoFinale4;
    //public Transform attackPoint;
    //public float attackRange = 0.5f;
    //public LayerMask enemyLayers;
    //public LayerMask enemyLayers2;
    // public int attackDamage = 40;
    //----------------------------------------------------------------------------------

    public bool fireball = false;
    public GameObject proyectil;
    
    public float tiempoIniciale;
    public float tiempoFinale;
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool estoyenSuelo;
    public float fuerzadeSalto = 6.5f;
    CircleCollider2D attackCollider;
    Vector2 mov;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool salto;

    private float espera = 0.8f;
    private float actualespera;

    public int maxhp = 250;
    public int hp;
    public UnityEngine.UI.Image barraRoja;

    float momentoAtaqueRecibido = 0;
    float tiempoGracia = 2f;

    IEnumerator dashCoroutine;
    bool isDashing;
    bool canDash = true;
    public float direction;

    public GameObject pocion;

    public GameObject colliderGO;

    // Start is called before the first frame update
    void Awake()
    { 
        trailR = this.GetComponent<TrailRenderer>();
       // GameObject colliderGO = new GameObject("TrailCollider", typeof(EdgeCollider2D));
        edgeC = colliderGO.GetComponent<EdgeCollider2D>();
        edgeC.isTrigger = true;
    }
    void Start()
    {
        trailR.enabled = false;
        ultimaMaxHP = 250;
        ultimaHP = 250;
        hp = maxhp;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();

        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * speed * h);
        float limiteSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limiteSpeed, rb2d.velocity.y);

        actualespera2 -= Time.deltaTime;

        SetColliderPointsFromTrail(trailR, edgeC);
       

        pocion = GameObject.Find("PocionVida");
        if (Input.GetKeyDown(KeyCode.X) && canDash == true)
        {
            
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            anim.Play("Dash");
            dashCoroutine = Dash(.1f,1);
            StartCoroutine(dashCoroutine);
           
        }
        actualespera -= Time.deltaTime;
        anim.SetFloat("Velocidad", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("EstoyEnElSuelo", estoyenSuelo);


        if ((Input.GetKeyDown(KeyCode.UpArrow) && estoyenSuelo == true) || (Input.GetKeyDown(KeyCode.W) && estoyenSuelo == true))
        {
            anim.SetBool("estaSaltando", true);
            salto = true;
        }
        // if( ataque <2 ){
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool atacando = stateInfo.IsName("Ataque1");
        if (Input.GetKeyDown(KeyCode.Z) && !atacando && actualespera < 0)
        {
            anim.SetTrigger("estaAtacando");
            actualespera = espera;
            atacando = true;
            if(fireball == true)
            {
                LanzarFuego();
                //Invoke("LanzarFuego", 0.1f);
            }
        }
        if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);
        if (atacando)
        {
            speed = 0f;
            if (!isDashing) { 
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > tiempoIniciale && playbackTime < tiempoFinale) attackCollider.enabled = true;
            else attackCollider.enabled = false;
        }
            else attackCollider.enabled = false;
        }
        if (!atacando)
        {
            attackCollider.enabled = false;
            speed = 25f;
            if (h > 0.1f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (h < -0.1f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        //}
    
        
        float valorDeseado = (float)hp / maxhp;
        barraRoja.fillAmount = Mathf.Lerp(barraRoja.fillAmount, valorDeseado, .1f);

        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.88f;
        if (estoyenSuelo)
        {
            rb2d.velocity = fixedVelocity;
        }
       
            
        
        if (salto)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * fuerzadeSalto, ForceMode2D.Impulse);
            salto = false;
        }
        if (isDashing)
        {
            momentoAtaqueRecibido = Time.time - 2;
            limiteSpeed = 99;
            rb2d.AddForce(new Vector2(direction * 50, 0), ForceMode2D.Impulse);
            if(estela2 == true) { 
                trailR.enabled = true;
                actualespera2 = espera2;
            }

        }
        if (!isDashing)
        {
            limiteSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        }
        if (h != 0)
        {
            direction = h;
        }
        if (actualespera2 < 0)
        {
            trailR.enabled = false;
        }
        //  if (canDash)
        // {
        //     trailR.enabled = false;
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            anim.SetBool("estaSaltando", false);
        }
        if (collision.gameObject.tag == "Plataforma")
        {
            anim.SetBool("estaSaltando", false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (Time.time > momentoAtaqueRecibido + tiempoGracia) {
            momentoAtaqueRecibido = Time.time;
            hp -= damage;
            if (hp <= 0)
        {
            anim.Play("Muerte");
        }
            else
            {
            anim.Play("Dolor");
            }
        }
    }
    IEnumerator Dash(float dashDuration, float dashCooldown){
        isDashing = true;
        canDash = false;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void ActualizarEquipamiento(List<HabilidadElemental> habilidadesEquipadas)
    {
        ultimaMaxHP = maxhp;
        ultimaHP = hp;
        ResetearModificadores();
        foreach (HabilidadElemental habilidad in habilidadesEquipadas)
        {

            //if(habilidad.tipohabilidad < 2)
            // {
            if(habilidad.fireball == true) fireball = habilidad.fireball;
            maxhp += habilidad.salud;
            proximaMaxHP = maxhp;
            if(habilidad.estela == true)estela2 = habilidad.estela;
            //if (habilidadesEquipadas<habilidad> null)
            //{
              //  proximaMaxHP = maxhp;
            //}
            //if (ultimaHP < maxhp) {
            //   hp = ultimaHP;
            // 
            GameManager.instance.jugador.GetComponentInChildren<Ataque>().extraDamage +=  habilidad.danio;
               // tiempoIniciale = habilidad.tiempoInicial;
               // tiempoFinale = habilidad.tiempoFinal;
                // animacionaRealizar = habilidad.nombreAnimacion;

           // }
        }
        comprobarVida();
       // PanelAtributos.instance.ActualizarTextosAtributos(this, )
    }

    public void ActualizarEquipamiento2(List<HabilidadElemental2> habilidadesEquipadas)
    {
        ResetearModificadores2();
        foreach (HabilidadElemental2 habilidad in habilidadesEquipadas)
        {
            
            if(habilidad.tipohabilidad < 2)
             {
            GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage += habilidad.danioBase;
             tiempoIniciale += habilidad.tiempoInicial;
             tiempoFinale += habilidad.tiempoFinal;

                animacion = habilidad.animacion;

            }
            else if (habilidad.tipohabilidad > 1 && habilidad.tipohabilidad <3)
            {
                 GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage2 += habilidad.danioBase;
                tiempoIniciale2 += habilidad.tiempoInicial2;
                tiempoFinale2 += habilidad.tiempoFinal2;

                animacion2 = habilidad.animacion;

            }
            else if (habilidad.tipohabilidad > 2 && habilidad.tipohabilidad < 4)
            {
                 GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage3 += habilidad.danioBase;
                tiempoIniciale3 += habilidad.tiempoInicial3;
                tiempoFinale3 += habilidad.tiempoFinal3;

                animacion3 = habilidad.animacion;

            }
            if (habilidad.tipohabilidad > 3)
            {
                 GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage4 += habilidad.danioBase;
                tiempoIniciale4 += habilidad.tiempoInicial4;
                tiempoFinale4 += habilidad.tiempoFinal4;

                animacion4 = habilidad.animacion;

            }
        }
        // PanelAtributos.instance.ActualizarTextosAtributos(this, )
    }

    private void ResetearModificadores()
    {
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().extraDamage = 0;
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().extraDamage2 = 0;
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().extraDamage3 = 0;
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().extraDamage4 = 0;
        maxhp = 250;

       
       // if (hp > maxhp)
       // {
       //     hp = maxhp;
      //  }
        fireball = false;
        estela2 = false;

    }

    private void comprobarVida()
    {
        if (ultimaMaxHP>proximaMaxHP && ultimaHP>proximaMaxHP)
        {
            hp = proximaMaxHP;
            if(hp>maxhp)
            {
                hp = maxhp;
            }
        }
    }

    private void ResetearModificadores2()
    {
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage = 20;
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage2 = 20;
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage3 = 20;
        GameManager.instance.jugador.GetComponentInChildren<Ataque>().attackDamage4 = 20;

        tiempoIniciale = 0;
        tiempoFinale = 2;

        tiempoIniciale2 = 0;
        tiempoFinale2 = 2;

        tiempoIniciale3 = 0;
        tiempoFinale3 = 2;

        tiempoIniciale4 = 0;
        tiempoFinale4 = 2;

        animacion = null;
        animacion2 = null;
        animacion3 = null;
        animacion4 = null;
    }
    void LanzarFuego()
    {
        GameObject newFuego;
        newFuego = Instantiate(proyectil, transform.GetChild(3).position, transform.rotation);
    }

    void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        for(int position = 0; position<trail.positionCount; position++)
        {
            points.Add(trail.GetPosition(position));
        }
        collider.SetPoints(points);
    }
    //----------------------------------------------------------------
    // void Attack()
    // {
    //   Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    //  Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers2);
    //   foreach (Collider2D enemy in hitEnemies)
    //  {
    //      enemy.GetComponent<EnemyControler>().TakeDamage(attackDamage);

    // }
    //  foreach (Collider2D enemy2 in hitEnemies2)
    //  {
    //      enemy2.GetComponent<EnemigoaDistanciaControlador>().TakeDamage(attackDamage);
    // }
    // }

    // private void OnDrawGizmosSelected()
    // {
    // if (attackPoint == null)
    //   return;
    // Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    // }
    //------------------------------------------------------------------
    // public void ActualizarEquipamiento(List<HabilidadElemental> habilidades)
    // {
    //   foreach (HabilidadElemental equipo in habilidades)
    //   {

    //   }
    // }
}
