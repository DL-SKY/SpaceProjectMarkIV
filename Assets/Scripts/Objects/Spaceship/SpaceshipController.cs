using SpaceProject.Data.Objects.Spaceship;
using SpaceProject.Objects.Spaceship.Subsystems;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] private SpaceshipData data;
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
                    subsystem.Value.Execute();
        }


        public SpaceshipData GetData()
        {
            return data;
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

            AddCommand(new ManeuverCommandData(ManeuverCommand.OnPitch, ManeuverType.Rotate, direction * data.pitchSpeed * _speedMod));
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

            AddCommand(new ManeuverCommandData(ManeuverCommand.OnRoll, ManeuverType.Rotate, direction * data.rollSpeed * _speedMod));
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

            AddCommand(new ManeuverCommandData(ManeuverCommand.OnYaw, ManeuverType.Rotate, direction * data.yawSpeed * _speedMod));
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

            AddCommand(new ManeuverCommandData(commandType, ManeuverType.Move, direction * data.strafeSpeed * _speedMod));
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
    }
}
