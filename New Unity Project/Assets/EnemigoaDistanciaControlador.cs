using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoaDistanciaControlador : ENEMIGO
{
    PolygonCollider2D attackCollider;
    public GameObject pro;
    public GameObject proyectil;
    //Vector3 prod;
    // Start is called before the first frame update
    public override void Start()
    {
        maxhp = 800;
        espera = 5f;
        distanciaAtaque = 10f;
        hp = maxhp;
      //  pro = GameObject.FindGameObjectWithTag("Proyectileador");
        //prod = pro.transform.position;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        limiteCaminataDer = transform.position.x + GetComponent<CircleCollider2D>().radius;
        limiteCaminataIzq = transform.position.x - GetComponent<CircleCollider2D>().radius;

        attackCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        attackCollider.enabled = false;
    }
    public override void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Muerte") && stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
        actualespera -= Time.deltaTime;
        anim.SetFloat("Velocidad", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("EstoyEnElSuelo", estoyenSuelo);

        switch (comportamiento)
        {
            case tipoComportamiento.persecución:
                AnimatorStateInfo stateinf = anim.GetCurrentAnimatorStateInfo(0);
                bool atacanding = stateinf.IsName("Ataque");
                if (!atacanding)
                    {
                    attackCollider.enabled = false;
                }
                    break;
            case tipoComportamiento.ataque:

                AnimatorStateInfo stateInfor = anim.GetCurrentAnimatorStateInfo(0);
                bool atacando = stateInfor.IsName("Ataque");
                if (!atacando)
                {
                    attackCollider.enabled = false;
                }
                if (!atacando && actualespera < 0)
                {
                    LanzarFuego();
                    //Invoke("LanzarFuego", 0.1f);
                }
                if (atacando)
                {
                    float playbackTime = stateInfo.normalizedTime;
                    if (playbackTime > 0 && playbackTime < 0.80) attackCollider.enabled = true;
                    else attackCollider.enabled = false;
                }
                if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);
                break;
        }
    // Update is called once per frame
   
   
    void LanzarFuego()
    {
        GameObject newFuego;
        newFuego = Instantiate(proyectil, pro.transform.position, transform.rotation);
    }
   // public void Atacado()
   // {
     //   if (--hp <= 0)
     //   {
     //       anim.Play("Muerte");
     //   }
     //   else
     //   {
     //       actualespera = espera - 3;
     //       anim.Play("recibeDaño");
     //   }
  //  }
  

}
}
