using System.Collections;
using System.Collections.Generic;
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
        enemigoIA.puedeHacerSuperataque = true;

        enemigoIA.lanzarCorrutinaFase3();
        
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

            if (!puede && !enemigoIA.ocupado)
            {
                enemigoIA.agent.speed = 2f;
                enemigoIA.agent.SetDestination(enemigoIA.jugador.transform.position);
            }
            else
            {
                if (enemigoIA.vidas == 1 && enemigoIA.ocupado == false && enemigoIA.puedeHacerSuperataque == true)
                {
                    enemigoIA.Superataque();
                }
            }

        }
    }

    public override void Salir()
    {
       
        base.Salir();
    }
}

