using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator m_Ani;
    private SpriteRenderer m_spriteRenderer;
    private Rigidbody2D m_Rb;

    public Transform FootPos;

    bool Moving = false;
    bool IsGround;
    int JumpCount = 1;
    float MoveX;
    float MoveSpeed = 7;
    float JumpPower = 10;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_Ani = GetComponent<Animator>();
        m_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveX = Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.smoothDeltaTime;
        this.transform.position = new Vector2(this.transform.position.x + MoveX, this.transform.position.y);

        if (MoveX > 0)
        {
            m_Ani.SetBool("Run", true);
            m_spriteRenderer.flipX = false;
        }
        else if (MoveX < 0)
        {
            m_Ani.SetBool("Run", true);
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_Ani.SetBool("Run", false);
        }

        MovingCheck();
        Crouch();
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.W) && !m_Ani.GetBool("Jumping"))
        {
            if(JumpCount > 0)
            {
                m_Rb.velocity = Vector2.up * JumpPower; 
                m_Ani.SetBool("Jumping", true);
                JumpCount = 0;
            }
        }

        IsGround = Physics2D.OverlapCircle(FootPos.position, 0.01f, LayerMask.GetMask("Ground"));

        if (IsGround)
        {
            m_Ani.SetBool("Jumping", false);
            JumpCount = 1;
        }
        else if (!IsGround)
        {
            m_Ani.SetBool("Jumping", true);
            JumpCount = 0;
        }
    }

    void MovingCheck()
    {
        if (MoveX > 0 || MoveX < 0 || m_Ani.GetBool("Jumping") == true) Moving = true;
        else Moving = false;
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.S) && !Moving)
        {
            m_Ani.SetBool("Crouch", true);
        }
        else
        {
            if (m_Ani.GetBool("Crouch") == true) m_Ani.SetBool("Crouch", false);
        }
    }

}
