using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Fase1 : Estado
{
    public Fase1() : base()
    {
        nombre = ESTADO.FASE1; 
    }

    public override void Entrar()
    {
        base.Entrar();
    }

    public override void Actualizar()
    {
            //enemigoIA.disparando = false;
        enemigoIA.PuedeAtacar();

        if (enemigoIA.PuedeAtacar()!)
        {
            enemigoIA.agent.speed = 9f;
            enemigoIA.agent.SetDestination(enemigoIA.jugador.transform.position);
        }

        if (enemigoIA.vidas == 2)
        {
            siguienteEstado = new Fase2(); 
            siguienteEstado.inicializarVariables(enemigoIA);
            faseActual = EVENTO.SALIR; 
        }

    }

    public override void Salir()
    {
 
        base.Salir();
    }

}