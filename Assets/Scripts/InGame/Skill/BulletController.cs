using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletController : MonoBehaviour,IDestroy
{
    public void DestroyObject()
    {
        GameManager.Instance.Player.BulletPool.Return(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(GameObject obj)
    {
        this.transform.position = GameManager.Instance.Player.transform.position;
        //TODO obj‚Ü‚Å“®‚­
    }
}
