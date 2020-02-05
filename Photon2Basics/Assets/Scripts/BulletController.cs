using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviour
{

    public Rigidbody2D rigid;
    public Vector2 bulletDirection;

    public PhotonView bulletView;

    public Bullet(Vector2 bulletDirection){
        this.bulletDirection = bulletDirection;

    }

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        bulletView = gameObject.GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet(bulletDirection);
    }

    public void onCollission2D(Collision2D collision){
        Destroy(gameObject);
    }

    public void moveBullet(Vector2 bulletDirection){

        rigid.velocity = bulletDirection;

    }
}
