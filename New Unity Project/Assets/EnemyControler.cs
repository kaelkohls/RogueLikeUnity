using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : ENEMIGO
{
    // Start is called before the first frame update
    CircleCollider2D attackCollider;

    public override void Start()
    {
        espera = 4f;
        distanciaAtaque = 4.1f;
        maxhp = 1000;
        hp = maxhp;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
        limiteCaminataDer = transform.position.x + GetComponent<CircleCollider2D>().radius;
        limiteCaminataIzq = transform.position.x - GetComponent<CircleCollider2D>().radius;
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
                if (atacando)
                {
                    float playbackTime = stateInfor.normalizedTime;
                    if (playbackTime > 0.562 && playbackTime < 0.80) attackCollider.enabled = true;

                    else attackCollider.enabled = false;
                }
                if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);

                break;
        }
    }
    // Update is called once per frame
  
   
  
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
    
}
