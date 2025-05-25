using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runas5 : MonoBehaviour
{
    public EnemigoIA scriptenemigo;
    public bool activada = false;
    public bool runapintada;
    public bool runaLista = false;
    public float intensidad = 3f;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptenemigo.luzruna == false)
        {
            Apagar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // se activan al pisar 1 fase
        if (other.name == "Jugador" && activada == false && scriptenemigo.fase < 3)
        {
            Material mat = this.GetComponent<MeshRenderer>().material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.yellow * intensidad);
            activada = true;
            scriptenemigo.contadorrunas++;
            runapintada = true;
        }

        if (other.name == "Jugador" && activada == false && scriptenemigo.fase == 3 && runaLista == true)
        {
            Material mat = this.GetComponent<MeshRenderer>().material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.yellow * intensidad);
            activada = true;
            scriptenemigo.contadorrunas++;
            runapintada = true;
        }
    }

    void Apagar()
    {
        activada = false;
        runapintada = false;
        Material mat = this.GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.white);
    }

    public void AvisoRuna()
    {
        Material mat = this.GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.blue * intensidad);
    }

    public void NoAvisoRuna()
    {
        Material mat = this.GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.white);
    }


}
