using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform Player;

    Vector3 vel = Vector3.zero;

        public float Tiemsua = .15f;


    private void FixedUpdate()
    {
        Vector3 Pospla = Player.position;
        Pospla.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, Pospla, ref vel, Tiemsua);
    }


}
