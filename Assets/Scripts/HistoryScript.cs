using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoryScript : MonoBehaviour {

    private string proxfase;
    public Text textElement;
    public static int hfase;
	// Use this for initialization
	void Start ()
    {
        switch(hfase)
        {
            case 1:
                proxfase = "Fase01";
                textElement.text = "Estou muito preocupado com voce, Jhonatan, suas notas estao caindo muito e voce esta sempre dormindo em aula. Se nas proximas aulas seu rendimento nao melhorar eu terei que chamar seus pais para conversar. Nao quero mais voce dormindo nas minhas aulas, entendeu?";
                break;
            case 2:
                proxfase = "Fase02";
                textElement.text = "Para ser um bom aluno e importante que voce se acostume a copiar a materia sempre que necessario. E voce so sabera quando e necessario se estiver prestando bastante atencao. Nao me decepcione, Jhonatan";
                break;
            case 3:
                proxfase = "Fase03";
                textElement.text = "Numa sala de aula sempre tem conversas paralelas e voce tem que saber recusar educadamente cada pedido de conversa e deixas as fofocas para um momento mais adequado. Cuidado para nao acabar se distraindo e aceitando uma conversa sem querer, seu rendimento pode ser prejudicado";
                break;
            case 4:
                proxfase = "Fase04";
                textElement.text = "Esta sendo dificil se manter acordado nao e? Comprei uma nova maquina de cafe, sempre que o sono estiver pesado voce pode tomar um pouco, ajuda. Mas cuidado para nao tomar cafe demais, faz mal.";
                break;
            default:
                textElement.text = "Lembre-se: Eu estou lhe observando. Trate de se comportar bem.";
                proxfase = "Fase0" + hfase;
                break;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //SceneManager.LoadScene(proxfase);
            SceneManager.LoadScene("Prototype");//temporariamenta so para a build
        }
	}
}