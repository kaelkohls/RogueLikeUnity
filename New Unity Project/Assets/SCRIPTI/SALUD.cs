using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SALUD : MonoBehaviour
{
    public int saludBase;
    private int saludActual;
    public int SaludActual { get { return saludActual; } set { if (value > 0 && value <= saludBase) { saludActual = value; } else if (value>saludBase){ saludActual = saludBase; } else { saludActual = 0; } } }
    void Start()
    {
        SaludActual = saludBase;
    }

    public void modificarSaludActual(int cantidad)
    {
        SaludActual += cantidad;
    }

}
