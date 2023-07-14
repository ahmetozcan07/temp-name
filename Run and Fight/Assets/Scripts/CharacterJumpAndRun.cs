using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class characterJumpAndRun : MonoBehaviour
{

    public static characterJumpAndRun Instance;

    [SerializeField] private float jumpForce = 5f;


    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool needToJump = false;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    [SerializeField] private Animator animator = null;

    private Rigidbody rb;

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

        rb = GetComponent<Rigidbody>();

        Observable.EveryUpdate()
            .Subscribe(_ => Run())
            .AddTo(disposables);
    }
    private void OnEnable()
    {
        LeanTouch.OnFingerTap += OnFingerTap;
    }
    private void OnDisable()
    {
        LeanTouch.OnFingerTap -= OnFingerTap;
    }

    private void Run()
    {
        animator.SetBool("Grounded", isGrounded);
        if (isGrounded)
        {
            animator.SetFloat("MoveSpeed", 10);
        }
    }

    private void OnFingerTap(LeanFinger finger)
    {
        if (isGrounded)
        {
            needToJump = true;
            JumpingAndLanding();
        }
    }

    public void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && isGrounded && needToJump)
        {
            isGrounded = false;
            m_jumpTimeStamp = Time.time;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  
        }

        if (!isGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("Land");
            isGrounded = true;
            needToJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("Engele çarptý");
            Time.timeScale = 0;
            MenuManager.Instance.EndMenu.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
