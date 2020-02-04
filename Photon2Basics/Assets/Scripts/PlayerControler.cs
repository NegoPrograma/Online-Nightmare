using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControler : MonoBehaviour
{

    public float playerSpeed = 5f;
    public PhotonView playerView;

    private Rigidbody2D rigid;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        playerView = gameObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(!playerView.IsMine)
            return;
        PlayerMove();
        PlayerRotation();
    }

    public void PlayerMove(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rigid.velocity = new Vector2(x,y);
    }

    public void PlayerRotation(){
        Vector3 mousePos = Input.mousePosition;

        mousePos = Camera.main.WorldToScreenPoint(mousePos);

        Vector2 direction = new Vector2(
            mousePos.x-transform.position.x,
            mousePos.y-transform.position.y
        );

        transform.up = direction;

    }
}
