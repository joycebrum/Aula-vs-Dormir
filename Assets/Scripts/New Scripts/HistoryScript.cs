using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoryScript : MonoBehaviour {

    [SerializeField] private List<string> textList;
    [SerializeField] private List<string> nextScene;
    public Text textElement;
    public static int position;
	// Use this for initialization
	void Start ()
    {
        textElement.text = textList[position];  
	}

    // Update is called once per frame
    private void OnMouseDown()
    {
        SceneManager.LoadScene(nextScene[position]);
    }
}