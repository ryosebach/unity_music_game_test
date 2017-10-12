using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class GamePresenter : MonoBehaviour
{

    [SerializeField] Button retryButton;
    [SerializeField] Button rankingButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button backButton;
    [SerializeField] RectTransform pausePanelRectTrans;
    [SerializeField] Text musicNameText;
    [SerializeField] Text pointText;
    [SerializeField] Text comboText;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = ScoreManager.Instance;
        musicNameText.text = DataManager.Instance.UseData.musicData.name;


        retryButton.OnClickAsObservable()
                   .Subscribe(_ =>
                   {
                       SceneManager.LoadSceneAsync("title");
                   });

        rankingButton.OnClickAsObservable()
                     .Subscribe(_ =>
                     {
                         naichilab.RankingLoader.Instance.SendScoreAndShowRanking(GameManager.Instance.point.Value);
                     });

        pauseButton.OnClickAsObservable()
                   .Subscribe(__ => pausePanelRectTrans.localScale = Vector3.one);

        backButton.OnClickAsObservable()
                  .Subscribe(__ => pausePanelRectTrans.localScale = Vector3.zero);

        GameManager.Instance.point.AsObservable()
                   .Subscribe(point =>
                   {
                       pointText.text = point.ToString();
                   });

        GameManager.Instance.combo.AsObservable()
                   .Subscribe(combo =>
                   {
                       comboText.text = "";
                       if (combo > 9)
                           comboText.text = "Combo\n" + combo + "<color=#0000ff>\nx" + scoreManager.GetMagnificationFromCombo() + "</color>";
                       if (combo > 49)
                           comboText.text = "Combo\n" + combo + "<color=#ffd700>\nx" + scoreManager.GetMagnificationFromCombo() + "</color>";
                       if (combo > 99)
                           comboText.text = "Combo\n" + combo + "<color=#ff0000>\nx" + scoreManager.GetMagnificationFromCombo() + "</color>";
                   });

    }
}
