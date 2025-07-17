using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pausa : MonoBehaviour
{
    public GameObject despausar;
    public int activo;

    bool active;
    Canvas canvas;
    public bool abierto = false;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            despausar.GetComponent<pausaInventario>().activo = 0;
            activo = 1;
            active = !active;
            canvas.enabled = active;
            Time.timeScale = (active) ? 0 : 1f;
        }
        if (activo < 1) {
            active = false;
            canvas.enabled = active;
        }
    }
    public void EmpezarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CerrarJuego()
    {
        Application.Quit();
    }

}
