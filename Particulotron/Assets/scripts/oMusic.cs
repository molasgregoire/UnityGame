using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oMusic : MonoBehaviour
{
    public GameObject radio;
    public AudioSource channel1;
    public AudioSource mainTheme;

    // Start is called before the first frame update
    void Start()
    {
        radio = new GameObject();
        channel1 = radio.AddComponent<AudioSource>();
        mainTheme = radio.AddComponent<AudioSource>();
        mainTheme.loop = true;
        mainTheme.clip = (AudioClip)Resources.Load("full");
        mainTheme.Play();
        //radio.GetComponent<AudioSource>().loop = true;
        //radio.GetComponent<AudioSource>().PlayOneShot( (AudioClip)Resources.Load("musicTest") ) ;
        //channel1.loop = true;
        //channel1.PlayOneShot((AudioClip)Resources.Load("musicTest"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMe( string titre)
    {
        channel1.PlayOneShot((AudioClip)Resources.Load(titre));
    }
}

