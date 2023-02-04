﻿using Data;
using UnityEngine;
namespace Settings
{
    public class MusicSettings
    {
        private const string MUSIC_KEY = "IsMuted";
        private readonly Save _save;

        private AudioSource _music;
        private AudioSource _sfx;

        public MusicSettings(Save save, AudioSource music, AudioSource sfx)
        {
            _save = save;
            _music = music;
            _sfx = sfx;
            Load();
        }

        public void OnMusicChange()
        {
            if (PlayerPrefs.HasKey(MUSIC_KEY))
            {
                MusicSave();

                return;
            }

            PlayerPrefs.SetInt(MUSIC_KEY, 1);
            MusicSave();
        }

        private void MusicSave()
        {
            var index = PlayerPrefs.GetInt(MUSIC_KEY);
            var isMuted = index == 0;
            //reverse
            _save.IsMuted = !isMuted;
            index = _save.IsMuted ? 0 : 1;
            PlayerPrefs.SetInt(MUSIC_KEY, index);

            Debug.Log(_save.IsMuted);
            _music.mute = !_save.IsMuted;
            _sfx.mute = !_save.IsMuted;
            GameController.Instance.OnMusicChange?.Invoke(!_save.IsMuted);
        }

        private void Load()
        {
            var index = PlayerPrefs.GetInt(MUSIC_KEY);
            var isMuted = index == 0;
            _music.mute = !_save.IsMuted;
            _sfx.mute = !_save.IsMuted;

            GameController.Instance.OnMusicChange?.Invoke(!_save.IsMuted);
        }
    }
}
