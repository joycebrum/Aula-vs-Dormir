using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="LevelData")]
public class LevelData : ScriptableObject {
	[Header("Geral")]
	public float duracao;
	public float modificadorDificuldade;
	public float[] intervaloTempo = {3,6};
	public float[] intervaloVelocidade = {5,10};


	[Header("Tipos de Movimento")]
	public bool movimentoOndular;
	
}
