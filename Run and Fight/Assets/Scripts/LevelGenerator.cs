using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private float index = 0;
    
    [SerializeField] private GameObject[] levels;
    
    [SerializeField] private GameObject startlevel;

    private CompositeDisposable disposables = new CompositeDisposable();


    // Start is called before the first frame update
    void Start()
    {
        GameObject start = Instantiate(startlevel, transform);
        start.transform.position = new Vector3(0, 0, 0);

        int random1 = Random.Range(0, levels.Length);
        int random2 = Random.Range(0, levels.Length);
        int random3 = Random.Range(0, levels.Length);

        GameObject nextLevel1 = Instantiate(levels[random1], transform);
        nextLevel1.transform.position = new Vector3(0, 0, 20);

        GameObject nextLevel2 = Instantiate(levels[random2], transform);
        nextLevel2.transform.position = new Vector3(0, 0, 40);

        GameObject nextLevel3 = Instantiate(levels[random3], transform);
        nextLevel3.transform.position = new Vector3(0, 0, 60);

        Observable.EveryUpdate().
            Subscribe(_ => GenerateAndMoveLevels())
            .AddTo(disposables);
    }

    private void GenerateAndMoveLevels()
    {
        gameObject.transform.position += new Vector3(0, 0, -4 * Time.deltaTime);

        if (transform.position.z <= index - 20)
        {


            int random4 = Random.Range(0, levels.Length);

            GameObject nextLevel4 = Instantiate(levels[random4], transform);
            nextLevel4.transform.position = new Vector3(0, 0, 60);

            index = index - 20;
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
