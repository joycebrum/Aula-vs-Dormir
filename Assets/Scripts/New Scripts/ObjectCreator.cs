using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour {

	[Header("Objetos Fixos")]
	[SerializeField] private List<GameObject> objetos;
	[SerializeField] private List<Transform> spawnPoints;

	[Header("Preferências")]
	[SerializeField] private int sinalDirecao;
    public bool Coffee;
    public float quant;

    public void Start()
    {
        Coffee = false; //sempre vai ser false para o criador bom e o ruim
        quant = 0;
    }
    public void CriaObjeto(float velocity = 5, int movementType = 0){
        if (!Coffee)
        {
            GameObject randObj = objetos[Random.Range(0, objetos.Count)];
            GameObject obj = Instantiate(randObj, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            obj.GetComponent<FlyingObjects>().SetVelocity(velocity + quant);
            obj.GetComponent<FlyingObjects>().SetDirection(sinalDirecao);
            obj.GetComponent<FlyingObjects>().SetMovementType(movementType);
        }
		//print("Objeto Criado");
	}
 }
