using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
	

    public NoteData noteData;
    public Transform[] targets;
	public GameObject noteManagerObj;

    float startPosZ = 29;
    NoteManager noteManager;

	public void Initialize () {
		Vector3 pos = new Vector3 (targets [noteData.Position].position.x, 0, startPosZ);
		transform.position = pos;
		noteManager = noteManagerObj.GetComponent<NoteManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (noteData == null)
			return;
        float t = noteData.Time - SoundManager.Instance.Time;
		float rate = t / noteManager.displayTime;
		Vector3 targetPos = targets [noteData.Position].position;
		transform.localPosition = new Vector3 (	transform.position.x,
												transform.position.y,
												Mathf.Lerp (targetPos.z, startPosZ, rate));
		if (t < -noteManager.missTime) {
			noteManager.Miss (this);
		}
		
	}
}
