using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerCompass : MonoBehaviour
{
	public GameObject ExitCompass;
	public GameObject ExitCompassTail;

    public float ExitCompassDistance = 0.5f;

    public GameObject CollectableCompass;
    public GameObject CollectableCompassTail;

	public float CollectableCompassDistance = 0.3f;

    private GameObject _exit;

	private List<GameObject> _collectables = new List<GameObject>();
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

		PointAtNearestCollectable();
	}

	private void PointAtExit()
	{
		Vector3 directionToExit = _exit.transform.position - transform.position;
        directionToExit = directionToExit.normalized;

		ExitCompass.transform.position = transform.position + (directionToExit * ExitCompassDistance);
        ExitCompassTail.transform.position = transform.position + (directionToExit * (ExitCompassDistance - 0.2f));
	}

	private void PointAtNearestCollectable()
	{
        _collectables = GameObject.FindGameObjectsWithTag("Collectable").ToList();
		if (_collectables.Count > 0)
        {
            CollectableCompass.SetActive(true);
            CollectableCompassTail.SetActive(true);
            var nearest = _collectables.Aggregate(_collectables[0], (GameObject arg1, GameObject arg2) =>
			{
                var directionTo = arg1.transform.position - transform.position;

                var directionToTwo = arg2.transform.position - transform.position;

				if (directionTo.magnitude > directionToTwo.magnitude)
				{
					return arg2;
				}
                return arg1;
			});

            Vector3 directionToCollectable = nearest.transform.position - transform.position;
            directionToCollectable = directionToCollectable.normalized;

            CollectableCompass.transform.position = transform.position + (directionToCollectable * CollectableCompassDistance);
            CollectableCompassTail.transform.position = transform.position + (directionToCollectable * (CollectableCompassDistance - 0.1f));
        }
		else
		{
			CollectableCompass.SetActive(false);
            CollectableCompassTail.SetActive(false);
        }
    }
}

