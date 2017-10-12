﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NoteManager : MonoBehaviour {

    public Queue<NoteData> noteDatas = new Queue<NoteData>();
	public List<Note> notes = new List<Note> ();

	[SerializeField]
    private Note noteobj;
	[SerializeField]
	private KeyCode[] keys;
	[SerializeField]
	public float missTime = 0.1f;
	[SerializeField]
	private TextAsset data;
	[SerializeField]
	public float displayTime;
    [SerializeField]
    private float startTime = 1.6f;
 
    private float beatTime;
    private int bpm = 150;
    private int posMax = 4;

	// Use this for initialization
	void Start () {
        data = DataManager.Instance.UseData.scoreData;
        displayTime = 6f / GameManager.Instance.noteSpeed.Value;
		beatTime  = 60f / bpm / 4f;

		float time = startTime;
		foreach (string s in data.text.Split(',')) {
			foreach (char c in s) {
				int pos = c - '0';
				if (pos >= 0 && pos < posMax) {
					noteDatas.Enqueue (new NoteData (pos, time));
				}
			}
			time += beatTime;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (noteDatas.Count != 0 && noteDatas.Peek ().Time - displayTime < SoundManager.Instance.Time) {
			var obj = Instantiate (noteobj);
			obj.noteData = noteDatas.Dequeue();
			obj.Initialize ();
			notes.Add (obj);
		}

		for (int i = 0; i < posMax; i++) {
			if (Input.GetKeyDown (keys [i])) {
				Note tempNote = notes.FirstOrDefault (n => n.noteData.Position == i);
				if (tempNote == null) {
					continue;
				}
                if (Mathf.Abs (tempNote.noteData.Time - SoundManager.Instance.Time) < missTime) {
					Perfect (tempNote);
				}
			}
		}
	}
	public void Perfect (Note note){
		notes.Remove (note);
		Destroy (note.gameObject);
        GameManager.Instance.combo.Value++;
		//↓コンボによるポイントの倍率適用
        GameManager.Instance.point.Value += ScoreManager.Instance.addPoint;
	}
	public void Miss(Note note){
		notes.Remove (note);
		Destroy (note.gameObject);
        GameManager.Instance.combo.Value = 0;
	}
}
public class NoteData{
	public int Position;
	public float Time;
	public NoteData (int pos, float time){
		Position = pos;
		Time = time;
	}
}