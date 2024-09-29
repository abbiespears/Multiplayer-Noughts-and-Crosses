using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerMovement : MonoBehaviourPunCallbacks
{

    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    PhotonView view;
    
    Vector2 moveDirection = Vector2.zero;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {

        if (view.IsMine)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            transform.position += input.normalized * moveSpeed * Time.deltaTime;
        }
        
    }

    



}
