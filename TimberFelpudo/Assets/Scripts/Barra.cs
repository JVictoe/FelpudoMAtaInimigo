using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barra : MonoBehaviour
{
    public float escalaBarra;
    public bool terminou;
    public bool comecou;
    public GameObject camecaCena;
    [SerializeField] private Principal gameEngine;
    
    // Start is called before the first frame update
    void Start()
    {
        escalaBarra = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (comecou)
        {
            if (escalaBarra > 0.1f)
            {
                escalaBarra = escalaBarra - 0.15f * Time.deltaTime;
                this.transform.localScale = new Vector2(escalaBarra, 1.0f);
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
        comecou = true;
    }

    public void Aumenta()
    {
        escalaBarra = escalaBarra + 0.040f;
        if (escalaBarra > 1.0f)
        {
            escalaBarra = 1.0f;
        }
    }

}
