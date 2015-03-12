using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {

	public void changeToScene (int sceneToChangeNumber) {
		Application.LoadLevel(sceneToChangeNumber);
	}
}
