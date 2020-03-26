using IPA;
using IPALogger = IPA.Logging.Logger;
using BS_Utils.Utilities;

namespace RandomSong
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static SemVer.Version Version => IPA.Loader.PluginManager.GetPlugin("RandomSong").Version;

        public static IPALogger Log { get; internal set; }

        [Init]
        public void Init(object thisIsNull, IPALogger log)
        {
            Log = log;
        }

        [OnStart]
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
    }
}
