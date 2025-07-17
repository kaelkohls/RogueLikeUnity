using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATACABLE : MonoBehaviour
{
    private SALUD miSalud;
    private Rigidbody2D miRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        miSalud = GetComponent<SALUD>();
        miRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void RecibirAtaque()
    {
        miSalud.SaludActual -= 1;
    }
    public void RecibirAtaque(int danio, Vector2 direccionDeAtaque)
    {
        miSalud.modificarSaludActual(-danio);
        miRigidbody.AddForce(direccionDeAtaque*danio*100 );
    }
}
