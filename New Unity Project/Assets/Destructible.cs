using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public string destroyState;
    public float timeforDisable;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ataque")
        {
            anim.Play(destroyState);
            yield return new WaitForSeconds(timeforDisable);

            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        //ESTO ES SI QUIERO DESTRUIR EL OBJETO,EN ESTE CASO ES UN COFRE,NO QUIERO QUE SE DESTRUYA
       // AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
       // if(stateInfo.IsName(destroyState) && stateInfo.normalizedTime >=1)
       // {
           // Destroy(gameObject);
       // }
    }
}
