using UnityEngine;
using System.Collections;

public class PlayerCompass : MonoBehaviour
{
	public GameObject ExitCompass;
	public GameObject ExitCompassTail;

	public float CompassDistance = 0.5f;

	private GameObject _exit;
	// Use this for initialization
	void Start()
	{
		_exit = GameObject.FindWithTag("ExitSign");
	}

	// Update is called once per frame
	void Update()
	{
		if (_exit)
		{
			PointAtExit();
		}
	}

	private void PointAtExit()
	{
		Debug.Log("Exit found");
		Vector3 directionToExit = _exit.transform.position - transform.position;
        directionToExit = directionToExit.normalized;

		ExitCompass.transform.position = transform.position + (directionToExit * CompassDistance);
        ExitCompassTail.transform.position = transform.position + (directionToExit * (CompassDistance - 0.2f));
        Debug.Log(directionToExit);
	}
}

