using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour {

	[Header("Objetos Fixos")]
	[SerializeField] private List<GameObject> objetos;
	[SerializeField] private List<Transform> spawnPoints;

	[Header("Preferências")]
	[SerializeField] private int sinalDirecao;
    public static bool coffee = true;
    public float valocidadeAMais;

    public void Start()
    {
        coffee = true;
        valocidadeAMais = 0;
    }
    public void CriaObjeto(float velocity = 5, int movementType = 0){
        GameObject randObj;
        if (coffee)
        {
            randObj = objetos[Random.Range(0, objetos.Count)];
            
        } else
        {
            randObj = objetos[Random.Range(0, objetos.Count - 1)];
        }
        GameObject obj = Instantiate(randObj, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        obj.GetComponent<FlyingObjects>().SetVelocity(velocity + valocidadeAMais);
        obj.GetComponent<FlyingObjects>().SetDirection(sinalDirecao);
        obj.GetComponent<FlyingObjects>().SetMovementType(movementType);
    }
 }
