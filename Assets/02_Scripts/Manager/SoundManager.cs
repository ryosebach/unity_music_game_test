using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public float Time { get { return audioSource.time; } }
    public BoolReactiveProperty IsPlaying = new BoolReactiveProperty();

	private AudioSource audioSource;

	#region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
	{
		get
		{
			if (instance == null)
			{
                instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

				if (instance == null)
				{
                    Debug.LogError(typeof(SoundManager) + "is nothing");
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (this != Instance)
		{
			Destroy(gameObject);
			return;
		}
	}
	#endregion


	void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = DataManager.Instance.UseData.musicData;
        audioSource.Play();
        this.UpdateAsObservable()
            .Subscribe(__ => IsPlaying.Value = audioSource.isPlaying);

    }
}
