using SpaceProject.Constants;
using SpaceProject.Data.Objects.Spaceship;
using SpaceProject.Events;
using SpaceProject.Objects.Spaceship;
using SpaceProject.Services;
using System;
using UnityEngine;

namespace SpaceProject.InputSystem.Adapters
{
    public class SpaceshipControlMobileAdapter : InputAdapter
    {
        private PlayerSpaceship playerSpaceship;


        public SpaceshipControlMobileAdapter(bool _enabled) : base(_enabled)
        {
            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);

            playerSpaceship = ComponentLocator.Resolve<PlayerSpaceship>();
            if (playerSpaceship == null)               
                SetEnable(false);
        }

        ~SpaceshipControlMobileAdapter()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);
        }


        public void SendCommand(ManeuverCommand _maneuverCommand, float _speedMod = 1.0f)
        {
            switch (_maneuverCommand)
            {
                case ManeuverCommand.OnPitch:
                    playerSpaceship.Spaceship.OnPitch(_speedMod > 0.0f ? PitchMode.ToUp : PitchMode.ToDown, Mathf.Abs(_speedMod));
                    break;

                case ManeuverCommand.OnRoll:
                    playerSpaceship.Spaceship.OnRoll(_speedMod > 0.0f ? RollMode.ToRight : RollMode.ToLeft, Mathf.Abs(_speedMod));
                    break;

                case ManeuverCommand.OnYaw:
                    playerSpaceship.Spaceship.OnYaw(_speedMod > 0.0f ? YawMode.ToRight : YawMode.ToLeft, Mathf.Abs(_speedMod));
                    break;

                case ManeuverCommand.OnStrafeY:
                    playerSpaceship.Spaceship.OnStrafe(_speedMod > 0.0f ? StrafeMode.ToUp : StrafeMode.ToDown, Mathf.Abs(_speedMod));
                    break;

                case ManeuverCommand.OnStrafeX:
                    playerSpaceship.Spaceship.OnStrafe(_speedMod > 0.0f ? StrafeMode.ToRight : StrafeMode.ToLeft, Mathf.Abs(_speedMod));
                    break;

                case ManeuverCommand.OnEngineSpeedChange:
                    playerSpaceship.Spaceship.OnEngineSpeedChange(_speedMod);
                    break;
            }
        }


        protected override void CustomUpdate() { }


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
