using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowSpeed;
    private ObjectPool<Arrow> pool;
    private Coroutine co;
    private Coroutine hitCo;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPool(ObjectPool<Arrow> pool)
    {
        this.pool = pool;
    }

    void OnEnable()
    {
        Vector2 dir = new Vector2(1f, 1f).normalized;
        rb.linearVelocity = dir * arrowSpeed;
        co = StartCoroutine(ReleaseTime());
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }

    IEnumerator ReleaseTime()
    {
        yield return null;
        gameObject.transform.SetParent(null);
        yield return new WaitForSecondsRealtime(5.0f);
        pool.Release(this);
        co = null;
    }

    IEnumerator DeleteTime()
    {
        yield return null;
        pool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitCo = StartCoroutine(DeleteTime());
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            hitCo = StartCoroutine(DeleteTime());
        }
    }

}
