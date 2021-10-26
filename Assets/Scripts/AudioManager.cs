using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private static AudioManager instance;

    public Sound[] sounds;
    private ArrayList queueSoundList;
    public ArrayList unmuteSounds;

    public static AudioManager instance;


    private void Awake()
    {
        queueSoundList = new ArrayList();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;



            if (s.name == "run")
            {
                s.source.outputAudioMixerGroup.audioMixer.SetFloat("pitchBlend", 1f / 1.5f);
                //  s.source.outputAudioMixerGroup.SetFloat();
            }

        }
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return false;
        }

        if (s.source.isPlaying)
        {
            return true;
        }

        return false;

    }



    public void queueSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound newSound = new Sound();
        newSound = s;

        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        newSound.source.Play();
        queueSoundList.Add(newSound);
    }


    public Sound Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return null;
        }

        s.source.Play();
        return s;

    }


    public void SetVolume(float oldMaxValue, float oldMinValue, float value)
    {

        float OldRange = (oldMaxValue - oldMinValue);
        float NewRange = (1.00f - .1f);
        float NewValue = (((value - oldMinValue * NewRange) / OldRange) + .1f);
        // Debug.Log(NewValue);
        foreach (Sound s in sounds)
        {
            s.volume = Mathf.Clamp(NewValue + s.volume, 0, 1);
        }
    }

    public void MuteSound()
    {

        foreach (Sound s in sounds)
        {
            unmuteSounds.Add(s);
            s.volume = 0.0f;
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }

        s.source.Stop();

    }


    void Start()
    {
        unmuteSounds = new ArrayList();
        // unmuteSounds = new Sound[5];

       // queueSound("bg");
        //  FindObjectOfType<AudioManager>().Play("background");
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
          //  SceneManager.UnloadSceneAsync("SampleScene");
            SceneManager.LoadScene("SampleScene");
        }

        foreach (Sound sound in queueSoundList.ToArray())
        {
            if (sound != null && sound.source.isPlaying == false)
            {
                //  Destroy(sound.source);
                queueSoundList.Remove(sound);
            }
          //  Debug.Log(queueSoundList.Count);
        }
    }
}
