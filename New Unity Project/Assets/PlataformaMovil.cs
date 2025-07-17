using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;


    public int numero1 = 0;
    public float speed=1;

    public Vector3 MoverHacia;
    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
        //if(target != null)
        //  {
        //     target.parent = null;
        //ida y vuelta automatica y permanente
        //      start = transform.position;
        //      end = target.position;
        //  }
    }
    private void Update()
    {
       
            transform.position = Vector3.MoveTowards(transform.position, MoverHacia, speed * Time.deltaTime);
      






        // if (target != null & numero1 == 1 )
        // {
        //     target.parent = null;
        //     //ida y vuelta automatica y permanente
        //     start = transform.position;
        //    end = target.position;
        //     float fixedSpeed = speed * Time.deltaTime;
        //     transform.position = Vector3.MoveTowards(start, end, fixedSpeed);
        // }
        // if (numero1 == 2) {
        //     float fixedSpeed = speed * Time.deltaTime;
        //     transform.position = Vector3.MoveTowards(end, start, fixedSpeed);
        //  }

    }
    public void Activacion(int numero)
   {
        numero1 = numero;
        //ida y vuelta automatica y permanente
      //  if (transform.position == target.position)
     //   {
      //      target.position = (target.position == start) ? end : start;
      //  }
    }
}
