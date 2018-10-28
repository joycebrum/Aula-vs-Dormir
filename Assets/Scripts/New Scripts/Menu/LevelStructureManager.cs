using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStructureManager : MonoBehaviour {

	public PlayerSave playerSave;
    public List<LevelData> levelTypes;

    public GameObject levelPrefab;
    public GameObject menu;

	[Header("UI Elements")]
	public Text playerName;
	public Text semesterText;
	public Text finalCR;
	public List<GameObject> classes;

	public int viewSemester;

	private void Start(){
		viewSemester = GetCurrentSemesterIndex();
		ShowSemesterOfIndex(viewSemester);
	}
	public void ShowSemesterOfIndex(int number){
		int last = GetLastLevelType(number);
		print(last);
		SetPlayerName();
		SetSemesterText(number);
		SetFinalCR(number);

		for(int i = 0; i < 4; i++){
			if(playerSave.semesters[number].cr[i] == -1){
				classes[i].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "??";
			}else{
				classes[i].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = playerSave.semesters[number].cr[i].ToString();
			}
			classes[i].transform.GetChild(2).GetComponent<Button>().interactable = false;
		}
		if(last != -1) classes[last].transform.GetChild(2).GetComponent<Button>().interactable = true;
	}
	public int GetCurrentSemesterIndex(){
		return playerSave.semesters.Count - 1;
	}
	public void SetSemesterText(int index){
		semesterText.text = (index+1) +"º Semestre";
	}
	public void SetPlayerName(){
		playerName.text = playerSave._name;
	}
	public void SetFinalCR(int index){
		finalCR.text = GetFinalCR(index).ToString();
	}
	public int GetLastLevelType(int semester){
        print("semester" + semester);
		int i;
		for(i = 0; i < 4; i++){
			if(playerSave.semesters[semester].cr[i] == -1){
				break;
			}
		}
        print("level " + i);
		return i;
	}
	public float GetFinalCR(int semester){
		float total = 0;
		int i;
		for(i = 0; i < 4; i++){
			if(playerSave.semesters[semester].cr[i] == -1){
				if(i == 0) return 0;
				break;
			}
			total += playerSave.semesters[semester].cr[i];
		}
		total /= (i);
		return Mathf.Round(total*10)/10;
	}
	public void ChangeSemester(int direction){
		if(viewSemester + direction > GetCurrentSemesterIndex() || viewSemester + direction < 0) return;
		viewSemester += direction;
		ShowSemesterOfIndex(viewSemester);
	}
	public void StartLevel(int i){
        StartCoroutine(StartNewLevel(i));
	}
    private IEnumerator StartNewLevel(int i)
    {
        var go = Instantiate(levelPrefab);
        yield return new WaitForEndOfFrame();
        GameObject.Find("LevelManager").GetComponent<LevelManager>().InitLevel(levelTypes[i]);
        menu.SetActive(false);
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelLoop() );
    }
}
