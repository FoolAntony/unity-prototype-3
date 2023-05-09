using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudio : MonoBehaviour

{
    private AudioSource gameAudio;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !playerControllerScript.gameOver)
        {
            gameAudio.pitch = (float)(double)1.17;
        }
        else
        {
            gameAudio.pitch = (float)(double)1.0;
        }
    }
}
