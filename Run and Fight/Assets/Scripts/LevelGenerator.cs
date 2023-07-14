using UniRx;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private float index = 0;
    
    [SerializeField] private GameObject[] levels;
    
    [SerializeField] private GameObject startlevel;

    public float moveAmount = -4;
    private CompositeDisposable disposables = new CompositeDisposable();


    // Start is called before the first frame update
    void Start()
    {
        GameObject start = Instantiate(startlevel, transform);
        start.transform.position = new Vector3(0, 0, 0);

        int random1 = Random.Range(0, levels.Length);
        int random2 = Random.Range(0, levels.Length);
        int random3 = Random.Range(0, levels.Length);
        int random4 = Random.Range(0, levels.Length);

        GameObject nextLevel1 = Instantiate(levels[random1], transform);
        nextLevel1.transform.position = new Vector3(0, 0, 19.9f);

        GameObject nextLevel2 = Instantiate(levels[random2], transform);
        nextLevel2.transform.position = new Vector3(0, 0, 39.8f);

        GameObject nextLevel3 = Instantiate(levels[random3], transform);
        nextLevel3.transform.position = new Vector3(0, 0, 59.7f);

        GameObject nextLevel4 = Instantiate(levels[random4], transform);
        nextLevel4.transform.position = new Vector3(0, 0, 79.6f);

        Observable.EveryUpdate().
            Subscribe(_ => GenerateAndMoveLevels())
            .AddTo(disposables);
    }

    private void GenerateAndMoveLevels()
    {
        gameObject.transform.position += new Vector3(0, 0, moveAmount * Time.deltaTime);

        if (transform.position.z <= index - 19.9f)
        {


            int random4 = Random.Range(0, levels.Length);

            GameObject nextLevel5 = Instantiate(levels[random4], transform);
            nextLevel5.transform.position = new Vector3(0, 0, 79.6f);

            index = index - 19.9f;
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
