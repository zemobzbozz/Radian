﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public GameObject defSrc, bgmSrc;
    public static SoundManager script;
    public AudioClip grassFootStep, radianceSource,dash;
    public AudioSource lastBGM = null, curBGM = null;
    public float smooth = 0.1f;
    public float bgmVolume = 0f;
    void Start()
    {
        script = this;
	
    }
    public AudioSource playOnListener(AudioClip clip, float volume = 1f)
    {
        GameObject src = (GameObject)Instantiate(defSrc, CharCtrl.script.gameObject.transform.position, transform.rotation);
        src.transform.SetParent(CharCtrl.script.gameObject.transform);
        src.GetComponent<AudioSource>().clip = clip;
        src.GetComponent<AudioSource>().volume = volume;
        src.GetComponent<AudioSource>().Play();
        return src.GetComponent<AudioSource>();
    }

    public AudioSource playOn(Transform pos, AudioClip clip, float volume = 1f)
    {
        GameObject src = (GameObject)Instantiate(defSrc, pos.position, Quaternion.identity);
        src.GetComponent<AudioSource>().clip = clip;
        src.GetComponent<AudioSource>().volume = volume;
        src.GetComponent<AudioSource>().Play();
        return src.GetComponent<AudioSource>();
    }

    public AudioSource playAndFollow(Transform pos, AudioClip clip, float volume = 1f)
    {
        GameObject src = (GameObject)Instantiate(defSrc, pos.position, Quaternion.identity);
        src.transform.SetParent(pos);
        src.GetComponent<AudioSource>().clip = clip;
        src.GetComponent<AudioSource>().volume = volume;
        src.GetComponent<AudioSource>().Play();
        return src.GetComponent<AudioSource>();
    }

    public AudioSource playBGM(AudioClip clip, float volume)
    {
        GameObject src = (GameObject)Instantiate(bgmSrc, transform.position, transform.rotation);
        src.transform.SetParent(gameObject.transform);
        src.GetComponent<AudioSource>().clip = clip;
        src.GetComponent<AudioSource>().volume = 0f;
        bgmVolume = volume;
        src.GetComponent<AudioSource>().Play();
        if (lastBGM)
            Destroy(lastBGM);
        lastBGM = curBGM;
        return curBGM = src.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (curBGM)
            curBGM.volume += (bgmVolume - curBGM.volume) * smooth;
        if (lastBGM)
        {
            lastBGM.volume = Mathf.Max(lastBGM.volume * smooth - 0.01f, 0f);
            if (lastBGM.volume == 0f)
                Destroy(lastBGM.gameObject);
        }
    }
}
