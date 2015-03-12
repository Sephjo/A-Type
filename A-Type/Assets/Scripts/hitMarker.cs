using UnityEngine;
using System.Collections;

public class hitMarker : MonoBehaviour
{
  Vector3 milieu;

  void Start()
  {
	milieu = gameObject.transform.position;
    off();
  }
  // Use this for initialization
  public void on()
  {
	gameObject.transform.position = milieu;
  }
	
  // Update is called once per frame
  public void off()
  {
	gameObject.transform.position = new Vector3(5000, 5000, 0);
  }
}
