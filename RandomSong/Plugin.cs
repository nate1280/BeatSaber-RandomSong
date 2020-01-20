using IPA;
using IPALogger = IPA.Logging.Logger;
using BS_Utils.Utilities;
using UnityEngine.SceneManagement;

namespace RandomSong
{
    public class Plugin : IBeatSaberPlugin
    {
        public static SemVer.Version Version => IPA.Loader.PluginManager.GetPlugin("RandomSong").Metadata.Version;

        public static IPALogger Log { get; internal set; }

        public void Init(object thisIsNull, IPALogger log)
        {
            Log = log;
        }

        public void OnApplicationStart()
        {
            Sprites.ConvertToSprites();

            BSEvents.OnLoad();
            BSEvents.menuSceneLoadedFresh += OnMenuSceneLoadedFresh;
        }

        private void OnMenuSceneLoadedFresh()
        {
            // add BSML mod settings
            //BSMLSettings.instance.AddSettingsMenu("NoFail Check", "NoFailCheck.Views.NoFailCheckSettings.bsml", NoFailCheckSettings.instance);

            // load main mod
            RandomSongUI.Instance.OnLoad();
        }

        #region Unused IPA Methods
        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) { }

        public void OnSceneLoaded(Scene scene, LoadSceneMode arg1) { }

        public void OnSceneUnloaded(Scene scene) { }

        public void OnApplicationQuit() { }

        public void OnLevelWasLoaded(int level) { }

        public void OnLevelWasInitialized(int level) { }

        public void OnUpdate() { }

        public void OnFixedUpdate() { }
        #endregion
    }
}
