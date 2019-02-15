using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PlayerSave")]
public class PlayerSave : ScriptableObject {
	public string _name;
	public List<SemesterOverview> semesters;
}
public enum LevelType{
	MATHMATIC,HISTORY,SCIENCE,FINAL
}
