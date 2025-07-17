using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma2 : MonoBehaviour
{
    public int numero1=0;
    public Transform StartPoint;
    public Transform EndPoint;

    public float speed=1;

    private Vector3 MoverHacia;

    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(numero1==1)
       {
            MoverHacia = EndPoint.position;
            transform.position = Vector3.MoveTowards(transform.position, MoverHacia, speed * Time.deltaTime);
        }
        if(numero1==2)
        {
            MoverHacia = StartPoint.position;
            transform.position = Vector3.MoveTowards(transform.position, MoverHacia, speed * Time.deltaTime);
        }
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
