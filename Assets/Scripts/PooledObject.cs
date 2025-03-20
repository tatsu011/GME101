using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField]
    int _prePoolSize;
    [SerializeField]
    bool _growingPool;
    [SerializeField]
    GameObject _prefab;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < _prePoolSize; i++)
        {
            Instantiate(_prefab, transform).SetActive(false);

        }
    }


    public GameObject GetObject()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
                return transform.GetChild(i).gameObject;
        }
        if(_growingPool)
        {
            return Instantiate(_prefab, transform);
        }
        return new GameObject(); //I dunno if unity likes this.
    }

}
