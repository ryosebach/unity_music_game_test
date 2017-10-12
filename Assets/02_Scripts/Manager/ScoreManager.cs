using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// This class manages Score. But this does't manage "game-point".
/// This manage magnification of score which is affected by "game-speed" and "game-combo".
/// </summary>
public class ScoreManager : MonoBehaviour
{


    #region Singleton
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (ScoreManager)FindObjectOfType(typeof(ScoreManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(ScoreManager) + "is nothing");
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

    public float addPoint { get; private set; }
    public const float defaultAddPoint = 100;

    private float magnificationFromSpeed;
    private float magnificationFromCombo;

    private void Start()
    {
        GameManager.Instance.noteSpeed.AsObservable()
                   .Subscribe(noteSpeed =>
                   {
                       magnificationFromSpeed = (1 + (noteSpeed - 1) / 10);
                   });

        GameManager.Instance.combo.AsObservable()
                   .Subscribe(combo =>
                   {
                       magnificationFromCombo = 1 + (combo / 10) / 10f;
                       addPoint = defaultAddPoint * magnificationFromCombo * magnificationFromSpeed;
                   });
    }

    /// <summary>
    /// Gets the magnification from combo.This method is expected to call from method which show combo Text.
    /// </summary>
    /// <returns>The magnification from combo.</returns>
    public float GetMagnificationFromCombo() { return magnificationFromCombo; }
}
