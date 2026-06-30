using UnityEngine;
using UnityEngine.Pool;

public class ArrowPooling : MonoBehaviour
{
    [SerializeField] private Arrow arrowPrefab;

    [SerializeField] private int defalutCapacity = 20;
    [SerializeField] private int maxSize = 50;

    public ObjectPool<Arrow> pool;

    private void Awake()
    {
        pool = new ObjectPool<Arrow>(
            CreatePool,
            EnablePool,
            DisablePool,
            DestroyPool,
            true,
            defalutCapacity,
            maxSize
            );
    }

    public Arrow ArrowPool()
    {
        return pool.Get();
    }

    private Arrow CreatePool()
    {
        Arrow arrow = Instantiate(arrowPrefab);
        arrow.SetPool(pool);
        return arrow;
    }

    private void EnablePool(Arrow obj)
    {
        if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.position = transform.position;
            rb.rotation = transform.eulerAngles.z;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            obj.transform.position = this.transform.position;
            obj.transform.rotation = this.transform.rotation;
        }

        obj.gameObject.SetActive(true);
    }

    private void DisablePool(Arrow obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void DestroyPool(Arrow obj) 
    { 
        Destroy(obj);
    }

}
