using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class characterJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;

    private bool isJumping = false;

    private Rigidbody rb;

    private CompositeDisposable disposables = new CompositeDisposable();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Observable.EveryUpdate()
            .Subscribe(_ => Jump())
            .AddTo(disposables);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
