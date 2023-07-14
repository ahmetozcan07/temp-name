using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private CompositeDisposable disposables = new CompositeDisposable();

    private LevelGenerator levelGenerator;

    // Start is called before the first frame update
    void Start()
    {
        levelGenerator = gameObject.GetComponent<LevelGenerator>();

        Observable.EveryUpdate()
            .Subscribe(_ => UpdateGame())
            .AddTo(disposables);
    }

    // Update is called once per frame
    void UpdateGame()
    {
        if (Score.Instance.score > 50000)
        {
            levelGenerator.moveAmount = -32;
        }
        if (Score.Instance.score > 20000)
        {
            levelGenerator.moveAmount = -16;
        }
        if (Score.Instance.score > 10000)
        {
            levelGenerator.moveAmount = -12;
        }
        if (Score.Instance.score > 5000)
        {
            levelGenerator.moveAmount = -10;
        }
        if (Score.Instance.score > 1000)
        {
            levelGenerator.moveAmount = -8;
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
