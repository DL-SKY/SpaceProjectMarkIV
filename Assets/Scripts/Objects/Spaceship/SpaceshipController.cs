using SpaceProject.Data.Objects.Spaceship;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] private SpaceshipData data;
        [SerializeField] private bool isFlightAssist = true;

        private new Rigidbody rigidbody;
        private Dictionary<ManeuverCommand, ManeuverCommandData> commands = new Dictionary<ManeuverCommand, ManeuverCommandData>();
        private List<ManeuverCommand> completedCommands = new List<ManeuverCommand>();


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            completedCommands.Clear();

            var vectorDelta = Vector3.zero;
            var rotationDelta = Vector3.zero;
            var controllability = data.controllability * Time.fixedDeltaTime;

            foreach (var command in commands)
            {
                switch (command.Value.type)
                {
                    case ManeuverType.Move:
                        vectorDelta += command.Value.CalculateDirection(controllability);
                        break;
                    case ManeuverType.Rotate:
                        rotationDelta += command.Value.CalculateDirection(controllability);
                        break;
                }

                if (isFlightAssist)
                {
                    command.Value.SetTargetDirection(Vector3.zero);
                    if (command.Value.CheckComplete())
                        completedCommands.Add(command.Key);
                }                
            }

            foreach (var completed in completedCommands)
                commands.Remove(completed);

            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotationDelta * Time.fixedDeltaTime));
            rigidbody.MovePosition(rigidbody.position + vectorDelta * Time.fixedDeltaTime);            
        }


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


        private void AddCommand(ManeuverCommandData _data)
        {
            if (commands.ContainsKey(_data.command))
                commands[_data.command].SetTargetDirection(_data.direction);
            else
                commands.Add(_data.command, _data);
        }
    }
}
