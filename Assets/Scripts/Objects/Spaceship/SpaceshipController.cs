using SpaceProject.Data.Objects.Spaceship;
using SpaceProject.Enums;
using SpaceProject.Objects.Spaceship.Subsystems;
using SpaceProject.ScriptableObjects.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship
{
    [Serializable]
    public class SpaceshipUIElementPosition
    {
        public EnumSpaceshipUIElement uiElement;
        public Vector3 position;
        public Vector3 rotation;
    }

    [Serializable]
    public class SpaceshipUIControlPosition
    {
        public EnumSpaceshipUIElement uiElement;
        public Vector2 anchorMin;
        public Vector2 anchorMax;
        public Vector2 anchoredPosition;
    }


    [RequireComponent(typeof(Rigidbody))]
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] private SpaceshipConfig config;
        [SerializeField] private bool isFlightAssist = true;

        public Rigidbody Rigidbody { get; private set; }
        
        private Dictionary<EnumSubsystems, SubsystemBase> subsystems = new Dictionary<EnumSubsystems, SubsystemBase>();

        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            CreateSubsystems();
        }

        private void FixedUpdate()
        {
            foreach (var subsystem in subsystems)
                if (subsystem.Value.ExecuteType == EnumSubsystemExecuteType.FixedUpdate)
                    subsystem.Value.Execute(Time.fixedDeltaTime);
        }


        public SpaceshipData GetData()
        {
            return config.data;
        }

        public List<SpaceshipUIElementPosition> GetUI3DElementPositions()
        {
            return config.uiElement3DPositions;
        }

        public List<SpaceshipUIControlPosition> GetUI2DControlPositions()
        {
            return config.uiControl2DPosition;
        }

        #region ManeuverSubsystem
        //тангаж (pitch)
        public void OnPitch(PitchMode _mode, float _speedMod = 1.0f)
        {
            var direction = Vector3.zero;
            switch (_mode)
            {
                case PitchMode.ToDown:
                    direction = Vector3.right;
                    break;
                case PitchMode.ToUp:
                    direction = -Vector3.right;
                    break;
            }

            AddCommand(new ManeuverCommandData(ManeuverCommand.OnPitch, ManeuverType.Rotate, direction * config.data.pitchSpeed * _speedMod));
        }

        //крен (roll)
        public void OnRoll(RollMode _mode, float _speedMod = 1.0f)
        {
            var direction = Vector3.zero;
            switch (_mode)
            {
                case RollMode.ToLeft:
                    direction = Vector3.forward;
                    break;
                case RollMode.ToRight:
                    direction = -Vector3.forward;
                    break;
            }

            AddCommand(new ManeuverCommandData(ManeuverCommand.OnRoll, ManeuverType.Rotate, direction * config.data.rollSpeed * _speedMod));
        }

        //рыскание (yaw)
        public void OnYaw(YawMode _mode, float _speedMod = 1.0f)
        {
            var direction = Vector3.zero;
            switch (_mode)
            {
                case YawMode.ToLeft:
                    direction = -Vector3.up;
                    break;
                case YawMode.ToRight:
                    direction = Vector3.up;
                    break;
            }

            AddCommand(new ManeuverCommandData(ManeuverCommand.OnYaw, ManeuverType.Rotate, direction * config.data.yawSpeed * _speedMod));
        }

        //вверх/вниз
        //влево/вправо
        public void OnStrafe(StrafeMode _mode, float _speedMod = 1.0f)
        {
            var commandType = ManeuverCommand.OnStrafeX;
            var direction = Vector3.zero;
            switch (_mode)
            {
                case StrafeMode.ToUp:
                    commandType = ManeuverCommand.OnStrafeY;
                    direction = transform.up;
                    break;

                case StrafeMode.ToDown:
                    commandType = ManeuverCommand.OnStrafeY;
                    direction = -transform.up;
                    break;

                case StrafeMode.ToLeft:
                    direction = -transform.right;
                    break;

                case StrafeMode.ToRight:
                    direction = transform.right;
                    break;
            }

            AddCommand(new ManeuverCommandData(commandType, ManeuverType.Move, direction * config.data.strafeSpeed * _speedMod));
        }
        #endregion

        #region EngineSubsystem
        public void OnEngineSpeedChange(float _speedMod = 1.0f)
        {
            AddCommand(_speedMod);
        }
        #endregion


        private void CreateSubsystems()
        {
            var newSubsystems = new EnumSubsystems[] 
                { 
                    EnumSubsystems.Maneuver, 
                    EnumSubsystems.Engine,
                };

            subsystems.Clear();

            foreach (var subsystem in newSubsystems)
                subsystems.Add(subsystem, SubsystemCreator.Create(subsystem, this));
        }

        private void AddCommand(ManeuverCommandData _data)
        {
            (subsystems[EnumSubsystems.Maneuver] as ManeuverSubsystem).AddCommand(_data);
        }

        private void AddCommand(float _deltaMod)
        {
            (subsystems[EnumSubsystems.Engine] as EngineSubsystem).ChangeSpeedMod(_deltaMod);
        }
    }
}
