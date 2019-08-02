using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randrot : MonoBehaviour {
	public float randomnum;
	public bool rottrue=false;
	float speed=55.0f;
	Quaternion startRotation;
	Quaternion endRotation;
	float rotationProgress = -1;
	bool waiting=false;
	// Use this for initialization
	void Start () {
		randomnum = Random.Range (140.0f, 180.0f);
		StartRotating(randomnum);
	}
	



	// Call this to start the rotation
	void StartRotating(float zPosition){

		// Here we cache the starting and target rotations
		startRotation = transform.rotation;
		endRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,zPosition, transform.rotation.eulerAngles.z);

		// This starts the rotation, but you can use a boolean flag if it's clearer for you
		rotationProgress = 0;
	}

	void Update() {
		if (waiting == false) {
			if (rotationProgress < 1 && rotationProgress >= 0) {
				rotationProgress += Time.deltaTime * 1;

				// Here we assign the interpolated rotation to transform.rotation
				// It will range from startRotation (rotationProgress == 0) to endRotation (rotationProgress >= 1)
				transform.rotation = Quaternion.Lerp (startRotation, endRotation, rotationProgress);
			}
		}

		if(rotationProgress > 1)
		{
			//wait
			waiting=true;
			StartCoroutine(wait(2.0f));
			randomnum = Random.Range (140.0f, 190.0f);
			StartRotating(randomnum);
		}
	}
	void dorote()
	{
		Vector3 euler = transform.eulerAngles;
		euler.y += Random.Range(0f, 40f);
		transform.eulerAngles = euler;
	}

	IEnumerator wait(float somesec)
	{
		yield return new WaitForSeconds(somesec);
		waiting = false;
	}
}
