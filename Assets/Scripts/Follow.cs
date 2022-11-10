using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var player_position = player.transform.position;
        this.transform.position = new Vector3(player_position.x, player_position.y, -10);
    }
}
