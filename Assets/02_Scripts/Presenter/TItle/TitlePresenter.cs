using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] Text speedText;

    private void Start()
    {
        GameManager.Instance.InitializeData();

        var keyInput = GetComponent<TitleKeyInput>();

        keyInput.LeftArrowDownObservable
                .Where(__ => GameManager.Instance.noteSpeed.Value > 1)
                .Subscribe(__ => GameManager.Instance.noteSpeed.Value--);
        keyInput.RightArrowDownObservable
                .Where(__ => GameManager.Instance.noteSpeed.Value < 10)
                .Subscribe(__ => GameManager.Instance.noteSpeed.Value++);

        keyInput.SpaceKeyDownObservable
                .Subscribe(__ => SceneManager.LoadSceneAsync("game"));

        GameManager.Instance.noteSpeed.AsObservable()
                   .Subscribe(noteSpeed =>
                   {
                       var magnificationFromSpeed = 1 + (noteSpeed - 1) / 10;
                       speedText.text = "Speed\n" + "< x" + noteSpeed + " >\n" + "(Point:x" + magnificationFromSpeed + ")";
                   });
    }
}
