using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Handlers
{
    public class ProgressHandler : MonoBehaviour
    {
        [SerializeField] private Image _filler;
        [SerializeField] private TMP_Text _persentageText;

        private void Awake()
        {
            _persentageText.text = "0 %";
            _filler.fillAmount = 0;
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);

            _filler.fillAmount = progress;
            _persentageText.text = string.Format("{0:0} %", progress * 100);
        }
    }
}