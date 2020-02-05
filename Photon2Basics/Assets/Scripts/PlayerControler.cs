using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControler : MonoBehaviour
{

    public float playerSpeed = 40f;
    public PhotonView playerView;

    private Rigidbody2D rigid;


    public Vector2 playerAim;
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
        bulletSpawn = GameObject.Find("spawnBullet");
    }

    void Update()
    {
        if(!playerView.IsMine)
            return;
        PlayerMove();
        PlayerRotation();
        /*if(Input.GetKeyDown(KeyCode.U))
            HealthUpdate(-10f);
        */
        if(Input.GetKeyDown(KeyCode.E))
                ShootBullet();


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

    public void ShootBullet(){
        PhotonNetwork.Instantiate("myBullet",bulletSpawn.transform.position,bulletSpawn.transform.rotation);
    }

    public void HealthUpdate(float damage){
        playerCurrentHealth +=damage;
        //o percentual da imagem vai de 0.0 até 1, e como o HP maximo é 100, deve-se dividir por 100 para garantir proporcionalidade
        playerHealthHeader.fillAmount = playerCurrentHealth/100;
    }
}
