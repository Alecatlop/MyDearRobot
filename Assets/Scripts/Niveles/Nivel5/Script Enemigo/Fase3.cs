using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;


public class Fase3 : Estado
{
    public Fase3() : base()
    {
        nombre = ESTADO.FASE3; 
    }

    public override void Entrar()
    {
        base.Entrar();
        enemigoIA.lanzarCorrutinaFase();
        enemigoIA.puedeHacerSuperataque = true;
        enemigoIA.lanzarCorrutinaFase3();
        for (int i = 0; i < enemigoIA.platasformas.Length; i++)
        {
            enemigoIA.platasformas[i].GetComponent<Nivel5Plataformas1>().MoverAbajo();
        }
        enemigoIA.luzruna = true;
       
    }

    public override void Actualizar()
    {
        if (enemigoIA.vidas < 1)
        {
            enemigoIA.Morir();
        }
        else
        {
            bool puede = enemigoIA.PuedeAtacar();

            if (!puede && !enemigoIA.ocupado && !enemigoIA.Superatataqueactivo)
            {
                //enemigoIA.animator.SetBool("caminar", true);
                enemigoIA.transform.LookAt(enemigoIA.jugador.transform.position);
                enemigoIA.agent.speed = 2f;
                enemigoIA.agent.SetDestination(enemigoIA.jugador.transform.position);
            }

            if (enemigoIA.vidas == 1 && enemigoIA.ocupado == false && enemigoIA.puedeHacerSuperataque == true)
            {
                enemigoIA.Superataque();
            }

            enemigoIA.ActivarRayo();

        }
    }

    public override void Salir()
    {
       
        base.Salir();
    }
}

