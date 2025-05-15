using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;


public class Fase2 : Estado
{
    public Fase2() : base()
    {
        nombre = ESTADO.FASE2; 
    }

    public override void Entrar()
    {
        base.Entrar();
        enemigoIA.agent.SetDestination(enemigoIA.centro.transform.position);
        enemigoIA.agent.speed = 6f;
        enemigoIA.lanzarCorrutinaFase();
        enemigoIA.Golpearsuelo();
        enemigoIA.luzruna = true;
    }

    public override void Actualizar()
    {

        if (enemigoIA.vidas == 1)
        {
            enemigoIA.ocupado = true;
            siguienteEstado = new Fase3();
            siguienteEstado.inicializarVariables(enemigoIA);
            faseActual = EVENTO.SALIR;
        }
        else enemigoIA.ActivarRayo();


    }

    public override void Salir()
    {
        enemigoIA.TerminarCorrutinas();
        base.Salir();
    }
}

