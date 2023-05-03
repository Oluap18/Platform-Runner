using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlacement : MonoBehaviour
{

    private GameObject playerObject;

    private const string PLAYER_OBJECT = "PlayerObject";

    [SerializeField] private float zDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find( PLAYER_OBJECT );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObject.transform.position + new Vector3(0, 0, zDistance);
    }
}
