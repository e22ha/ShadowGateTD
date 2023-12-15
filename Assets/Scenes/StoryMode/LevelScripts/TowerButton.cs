// TowerButton.cs

using Scenes.StoryMode.LevelScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.StoryMode.Scripts
{
    public class TowerButton : MonoBehaviour
    {
        private NodeBuilder _nodeBuilder;
        private int _towerIndex;

        private TMP_Text _buttonText;
        private Button _button;

        public void Initialize(NodeBuilder builder, int index)
        {
            _nodeBuilder = builder;
            _towerIndex = index;
            
            _button = GetComponentInChildren<Button>();

            if (_button != null)
            {
                _buttonText = _button.GetComponentInChildren<TMP_Text>();

                if (_buttonText != null)
                {
                    SetButtonName(_nodeBuilder.GetTowerName(_towerIndex));
                }
                else
                {
                    Debug.LogError("Text component not found as a child of the button.");
                }

                _button.onClick.AddListener(OnClick);
            }
            else
            {
                Debug.LogError("Button component not found on the game object.");
            }
        }

        private void SetButtonName(string towerName)
        {
            if (_buttonText != null)
            {
                _buttonText.text = towerName;
            }
        }

        private void OnClick()
        {
            _nodeBuilder?.SelectTowerPrefab(_towerIndex);
            _nodeBuilder?.BuildTower();
        }
    }
}