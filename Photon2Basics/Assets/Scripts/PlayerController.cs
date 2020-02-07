using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{

    public float playerSpeed = 43f;
    public PhotonView playerView;

    private Rigidbody2D rigid;


    public Vector2 playerAim;
    public GameObject bulletSprite;
    public GameObject bulletSpawn;

    public Image playerHealthHeader;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        playerView = gameObject.GetComponent<PhotonView>();
        playerMaxHealth = 100f;
        playerCurrentHealth = playerMaxHealth;
    }

    void Update()
    {
        if(playerView.IsMine){
            PlayerMove();
            PlayerRotation();
            /*if(Input.GetKeyDown(KeyCode.U))
            HealthUpdate(-10f);
            */
            if(Input.GetKeyDown(KeyCode.E)){
            //    ShootBullet();
                playerView.RPC("ShootBulletRPC",RpcTarget.AllBuffered);
            }
        }
    }

    public void PlayerMove(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rigid.velocity = new Vector2(x,y)*playerSpeed;
    }

    public void PlayerRotation(){
        Vector3 mousePos = Input.mousePosition;

        mousePos = Camera.main.WorldToScreenPoint(mousePos);

        playerAim = new Vector2(
            mousePos.x-transform.position.x,
            mousePos.y-transform.position.y
        );

        transform.up = playerAim;
    }

    /*public void ShootBullet(){
        PhotonNetwork.Instantiate("MyBulletPhotonView",bulletSpawn.transform.position,bulletSpawn.transform.rotation);
    }*/


    [PunRPC]
    public void ShootBulletRPC(){
        Debug.Log("I got called, bitch!");
        Instantiate(this.bulletSprite,this.bulletSpawn.transform.position,this.bulletSpawn.transform.rotation);
    }


    public void takeDamage(float value){
        playerView.RPC("takeDamageNetwork",RpcTarget.AllBuffered,value);
    }


    [PunRPC]
    void takeDamageNetwork(float damage){
        HealthUpdate(damage);
    }


    public void HealthUpdate(float damage){
        this.playerCurrentHealth +=damage;
        this.playerHealthHeader.fillAmount = this.playerCurrentHealth/100;
    }
}
