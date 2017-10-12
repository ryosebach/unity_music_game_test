using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TitleKeyInput : MonoBehaviour
{

    private Subject<Unit> rightArrowDownSubject = new Subject<Unit>();
    public IObservable<Unit> RightArrowDownObservable
    {
        get
        {
            return rightArrowDownSubject.AsObservable();
        }
    }

    private Subject<Unit> leftArrowDownSubject = new Subject<Unit>();
    public IObservable<Unit> LeftArrowDownObservable
    {
        get
        {
            return leftArrowDownSubject.AsObservable();
        }
    }

    private Subject<Unit> spaceKeyDownSubject = new Subject<Unit>();
    public IObservable<Unit> SpaceKeyDownObservable
    {
        get{
            return spaceKeyDownSubject.AsObservable();
        }
    }

    private void Start()
    {
        this.UpdateAsObservable()
            .Where(__ => Input.GetKeyDown(KeyCode.RightArrow))
            .Subscribe(__ => rightArrowDownSubject.OnNext(Unit.Default));

        this.UpdateAsObservable()
            .Where(__ => Input.GetKeyDown(KeyCode.LeftArrow))
            .Subscribe(__ => leftArrowDownSubject.OnNext(Unit.Default));

        this.UpdateAsObservable()
            .Where(__ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(__ => spaceKeyDownSubject.OnNext(Unit.Default));
    }

}
