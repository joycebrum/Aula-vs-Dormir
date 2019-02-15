using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerAttributes  {
    public string nome;
    public int updates;

	public int rendimentoInicial;
    public int rendimentoMaximo;
	public int ganhoPorToque;

	public float cooldownBanheiro;
	public float cooldownAgua;
	
	public float duracaoBanheiro;
	public int rendimentoPorAgua;

	public int passesBanheiro;
	public int passesAgua;
}
