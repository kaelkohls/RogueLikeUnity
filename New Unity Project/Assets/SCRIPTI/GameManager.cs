using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Transform playerSpawnPoint;    
    public GameObject jugador;
    public static GameManager instance;

    public GameObject col;
    public Renderer fondo;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        // Crear Mapa
        for (int i = 0; i < 100; i++)
        {
            Instantiate(col, new Vector2(-40 + i, -18), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f, 0) * Time.deltaTime;
    }
}
