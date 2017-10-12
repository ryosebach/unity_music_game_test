using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameManager)FindObjectOfType(typeof(GameManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(GameManager) + "is nothing");
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

    public FloatReactiveProperty noteSpeed = new FloatReactiveProperty(1f);
    public FloatReactiveProperty point = new FloatReactiveProperty();
    public IntReactiveProperty combo = new IntReactiveProperty();

    public void InitializeData()
    {
        noteSpeed = new FloatReactiveProperty(1f);
        point = new FloatReactiveProperty();
        combo = new IntReactiveProperty();
    }
}
