using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        enemigoIA.lanzarCorrutinaFase();
        enemigoIA.agent.SetDestination(enemigoIA.centro.transform.position);
        enemigoIA.transform.LookAt(enemigoIA.centro.transform.position);
        //enemigoIA.animator.SetBool("caminar", true);
        for (int i = 0; i < enemigoIA.platasformas.Length; i++)
        {
            enemigoIA.platasformas[i].GetComponent<Nivel5Plataformas1>().MoverArriba();
        }
        enemigoIA.agent.speed = 6f;
        enemigoIA.luzruna = true;
       
    }

    public override void Actualizar()
    {

        if (enemigoIA.vidas == 1)
        {
            enemigoIA.animator.SetBool("terremoto", false);
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

