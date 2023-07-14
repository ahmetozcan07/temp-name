using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static  Score Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private float timer;
    public int score;

    private CompositeDisposable disposables = new CompositeDisposable();
    // Start is called before the first frame update
    void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Observable.EveryUpdate()
            .Subscribe(_ => UpdateScore())
            .AddTo(disposables);
    }

    void UpdateScore()
    {
        timer += Time.deltaTime;

        if (timer > 0.1f)
        {

            score += 10;

            scoreText.text = "Score :" + score.ToString();

            timer = 0;
        }

        if(score > PlayerPrefs.GetInt("HighScore", 0)){
            PlayerPrefs.SetInt("HighScore", score);
        }
        
        highScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
