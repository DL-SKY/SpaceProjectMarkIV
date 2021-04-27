using SpaceProject.Objects.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.UI.Windows.Test
{
    public class SpaceShipControlPanel : MonoBehaviour
    {
        [SerializeField] private List<SpaceshipUIElementPrefab> uiPrefabs = new List<SpaceshipUIElementPrefab>();

        private PlayerSpaceship playerSpaceship;


        public void Initialize(PlayerSpaceship _playerSpaceship)
        {
            playerSpaceship = _playerSpaceship;

            CreateControls();
        }


        private void CreateControls()
        {
            foreach (var point in playerSpaceship.Spaceship.GetUI2DControlPositions())
            {
                var prefab = uiPrefabs.Find(x => x.uiElement == point.uiElement)?.prefab;
                if (prefab != null)
                {
                    var newControl = Instantiate(prefab, transform);
                    var controlRectTransform = newControl.GetComponent<RectTransform>();
                    controlRectTransform.anchorMin = point.anchorMin;
                    controlRectTransform.anchorMax = point.anchorMax;
                    controlRectTransform.anchoredPosition = point.anchoredPosition;
                }
            }
        }
    }
}
