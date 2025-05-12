using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Fase1 : Estado
{
    public Fase1() : base()
    {
        nombre = ESTADO.FASE1; // Guardamos el nombre del estado en el que nos encontramos.
    }

    public override void Entrar()
    {
        // Le pondríamos la animación de disparar, o lo que sea...
        
        base.Entrar();
        enemigoIA.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public override void Actualizar()
    {
            siguienteEstado = new PatrulleroVigilar(); // Si el NPC no puede atacar al jugador, lo ponemos a vigilar (por ejemplo).
            siguienteEstado.inicializarVariables(enemigoIA);
            faseActual = EVENTO.SALIR; // Cambiamos de FASE ya que pasamos de ATACAR a VIGILAR.
            enemigoIA.disparando = false;
            enemigoIA.NoDisparar();

        enemigoIA.agent.SetDestination(enemigoIA.jugador.transform.position);

        if (enemigoIA.disparando == false)
            {
                enemigoIA.disparando = true;
                enemigoIA.Disparar();
            }
        
    }

    public override void Salir()
    {
        // Le resetearíamos la animación de disparar, detener las corrutinas, o lo que sea...
        base.Salir();
    }

    public bool PuedeAtacar()
    {
        // ...
        return false; // El NPC NO ESTÁ lo suficientemente cerca para atacar al jugador.
    }
}