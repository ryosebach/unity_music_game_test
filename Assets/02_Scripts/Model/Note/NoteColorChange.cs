using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NoteColorChange : MonoBehaviour
{

    MeshRenderer rend;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        GameManager.Instance.combo.AsObservable()
                   .Subscribe(combo =>
                   {
                       rend.material.color = Color.cyan;
                       if (combo > 9)
                           rend.material.color = Color.blue;
                       if (combo > 49)
                           rend.material.color = Color.yellow;
                       if (combo > 99)
                           rend.material.color = Color.red;
                   }).AddTo(this.gameObject);
    }
}
