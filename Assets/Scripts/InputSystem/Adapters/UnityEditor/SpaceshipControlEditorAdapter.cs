using SpaceProject.Constants;
using SpaceProject.Events;
using SpaceProject.Objects.Spaceship;
using SpaceProject.Services;
using UnityEngine;

namespace SpaceProject.InputSystem.Adapters
{
    public class SpaceshipControlEditorAdapter : InputAdapter
    {
        private PlayerSpaceship playerSpaceship;


        public SpaceshipControlEditorAdapter(bool _enabled) : base(_enabled)
        {
            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);

            playerSpaceship = ComponentLocator.Resolve<PlayerSpaceship>();
            if (playerSpaceship == null)
                SetEnable(false);
        }

        ~SpaceshipControlEditorAdapter()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);
        }


        protected override void CustomUpdate()
        {
            //TODO

            //тангаж (pitch)
            //крен (roll)

            //рыскание (yaw)

            //вверх/вниз
            if (Input.GetKey(KeyCode.UpArrow))
                playerSpaceship.transform.Translate(Vector3.up * Time.deltaTime);
            if (Input.GetKey(KeyCode.DownArrow))
                playerSpaceship.transform.Translate(- Vector3.up * Time.deltaTime);
            //влево/вправо
        }

        private void OnPlayerSpaceshipCreateHandler(CustomEvent _event)
        {
            playerSpaceship = (PlayerSpaceship)_event.EventData;            
            SetEnable(true);
        }

        private void OnPlayerSpaceshipDestroyHandler(CustomEvent _event)
        {
            playerSpaceship = null;
            SetEnable(false);
        }
    }
}
