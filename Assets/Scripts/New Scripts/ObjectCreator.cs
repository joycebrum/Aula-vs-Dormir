using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour {

	[Header("Objetos Fixos")]
	[SerializeField] private List<GameObject> objetos;
	[SerializeField] private List<Transform> spawnPoints;

	[Header("Preferências")]
	[SerializeField] private int sinalDirecao;
    public bool CoffeInTheGame;//evita ter muitos cafes no jogo
    public bool coffeeBadEffect;
    public float quant;

    public void Start()
    {

        CoffeInTheGame = false;
        coffeeBadEffect = false;
    }
    public void CriaObjeto(float velocity = 5, int movementType = 0){
        GameObject randObj;
        if (CoffeInTheGame)//precisa implementar ainda no CoffeeScript
        {
            randObj = objetos[Random.Range(0, objetos.Count-1)];//retira o ultimo item (cafe)
        }
        else
        {
            randObj = objetos[Random.Range(0, objetos.Count)];
        }
        GameObject obj = Instantiate(randObj,spawnPoints[Random.Range(0,spawnPoints.Count)].position,Quaternion.identity);

        if (coffeeBadEffect)
        {
            obj.GetComponent<FlyingObjects>().SetVelocity(velocity+quant);
        }
        else
        {
            obj.GetComponent<FlyingObjects>().SetVelocity(velocity);
        }
		obj.GetComponent<FlyingObjects>().SetDirection(sinalDirecao);
		obj.GetComponent<FlyingObjects>().SetMovementType(movementType);

		//print("Objeto Criado");
	}
 }
