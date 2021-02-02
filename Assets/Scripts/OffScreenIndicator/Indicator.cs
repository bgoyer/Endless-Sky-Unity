using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OffScreenIndicator
{
    /// <summary>
    /// Assign this script to the indicator prefabs.
    /// </summary>
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private IndicatorType indicatorType;
        private Image indicatorImage;
        public Text distanceText;
        public Text nameText;

        /// <summary>
        /// Gets if the game object is active in hierarchy.
        /// </summary>
        public bool Active
        {
            get
            {
                return transform.gameObject.activeInHierarchy;
            }
        }

        /// <summary>
        /// Gets the indicator type
        /// </summary>
        public IndicatorType Type
        {
            get
            {
                return indicatorType;
            }
        }

        private void Awake()
        {
            indicatorImage = transform.GetComponent<Image>();
        }

        /// <summary>
        /// Sets the image color for the indicator.
        /// </summary>
        /// <param name="color"></param>
        public void SetImageColor(Color color)
        {
            indicatorImage.color = color;
            if (distanceText != null)
            {
                distanceText.color = Color.white;
            }
        }

        /// <summary>
        /// Sets the distance text for the indicator.
        /// </summary>
        /// <param name="value"></param>
        public void SetDistanceText(float value, Target target)
        {
            if (nameText != null)
            {
                nameText.text = target.gameObject.name.ToUpper();
            }
            if (distanceText != null)
            {
                {
                    distanceText.text = value >= 1.496e+4 ? (value / 1.496e+4).ToString("0.0") + "AU" : value >= 0 ? Mathf.Floor(value) + "KM" : "";
                }
            }
        }

        /// <summary>
        /// Sets the distance text rotation of the indicator.
        /// </summary>
        /// <param name="rotation"></param>
        public void SetTextRotation(Quaternion rotation)
        {
            if (distanceText != null)
            {
                distanceText.rectTransform.rotation = rotation;
            }
        }

        /// <summary>
        /// Sets the indicator as active or inactive.
        /// </summary>
        /// <param name="value"></param>
        public void Activate(bool value)
        {
            transform.gameObject.SetActive(value);
        }
    }

    public enum IndicatorType
    {
        BOX,
        ARROW,
        PLANET,
        STAR,
        ENEMYOUTPOST,
        ASTEROIDFIELD,
        OUTPOST
    }
}