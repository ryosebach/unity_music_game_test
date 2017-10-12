using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class DataManager : MonoBehaviour
{
    int count;
	private IObservable<Unit> _resourceLoadObservable;

	public IObservable<Unit> LoadDataAsync
	{
		get
		{
			return _resourceLoadObservable ?? (_resourceLoadObservable =
						Observable.FromCoroutine(LoadDataAsyncCorutine).PublishLast().RefCount());
		}
	}


    #region Singleton
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (DataManager)FindObjectOfType(typeof(DataManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(DataManager) + "is nothing");
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
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region User Datas
    /// <summary>
    /// Music Data List
    /// </summary>
    public List<Data> datas = new List<Data>();

    /// <summary>
    /// UseData
    /// </summary>
    private Data useData;
    public Data UseData
    {
        get
        {
            if (useData == null)
            {
                return datas.First();
            }
            else
            {
                return useData;
            }
        }
        set { useData = value; }
    }
    #endregion

    #region LoadDataAsyncCorutine
    /// <summary>
    /// Loads the data which is expected async corutine.
    /// This method is needed to call from Main-Thread.
    /// </summary>
    /// <returns>The data async corutine.</returns>
    public IEnumerator LoadDataAsyncCorutine()
    {
        Debug.Log("Corutine called times : " + count++);
        foreach (var music in Resources.LoadAll<AudioClip>("Musics"))
        {
            if (datas.Where(x => x.musicData.name == music.name).FirstOrDefault() != null)
                continue;

            var score = Resources.Load<TextAsset>("Scores/" + music.name);
            datas.Add(new Data(score, music));
		}
        yield return null;
    }
    #endregion

    private void Start()
    {
		LoadDataAsync.Subscribe(_ => Debug.Log("hoge"));
    }
}

#region Data
/// <summary>
/// Game Data which is composed "MusicData" and "ScoreData".
/// </summary>
public class Data
{
    public TextAsset scoreData;
    public AudioClip musicData;

    public Data(TextAsset scoreData, AudioClip musicData)
    {
        this.scoreData = scoreData;
        this.musicData = musicData;
    }
}
#endregion