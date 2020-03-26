using HMUI;
using IPA.Utilities;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace RandomSong.Extensions
{
    public static class ButtonExtensions
    {
        public static void SetButtonText(this Button _button, string _text)
        {
            Polyglot.LocalizedTextMeshProUGUI localizer = _button.GetComponentInChildren<Polyglot.LocalizedTextMeshProUGUI>();

            if (localizer != null)
            {
                GameObject.Destroy(localizer);
            }

            var tmpUgui = _button.GetComponentInChildren<TextMeshProUGUI>();

            if (tmpUgui != null)
            {
                tmpUgui.text = _text;
            }
        }

        public static void SetButtonIcon(this Button _button, Sprite _icon)
        {
            if (_button.GetComponentsInChildren<Image>().Count() > 1)
                _button.GetComponentsInChildren<Image>().First(x => x.name == "Icon").sprite = _icon;
        }

        public static HoverHint AddHintText(this Button _button, string text)
        {
            var parent = _button.transform as RectTransform;
            var hoverHint = parent.gameObject.AddComponent<HoverHint>();
            hoverHint.text = text;
            var hoverHintController = Resources.FindObjectsOfTypeAll<HoverHintController>().First();
            hoverHint.SetField("_hoverHintController", hoverHintController);
            return hoverHint;
        }

    }
}
