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
        if (Score.Instance.score > 25000)
        {
            levelGenerator.moveAmount = -75;
        }
        else if (Score.Instance.score > 19000)
        {
            levelGenerator.moveAmount = -65;
        }
        else if (Score.Instance.score > 15000)
        {
            levelGenerator.moveAmount = -50;
        }
        else if (Score.Instance.score > 13000)
        {
            levelGenerator.moveAmount = -40;
        }
        else if (Score.Instance.score > 10000)
        {
            levelGenerator.moveAmount = -32;
        }
        else if (Score.Instance.score > 8000)
        {
            levelGenerator.moveAmount = -24;
        }
        else if (Score.Instance.score > 6000)
        {
            levelGenerator.moveAmount = -16;
        }
        else if (Score.Instance.score > 4000)
        {
            levelGenerator.moveAmount = -12;
        }
        else if (Score.Instance.score > 2000)
        {
            levelGenerator.moveAmount = -10;
        }
        else if (Score.Instance.score > 800)
        {
            levelGenerator.moveAmount = -8;
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
