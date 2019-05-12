using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="SemesterOverview")]
public class SemesterOverview : ScriptableObject {
	public float[] cr = {-1,-1,-1,-1,-1}; //0 = math;1 = history;2 = science;3 = all/3; 4 = final test
}
