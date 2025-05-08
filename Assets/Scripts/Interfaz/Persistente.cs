using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Persistente : MonoBehaviour
{
    public float valorcalidad;
    public float volumenmusica;
    public float volumenefectos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Persistente"));
        //sliderbrillo = GameObject.Find("SliderBrillo");
        //valorcalidad = sliderbrillo.GetComponent<Slider>().value;
    }
}
