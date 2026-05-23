using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChooseOneLogic : MonoBehaviour
    {
        [SerializeField] private Button[] options;
        [SerializeField] private Image selectionIndicator;
        private int _currentIndex;

        private void Awake()
        {
            foreach (var option in options)
            {
                option.onClick.AddListener(() => OnOptionSelected(option));
            }
            
            _currentIndex = 0;
        }

        private void OnOptionSelected(Button selectedOption)
        {
            for (var i = 0; i < options.Length; i++)
            {
                if (options[i] != selectedOption) continue;

                _currentIndex = i;
                MoveIndicatorToOption(i);
                break;
            }
        }

        private void MoveIndicatorToOption(int i)
        {
            selectionIndicator.transform.position = options[i].transform.position;
        }

        public int GetSelectedOptionIndex()
        {
            return _currentIndex + 2;
        }
    }
}