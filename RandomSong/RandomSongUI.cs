using HMUI;
using IPA.Utilities;
using RandomSong.Extensions;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RandomSong
{
    public class RandomSongUI : MonoBehaviour
    {
        public bool initialized = false;

        private Button _randomButton;

        private static RandomSongUI _instance = null;
        public static RandomSongUI Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject("RandomSong").AddComponent<RandomSongUI>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        internal void OnLoad()
        {
            // get containers/transforms
            var _levelSelectionFlowCoordinator = Resources.FindObjectsOfTypeAll<SoloFreePlayFlowCoordinator>().First();
            var _levelSelectionNavigationController = _levelSelectionFlowCoordinator.GetField<LevelSelectionNavigationController, LevelSelectionFlowCoordinator>("_levelSelectionNavigationController");
            var _levelCollectionViewController = _levelSelectionNavigationController.GetField<LevelCollectionViewController, LevelSelectionNavigationController>("_levelCollectionViewController");

            var _levelCollectionTableView = _levelCollectionViewController.GetField<LevelCollectionTableView, LevelCollectionViewController>("_levelCollectionTableView");
            var _tableView = _levelCollectionTableView.GetField<TableView, LevelCollectionTableView>("_tableView");

            RectTransform viewControllersContainer = FindObjectsOfType<RectTransform>().First(x => x.name == "ViewControllers");

            _randomButton = Instantiate(Resources.FindObjectsOfTypeAll<Button>().Last(x => (x.name == "PracticeButton")), _levelCollectionViewController.rectTransform, false);
            _randomButton.onClick = new Button.ButtonClickedEvent();
            _randomButton.onClick.AddListener(() =>
            {
                // get beatmaps
                var beatmaps = _levelCollectionTableView.GetField<IPreviewBeatmapLevel[], LevelCollectionTableView>("_previewBeatmapLevels").ToList();

                // get a random song
                var songToPick = beatmaps.OrderBy(b => Guid.NewGuid()).First();

                // get the index
                var randomRow = beatmaps.IndexOf(songToPick);

                // offset row if pack header is showing
                if (_levelCollectionTableView.GetField<bool, LevelCollectionTableView>("_showLevelPackHeader"))
                {
                    randomRow++;
                }

                // scroll to song
                _tableView.ScrollToCellWithIdx(randomRow, TableViewScroller.ScrollPositionType.Center, false);

                // select song
                _tableView.SelectCellWithIdx(randomRow, true);
            });
            _randomButton.name = "RandomSongButton";

            (_randomButton.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
            (_randomButton.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
            (_randomButton.transform as RectTransform).anchoredPosition = new Vector2(32f, 34.5f);
            (_randomButton.transform as RectTransform).sizeDelta = new Vector2(12f, 6f);

            var hoverHints = _randomButton.GetComponents<HoverHint>();
            hoverHints.ToList().ForEach(hoverHint => Destroy(hoverHint));

            _randomButton.AddHintText("Pick a random song");
            _randomButton.SetButtonText("");
            _randomButton.SetButtonIcon(Sprites.RandomIcon);
            _randomButton.GetComponentsInChildren<UnityEngine.UI.Image>().First(x => x.name == "Stroke").sprite = Resources.FindObjectsOfTypeAll<Sprite>().First(x => x.name == "RoundRectSmallStroke");

            var _randomIconLayout = _randomButton.GetComponentsInChildren<HorizontalLayoutGroup>().First(x => x.name == "Content");
            _randomIconLayout.padding = new RectOffset(0, 0, 0, 0);
        }
    }
}
