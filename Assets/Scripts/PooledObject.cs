using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField]
    int PrePoolSize;
    [SerializeField]
    bool growingPool;
    [SerializeField]
    GameObject prefab;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < PrePoolSize; i++)
        {
            Instantiate(prefab, transform).SetActive(false);

        }
    }


    public GameObject GetObject()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
                return transform.GetChild(i).gameObject;
        }
        if(growingPool)
        {
            return Instantiate(prefab, transform);
        }
        return new GameObject(); //I dunno if unity likes this.
    }

}
