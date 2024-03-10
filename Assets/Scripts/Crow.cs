using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    
    public float xSpeed;
    public float minYSpeed;
    public float maxYSpeed;

    public Crow(float speed)
    {
        xSpeed = speed;
    }

    public GameObject deathVfx;
    Rigidbody2D m_rb;
    bool m_moveLeftOnStart;

    GameManager m_gm;
    GameGUIManager m_ggm;
    bool m_isDead;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gm = FindObjectOfType<GameManager>();
        m_ggm = FindObjectOfType<GameGUIManager>();

    }
    private void Start()
    {
        m_moveLeftOnStart = true;
        RandomMovingDirection();
    }
    private void Update()
    {
        m_rb.velocity = m_moveLeftOnStart ?
        new Vector2(-xSpeed, Random.Range(minYSpeed, maxYSpeed))
        : new Vector2(xSpeed, Random.Range(minYSpeed, maxYSpeed));

        Flip();

    }

    public void RandomMovingDirection()
    {
        m_moveLeftOnStart = transform.position.x > 0 ? true : false;

    }
    void Flip()
    {
        if (m_moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }
    }
    public void Die()
    {
        m_gm.BirdKilled = m_gm.BirdKilled - 1;

        GameManager.Ins.BirdKilled--;

        if (deathVfx)
        {
            GameObject bld = Instantiate(deathVfx, transform.position, Quaternion.identity);
            Destroy(bld, 1f);
        }
       
        Destroy(gameObject);
        
        m_ggm.UpdateKilledCounting(m_gm.BirdKilled);
    }
}
