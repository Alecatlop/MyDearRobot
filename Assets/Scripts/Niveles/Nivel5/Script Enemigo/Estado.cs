using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Estado
{

    public EnemigoIA enemigoIA;


    public void inicializarVariables(EnemigoIA _enemigoIA)
    {
        enemigoIA = _enemigoIA;
    }


    public enum ESTADO
    {
        FASE3, FASE2, FASE1
    }


    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

    public ESTADO nombre;
    protected EVENTO faseActual;
    protected Estado siguienteEstado;


    public Estado()
    {
    }


    public virtual void Entrar() { faseActual = EVENTO.ACTUALIZAR; }
    public virtual void Actualizar() { faseActual = EVENTO.ACTUALIZAR; }
    public virtual void Salir() { faseActual = EVENTO.SALIR; }


    public Estado Procesar()
    {
        if (faseActual == EVENTO.ENTRAR) Entrar();
        if (faseActual == EVENTO.ACTUALIZAR) Actualizar();
        if (faseActual == EVENTO.SALIR)
        {
            Salir();
            return siguienteEstado;
        }
        return this;
    }

}

