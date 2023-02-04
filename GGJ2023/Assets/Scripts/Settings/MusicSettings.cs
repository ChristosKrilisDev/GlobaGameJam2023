using Data;
using UnityEngine;
namespace Settings
{
    public static class MusicSettings
    {
        private const string MUSIC_KEY = "IsMuted";
        private static Save _save;

        private static AudioSource _music;
        private static AudioSource _sfx;
        
        public static void Init(Save save, AudioSource music, AudioSource sfx)
        {
            _save = save;
            _music = music;
            _sfx = sfx;
            Load();
        }

        public static void OnMusicChange()
        {
            if (PlayerPrefs.HasKey(MUSIC_KEY))
            {
                MusicSave();

                return;
            }

            PlayerPrefs.SetInt(MUSIC_KEY, 1);
            MusicSave();
        }

        private static void MusicSave()
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

        private static void Load()
        {
            var index = PlayerPrefs.GetInt(MUSIC_KEY);
            var isMuted = index == 0;
            _music.mute = !_save.IsMuted;
            _sfx.mute = !_save.IsMuted;

            GameController.Instance.OnMusicChange?.Invoke(!_save.IsMuted);
        }

        
        
        
        public static void PlayOneShot(AudioClip audioClip)
        {
            if (_sfx.isPlaying)
            {
                _sfx.Stop();
            }
            
            _sfx.PlayOneShot(audioClip);
        }
        
        public static void PlayOneShotOver(AudioClip audioClip)
        {
            _sfx.PlayOneShot(audioClip);
        }
        
    }
}
