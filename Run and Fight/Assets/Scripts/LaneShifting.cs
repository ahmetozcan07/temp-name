using UnityEngine;
using Lean.Touch;
using System;
using UniRx;
using UnityEditor.Build.Reporting;
using TMPro;

public class LaneShifting : MonoBehaviour
{
    private Boolean lane1 = false;
    private Boolean lane2 = true;
    private Boolean lane3 = false;

    private Vector3 target;

    private Boolean isMoving = false;

    private CompositeDisposable disposables = new CompositeDisposable();

    private void Start()
    {
        Debug.Log(LeanTouch.CurrentSwipeThreshold + "  swipe threshhold  ");

        Observable.EveryUpdate()
            .Subscribe(_ => Shifting(target))
            .AddTo(disposables);
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += OnFingerSwipe;
    }
    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= OnFingerSwipe;
    }

    private void OnFingerSwipe(LeanFinger finger)
    {
        Debug.Log("ekran kaydýrýldý.");
        if (characterJumpAndRun.Instance.isGrounded)
        {
            if (finger.SwipeScreenDelta.x < 0) //sol
            {
                if (lane2)
                {
                    lane1 = true;
                    lane2 = false;
                    target = new Vector3(-4, 0, 0);
                }
                else if (lane3)
                {
                    lane2 = true;
                    lane3 = false;
                    target = new Vector3(0, 0, 0);
                }

            }
            else if (finger.SwipeScreenDelta.x > 0) //sað
            {
                if (lane1)
                {
                    lane2 = true;
                    lane1 = false;
                    target = new Vector3(0, 0, 0);
                }
                else if (lane2)
                {
                    lane3 = true;
                    lane2 = false;
                    target = new Vector3(4, 0, 0);
                }
            }
            isMoving = true;
            characterJumpAndRun.Instance.needToJump = true;
            characterJumpAndRun.Instance.JumpingAndLanding();

        }

    }

    private void Shifting(Vector3 target)
    {
        float step = 5 * Time.deltaTime;

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }

        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget <= 0.01f)
        {
            isMoving = false;
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}