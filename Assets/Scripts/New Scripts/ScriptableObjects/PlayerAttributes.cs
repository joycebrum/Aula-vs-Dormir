using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PlayerAttributes")]
public class PlayerAttributes : ScriptableObject {
	[Header("Atributos")]
	public int rendimentoInicial;
	public int ganhoPorToque;

	public float cooldownBanheiro;
	public float cooldownAgua;
	
	public float duracaoBanheiro;
	public int rendimentoPorAgua;

	public int passesBanheiro;
	public int passesAgua;
}
