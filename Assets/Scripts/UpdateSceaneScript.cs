using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSceaneScript : MonoBehaviour {

    public static int updates=0;
    public Text tempt;
    public static Text numUpdates;
    public Button[] botao;
    private int updatesantigo;
	// Use this for initialization
	void Start ()
    {
        numUpdates = tempt;
        float temp = Player_Controler.num;
        if(temp>=0.75)
        {
            updates++;
        }
        numUpdates.text = "Updates possiveis = " + updates.ToString();
        updatesantigo = updates;
	}

    private void Update()
    {
        if (updates != updatesantigo)
        {
            if (updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
            updatesantigo = updates;
        }
    }


}
