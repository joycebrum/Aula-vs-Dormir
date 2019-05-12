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
    public static string textValue;
	// Use this for initialization
	void Start ()
    {
        textElement.text = textValue;  
	}

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene(nextScene[position]);
        }
    }
}