using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class MusicSelectPresenter : MonoBehaviour
{

    public Transform buttonParentTransform;
    DataManager dataManager;
    public GameObject buttonPrefab;

    void Start()
    {
        Observable.Timer(TimeSpan.FromSeconds(2))
                  .Subscribe(__ =>
                  {
                      int id2 = System.Threading.Thread.CurrentThread.ManagedThreadId;
                      Debug.Log("id2 : " + id2);
                      Debug.Log("calllerrrr");
                      Hoge();
                  });

        Observable.Return(Unit.Default)
                  .Delay(TimeSpan.FromSeconds(1))
                  .Subscribe(__ => Hoge());
        Invoke("Hoge", 3f);
        StartCoroutine(Fuga());

        Hoge();Hoge();Hoge();

    Observable.ToAsync(() => Hoge())().Subscribe(__ => { Debug.Log("done"); });
    }

    void Hoge()
    {
        DataManager.Instance.LoadDataAsync
                   .Subscribe(_ => { });
    }

    IEnumerator Fuga()
    {
        yield return new WaitForSeconds(2);
        DataManager.Instance.LoadDataAsync
           .Subscribe(_ => { }, () => Select());
    }

    void Select()
    {
        var buttons = buttonParentTransform.GetComponentsInChildren<Button>();

        dataManager = DataManager.Instance;
        for (int j = buttons.Length; j < dataManager.datas.Count; j++)
        {
            Instantiate(buttonPrefab, buttonParentTransform);
        }

        var buttonTexts = buttonParentTransform.GetComponentsInChildren<Text>();
        buttons = buttonParentTransform.GetComponentsInChildren<Button>();

        int i = 0;
        foreach (var item in dataManager.datas)
        {
            if (i >= buttonTexts.Length)
                continue;
            buttonTexts[i].text = item.musicData.name;
            buttons[i].onClick.AddListener(() =>
            {
                dataManager.UseData = item;
                GSSA.SpreadSheetSetting.Instance.SetSheetName(item.musicData.name);
            });
            i++;
        }
    }
}
