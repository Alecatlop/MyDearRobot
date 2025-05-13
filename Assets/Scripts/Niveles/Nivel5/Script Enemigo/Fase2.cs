using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Fase2 : Estado
{
    public Fase2() : base()
    {
        nombre = ESTADO.FASE2; 
    }

    public override void Entrar()
    {
        base.Entrar();

    }

    public override void Actualizar()
    {
        enemigoIA.Golpearsuelo();

        if (enemigoIA.vidas == 1)
        {
            siguienteEstado = new Fase3();
            siguienteEstado.inicializarVariables(enemigoIA);
            faseActual = EVENTO.SALIR;
        }
        
       
    }

    public override void Salir()
    {
       
        base.Salir();
    }
}

