using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpforce;

    public float health;

    [SerializeField] Transform camheight;

    Rigidbody rb;
    CapsuleCollider col;

    Vector3 movinput;

    float jumpflag;
    float coyotetime;

    bool running;

    private void Start()
    {
        health = 100;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        HandleInput();

        if (Grounded())
            coyotetime = .5f;
        else if (coyotetime >= 0)
            coyotetime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //WASD MOVEMENT
        rb.AddForce(movinput * speed * (running?2:1), ForceMode.Impulse);

        //JUMP DECAY
        if (jumpnumber > 0)
        {
            jumpnumber -= Time.fixedDeltaTime;
            rb.AddForce(Vector3.up * (1 + jumpnumber) * jumpforce, ForceMode.Impulse);
        }

        //JUMP START
        if (jumpflag > 0)
        {
            Jump();
            jumpflag -= Time.deltaTime;

        }
    }

    void HandleInput()
    {
        //WASD
        movinput = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        movinput.Normalize();

        running = Input.GetKey(KeyCode.LeftShift);

        //JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpflag = .4f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (col.height == 2)
                ColSize(1);
            else if (col.height == 1 && !uncrouchqueued)
                StartCoroutine(Uncrouch());
        }
    }
    bool uncrouchqueued;
    IEnumerator Uncrouch()
    {
        uncrouchqueued = true;
        yield return new WaitUntil(() => !Roofed());
        ColSize(2);
        uncrouchqueued = false;
    }

    //PREPARE A JUMP
    float jumpnumber;
    void Jump()
    {
        if (coyotetime > 0)
        {
            jumpflag = -1;
            coyotetime = 0;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);
            jumpnumber = .2f;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    void ColSize(float size)
    {
        if (size > 1 && Roofed())
            return;

        col.center = new Vector3(0, (size / 2), 0);
        col.height = size;
        camheight.localPosition = new Vector3(0, -.5f + size);
        transform.position = transform.position + transform.up * .5f;
    }

    bool Grounded()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position + (transform.up * .5f), -transform.up, .6f, ~0);
        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject && hit.collider.isTrigger == false)
                return true;
        }

        return false;
    }

    bool Roofed()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position + transform.up, transform.up, .6f, ~0);
        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
                return true;
        }

        return false;
    }
}
