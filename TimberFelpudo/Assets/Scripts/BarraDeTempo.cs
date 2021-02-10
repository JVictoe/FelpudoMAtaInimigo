using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeTempo : MonoBehaviour
{

    public Slider barraTempo;
    public bool terminou;
    public bool comecou;
     float tempoMaximo = 1f;
    [SerializeField] private Principal gameEngine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         
      //  DiminuiTempo();

        if (comecou)
        {
            if (barraTempo.value > 0.1f)
            {
                barraTempo.value -= 0.15f * Time.deltaTime;
                //this.transform.localScale = new Vector2(barraTempo.value, 1.0f);
            }
            else
            {
                if (!terminou)
                {
                    terminou = true;
                    gameEngine.FimDeJogo();
                }
            }
        }

    }

    public void Comecou()
    {
        barraTempo.minValue = 0;
        barraTempo.maxValue = tempoMaximo;
        barraTempo.value = tempoMaximo;

        comecou = true;
    }

    public void Aumenta()
    {
        barraTempo.value += 0.040f;

        if (barraTempo.value > 1.0f)
        {
            barraTempo.value = 1.0f;
        }
    }
}
