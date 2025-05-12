using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Fase3 : Estado
{
    public Fase3() : base()
    {
        nombre = ESTADO.FASE2; 
    }

    public override void Entrar()
    {
        base.Entrar();
    }

    public override void Actualizar()
    {
        enemigoIA.PuedeAtacar();

        if (enemigoIA.PuedeAtacar()!)
        {
            enemigoIA.agent.speed = 9f;
            enemigoIA.agent.SetDestination(enemigoIA.jugador.transform.position);
        }


        if (enemigoIA.vidas == 0)
        {
            enemigoIA.Morir();
        }
    }

    public override void Salir()
    {
       
        base.Salir();
    }
}

