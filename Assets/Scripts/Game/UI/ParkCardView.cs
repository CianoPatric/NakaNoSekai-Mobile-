using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ParkCardView : MonoBehaviour
    {
        public Image Image;
        public TextMeshProUGUI ParkNameText;
        public TextMeshProUGUI AuthorNameText;

        public void SetData(Sprite image, string parkName, string authorName)
        {
            Image.sprite = image;
            ParkNameText.text = parkName;
            AuthorNameText.text = authorName;
        }
    }
}