using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fireRate;
    float m_curFireRate;
    public GameObject viewFinder;
    GameObject m_viewFinderClone;
    AudioController m_audio;
    bool m_isShooted;

    public void Awake()
    {
        m_isShooted = false;
        m_curFireRate = fireRate;
        m_audio = FindObjectOfType<AudioController>();

    }
    private void Start()
    {
        if (viewFinder)
        {
            m_viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity); //
        }
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && !m_isShooted  && !GameManager.Ins.IsGameOver) 
        {
            Shot(mousePos);
        }
        if (m_isShooted)
        {
            m_curFireRate -= Time.deltaTime;
            if (m_curFireRate <= 0)
            {
                m_isShooted = false;

                m_curFireRate = fireRate;
            }
            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / fireRate);
        }
        if (m_viewFinderClone)
        {
            m_viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }
    }
    void Shot(Vector3 mousePos)
    {
        m_isShooted = true;

        Vector3 shootDir = Camera.main.transform.position - mousePos;

        shootDir.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4f))
                {
                    //ktra khi ma? b??n tru?ng con chim
                    Bird bird = hit.collider.GetComponent<Bird>();
                    Crow crow = hit.collider.GetComponent<Crow>();
                    if (bird)
                    {
                        bird.Die();
                    }
                    if (crow)
                    {
                        crow.Die();
                    }
                }
            }
        }
        m_audio.playSound(m_audio.shooting);
    }
}
