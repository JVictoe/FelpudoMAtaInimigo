using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Principal : MonoBehaviour
{

    public GameObject jogadorFelpudo;
    public GameObject felpudoIdle;
    public GameObject felpudoBate;

    public GameObject barrel;
    public GameObject enemyRight;
    public GameObject enemyLeft;

    float escalaJogadorHorizontal;

    private List<Blocos> listaBlocos;

    bool ladoPersonagem;

    public AudioClip somBate;
    public AudioClip somPerde;

    public Text pontuacao;
    int score;

    bool comecou;
    bool acabou;
    [SerializeField] private BarraDeTempo gameEngine;

    // Start is called before the first frame update
    void Start()
    {
        escalaJogadorHorizontal = transform.localScale.x;

        felpudoBate.SetActive(false);
        listaBlocos = new List<Blocos>();
        CreateBarrelStart();

        pontuacao.transform.position = new Vector2(Screen.width / 2,
            Screen.height / 2);
        pontuacao.text = "Toque para iniciar!";
        pontuacao.fontSize = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (!acabou)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if(!comecou)
                {
                    comecou = true;
                    gameEngine.Comecou();
                }
                GetComponent<AudioSource>().PlayOneShot(somBate);
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    BateDireita();
                }
                else
                {
                    BateEsquerda();
                }
                listaBlocos.RemoveAt(0);
                ReposicionaBloco();
                ConfereJogada();
            }
        }
    }

    void BateDireita()
    {
        ladoPersonagem = true;
        felpudoIdle.SetActive(false);
        felpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(1.1f, jogadorFelpudo.transform.position.y);
        jogadorFelpudo.transform.localScale = new Vector2(-escalaJogadorHorizontal,
            jogadorFelpudo.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        listaBlocos[0].AnimaDireita();
    }

    void BateEsquerda()
    {
        ladoPersonagem = false;
        felpudoIdle.SetActive(false);
        felpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(-1.1f, jogadorFelpudo.transform.position.y);
        jogadorFelpudo.transform.localScale = new Vector2(escalaJogadorHorizontal,
            jogadorFelpudo.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        listaBlocos[0].AnimaEsquerda();
    }

    void VoltaAnimacao()
    {
        felpudoIdle.SetActive(true);
        felpudoBate.SetActive(false);
    }

    GameObject CreateNewBarrel(Vector2 posicao)
    {
        GameObject newBarrel;

        if (Random.value > 0.5f || listaBlocos.Count < 2)
        {
            newBarrel = Instantiate(barrel);
        }
        else
        {
            if (Random.value > 0.5f)
            {
                newBarrel = Instantiate(enemyRight);
            }
            else
            {
                newBarrel = Instantiate(enemyLeft);
            }
        }
        newBarrel.transform.position = posicao;

        return newBarrel;
    }

    void CreateBarrelStart()
    {
        for (int i = 0; i <= 7; i++)
        {
            GameObject objetoBarril = CreateNewBarrel(new Vector2(0, -2.1f + (i * 0.99f)));
            Blocos blocos = objetoBarril.GetComponent<Blocos>();
            listaBlocos.Add(blocos);
        }
    }

    void ReposicionaBloco()
    {
        GameObject objetoBarril = CreateNewBarrel(new Vector2(0, -2.1f + (8 * 0.99f)));
        Blocos blocos = objetoBarril.GetComponent<Blocos>();
        listaBlocos.Add(blocos);

        for (int i = 0; i <= 7; i++)
        {
            listaBlocos[i].transform.position = new Vector2(
                listaBlocos[i].transform.position.x,
                listaBlocos[i].transform.position.y - 0.99f
                );
        }
    }

    void ConfereJogada()
    {
        if (listaBlocos[0].gameObject.CompareTag("barril"))
        {
            if ((listaBlocos[0].name == "barrilesq1(Clone)" && !ladoPersonagem) ||
                (listaBlocos[0].name == "barrildir1(Clone)" && ladoPersonagem))
            {
                FimDeJogo();
            }
            else
            {
                MarcaPonto();
            }
        }
        else
        {
            MarcaPonto();
        }
    }

    void MarcaPonto()
    {
        score++;
        pontuacao.text = score.ToString();
        pontuacao.fontSize = 15;
        pontuacao.color = new Color(0f, 0f, 0f, 255f);
        gameEngine.Aumenta();
    }

    public void FimDeJogo()
    {
        acabou = true;
        felpudoBate.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);
        felpudoIdle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);

        jogadorFelpudo.GetComponent<Rigidbody2D>().isKinematic = false;
        

        if (ladoPersonagem)
        {
            jogadorFelpudo.GetComponent<Rigidbody2D>().AddTorque(300.0f);
            jogadorFelpudo.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 3.0f);
        }
        else
        {
            jogadorFelpudo.GetComponent<Rigidbody2D>().AddTorque(-300.0f);
            jogadorFelpudo.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, 3.0f);
        }
        GetComponent<AudioSource>().PlayOneShot(somPerde);
        Invoke("RecarregaCena", 2);
    }

    void RecarregaCena()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
