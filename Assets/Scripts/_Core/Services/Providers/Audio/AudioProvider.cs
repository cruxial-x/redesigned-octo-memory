using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioProvider : MonoBehaviour, IProvider<IGameAudio>
{
  [System.Serializable]
  private class VolumeControl
  {
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public bool Initialized => masterVolumeSlider != null && musicVolumeSlider != null && sfxVolumeSlider != null;
  }
  [SerializeField] private GameAudio _audioHandler;
  [SerializeField] private VolumeControl _volumeControl;

  private void Awake()
  {
    IGameAudio audioHandler = _audioHandler;
    ServiceLocator.RegisterService(audioHandler);
    if (!_volumeControl.Initialized)
    {
      return;
    }
    _volumeControl.masterVolumeSlider.onValueChanged.AddListener(_audioHandler.SetMasterVolume);
    _volumeControl.masterVolumeSlider.value = _audioHandler.masterVolume;
    _volumeControl.musicVolumeSlider.onValueChanged.AddListener(_audioHandler.SetMusicVolume);
    _volumeControl.musicVolumeSlider.value = _audioHandler.musicVolume;
    _volumeControl.sfxVolumeSlider.onValueChanged.AddListener(_audioHandler.SetSFXVolume);
    _volumeControl.sfxVolumeSlider.value = _audioHandler.sfxVolume;
  }

  public IGameAudio Get()
  {
    return _audioHandler;
  }
  void Update()
  {
    if (_audioHandler.VolumeChanged)
    {
      _audioHandler.AdjustVolume();
    }
  }
}