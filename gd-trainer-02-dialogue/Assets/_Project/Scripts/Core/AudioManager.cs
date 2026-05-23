using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Объяснение: ивент для удаленного вызова Звуков
    public Action<AudioClip> playSound;
    //Объяснение: ивент для удаленного смены Музыки
    public Action<AudioClip> playMusic;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    //Объяснение: переменное хранящее громкость звуков, сохраняет/загружает через PlayerPrefs
    public float SoundVolume
    {
        get => PlayerPrefs.GetFloat("SoundVolume",1);
        set => PlayerPrefs.SetFloat("SoundVolume",value);
    }
    //Объяснение: переменное хранящее громкость музыки, сохраняет/загружает через PlayerPrefs
    public float MusicVolume
    {
        get => PlayerPrefs.GetFloat("MusicVolume",1);
        set => PlayerPrefs.SetFloat("MusicVolume",value);
    }
    
    private void Awake()
    {
        _musicSource = gameObject.AddComponent<AudioSource>();
        _soundSource = gameObject.AddComponent<AudioSource>();
        
        _musicSource.loop = true;
        
        _musicSource.volume = MusicVolume;
        _soundSource.volume = SoundVolume;
        
        playSound += PlaySound;
        playMusic += PlayMusic;
    }

    private void OnDestroy()
    {
        playSound -= PlaySound;
        playMusic -= PlayMusic;
    }

    //Вызов: Изменение громкости звуков
    public void ChangeSoundVolume(float volume)
    {
        SoundVolume = volume;
        _soundSource.volume = SoundVolume;
    }
    //Вызов: Изменение громкости музыки
    public void ChangeMusicVolume(float volume)
    {
        MusicVolume = volume;
        _musicSource.volume = MusicVolume;
    }
    //Вызов: Проигрывает звук
    private void PlaySound(AudioClip clip)
    {
        if(_soundSource != null)
            _soundSource.PlayOneShot(clip);
    }
    //Вызов: Меняет музыку и проигрывает его
    private void PlayMusic(AudioClip clip)
    {
        if (_musicSource == null)
            return;
        _musicSource.clip = clip;
        _musicSource.Play();
    }
}
