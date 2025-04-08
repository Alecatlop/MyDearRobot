using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runa : MonoBehaviour
{
    public GameObject runa;
    public GameObject camara;
    public bool activado = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Pintar()
    {
        activado = true;

        Material mat = this.GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.yellow);
        camara.SetActive(true);

        yield return new WaitForSeconds(2f);
        Material runaMat = runa.GetComponent<MeshRenderer>().material;
        runaMat.EnableKeyword("_EMISSION");
        runaMat.SetColor("_EmissionColor", Color.yellow);
        yield return new WaitForSeconds(2f);
        camara.SetActive(false);

        yield return null; // Opcional, por si en el futuro quieres añadir un tiempo o animación

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            StartCoroutine(Pintar());
        }
    }
}
