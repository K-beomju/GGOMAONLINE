using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}



public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;


    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;


    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void PlaySE(string _soundName)
    {
        for (int i = 0; i < sfxSounds.Length; i++)
        {
            if(_soundName == sfxSounds[i].soundName)
            {
                for(int x = 0; x < sfxPlayer.Length; x++)
                {
                    if(!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfxPlayer[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용중입니다.");
                return;
            }
        }
        Debug.Log("d");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
