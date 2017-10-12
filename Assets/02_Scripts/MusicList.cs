using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicList : MonoBehaviour {
	public string[] musicTitles;
	public string[] musicDifficulty;
	public float[] musicBpms;
	public GameObject titleText;
	public GameObject diffText;
	public GameObject bpmText;
	public int selectNumber = 1;
	public static string selectedMusicTitle;
}
