namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;
    using VRTK.Controllables;

    public class LangButton : MonoBehaviour
    {
        public VRTK_BaseControllable controllable;
        public string outputOnMax = "Maximum Reached";
        public string outputOnMin = "Minimum Reached";
        public int slideOffset = 1;
        private AudioSource buttonBoop;

        protected virtual void OnEnable()
        {
            buttonBoop = GetComponent<AudioSource>();
            controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
            controllable.ValueChanged += ValueChanged;
            controllable.MaxLimitReached += MaxLimitReached;
            controllable.MinLimitReached += MinLimitReached;
        }

        protected virtual void ValueChanged(object sender, ControllableEventArgs e)
        {

        }

        protected virtual void MaxLimitReached(object sender, ControllableEventArgs e)
        {
            if (outputOnMax != "")
            {
                Debug.Log(outputOnMax);

                // If slideOffset is 1, moves to the right, if slideOffset is -1, moves to the left
                if (slideOffset < 0 && LanguageSelection.currentSlide + slideOffset < 0)
                {
                    LanguageSelection.currentSlide = LanguageSelection.NUM_SLIDES - 1;
                }
                else
                {
                    LanguageSelection.currentSlide = (LanguageSelection.currentSlide + slideOffset) % LanguageSelection.NUM_SLIDES;
                }
                LanguageSelection.changeFlag = true;
                buttonBoop.Play();

                // To persist the data, save the current language to Player Preferences
                VocabManager.languageID = LanguageSelection.currentSlide;
                PlayerPrefs.SetInt("languageID", VocabManager.languageID);

            }


        }

        protected virtual void MinLimitReached(object sender, ControllableEventArgs e)
        {
            if (outputOnMin != "")
            {
                Debug.Log(outputOnMin);
            }
        }
    }
}