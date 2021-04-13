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
            //тангаж (pitch)
            if (Input.GetKey(KeyCode.W))
                playerSpaceship.Spaceship.OnPitch(PitchMode.ToUp);
            if (Input.GetKey(KeyCode.S))
                playerSpaceship.Spaceship.OnPitch(PitchMode.ToDown);

            //крен (roll)
            if (Input.GetKey(KeyCode.A))
                playerSpaceship.Spaceship.OnRoll(RollMode.ToLeft);
            if (Input.GetKey(KeyCode.D))
                playerSpaceship.Spaceship.OnRoll(RollMode.ToRight);

            //рыскание (yaw)
            if (Input.GetKey(KeyCode.Q))
                playerSpaceship.Spaceship.OnYaw(YawMode.ToLeft);
            if (Input.GetKey(KeyCode.E))
                playerSpaceship.Spaceship.OnYaw(YawMode.ToRight);

            //вверх/вниз
            if (Input.GetKey(KeyCode.UpArrow))
                playerSpaceship.Spaceship.OnStrafe(StrafeMode.ToUp);
            if (Input.GetKey(KeyCode.DownArrow))
                playerSpaceship.Spaceship.OnStrafe(StrafeMode.ToDown);

            //влево/вправо
            if (Input.GetKey(KeyCode.LeftArrow))
                playerSpaceship.Spaceship.OnStrafe(StrafeMode.ToLeft);
            if (Input.GetKey(KeyCode.RightArrow))
                playerSpaceship.Spaceship.OnStrafe(StrafeMode.ToRight);
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
