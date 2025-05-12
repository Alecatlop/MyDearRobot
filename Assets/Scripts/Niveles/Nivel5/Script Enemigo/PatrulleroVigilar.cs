using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Constructor para VIGILAR
public class PatrulleroVigilar : Estado
{
    public PatrulleroVigilar() : base()
    {
        nombre = ESTADO.VIGILAR; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de andar, calcular los puntos por los que patrulla, etc...
        enemigoIA.GetComponent<MeshRenderer>().material.color = Color.green;
        enemigoIA.agent.SetDestination(enemigoIA.destinos[enemigoIA.a].transform.position);
        base.Entrar();
    }

    public override void Actualizar()
    {
        // Le decimos que se vaya moviendo y patrullando...

       
            siguienteEstado = new Fase1();
            siguienteEstado.inicializarVariables(enemigoIA);
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de VIGILAR a ATACAR.
       
    }

    public override void Salir()
    {
        // Le resetearíamos la animación de andar, detener las corrutinas, o lo que sea...
        base.Salir();
    }
}

