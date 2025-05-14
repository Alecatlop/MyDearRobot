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

        if (enemigoIA.vidas == 2 && !enemigoIA.ocupado)
        {
            Debug.Log("Fase 2");
            siguienteEstado = new Fase2(); 
            siguienteEstado.inicializarVariables(enemigoIA);
            faseActual = EVENTO.SALIR; 
        }
        else
        {
            bool puede = enemigoIA.PuedeAtacar();

            if (!puede && !enemigoIA.ocupado)
            {
                enemigoIA.agent.speed = 2f;
                enemigoIA.agent.SetDestination(enemigoIA.jugador.transform.position);
            }
        } 

    }

    public override void Salir()
    {
 
        base.Salir();
    }

}