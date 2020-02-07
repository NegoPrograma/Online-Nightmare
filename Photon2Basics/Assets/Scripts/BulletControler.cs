
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletControler : MonoBehaviourPun
{

    public Rigidbody2D rigid;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletTimeCount;
    public Vector2 bulletDirection;
    public Vector2 initialPos;
    public PhotonView bulletView;

   // public PhotonView bulletView;


    public void SetDirection(Vector2 bulletDirection,Vector2 initialPos){
        this.bulletDirection = bulletDirection;
        this.initialPos = initialPos;

    }
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        bulletSpeed = 100f;
        bulletLifeTime = 5f;
        moveBullet();
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletTimeCount > bulletLifeTime){
            Destroy(this.gameObject);
        }
        bulletTimeCount += Time.deltaTime;            
    }

    void OnTriggerEnter2D(Collider2D collision){
        //check if collided with a player prefab that isn't yours
            PlayerController collidedPlayer = collision.gameObject.GetComponent<PlayerController>();
            if(!collidedPlayer.playerView.IsMine && collidedPlayer != null){
                collidedPlayer.takeDamage(-10f);
                bulletView.RPC("bulletDestroy",RpcTarget.AllViaServer);
            }
    }

    public void moveBullet(){
        rigid.AddForce(transform.up*bulletSpeed,ForceMode2D.Force);
    }

    [PunRPC]
    public void bulletDestroy(){
        Destroy(this.gameObject);
    }
}