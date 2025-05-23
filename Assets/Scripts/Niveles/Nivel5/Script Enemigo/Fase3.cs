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
        enemigoIA.animator.SetBool("caminar", true);
        enemigoIA.lanzarCorrutinaFase();
        enemigoIA.puedeHacerSuperataque = true;
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

            if (!puede && !enemigoIA.ocupado && !enemigoIA.Superataqueactivo)
            {
                enemigoIA.agent.speed = 2f;
                enemigoIA.agent.SetDestination(enemigoIA.jugador.transform.position);
            }

            if (enemigoIA.vidas == 1 && enemigoIA.ocupado == false && enemigoIA.puedeHacerSuperataque == true && enemigoIA.contadorrunas < 6)
            {
                enemigoIA.IniciarSuperataque();
            }

            enemigoIA.ActivarRayo();

        }
    }

    public override void Salir()
    {
       
        base.Salir();
    }
}

