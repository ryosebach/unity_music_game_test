using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class KeyParticle : MonoBehaviour {

    [SerializeField] private KeyCode keyCode;
    private ParticleSystem pSys;

    private void Awake()
    {
        pSys = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        this.UpdateAsObservable()
            .Where(__ => Input.GetKeyDown(keyCode))
            .Subscribe(__ => pSys.Play());
    }
}
