using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public static GameMaster gm;

    public CameraShake cameraShake;

    void Awake()
	{
			if (gm == null)
			{
				gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
			}
	}

    //cache
    private AudioManager audioManager;

	private void Start()
	{
        //caching
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No Audio Manager found in scene");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB pressed.");
            cameraShake.Shake(0.1f, 0.2f);
        }
    }

}
