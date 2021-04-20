using SpaceProject.Data.Objects.Spaceship;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship.Subsystems
{
    public class ManeuverSubsystem : SubsystemBase
    {
        private Rigidbody rigidbody;        

        private Dictionary<ManeuverCommand, ManeuverCommandData> commands = new Dictionary<ManeuverCommand, ManeuverCommandData>();
        private List<ManeuverCommand> completedCommands = new List<ManeuverCommand>();

        //TODO
        private bool isFlightAssist = true;


        public ManeuverSubsystem(SpaceshipController _spaceship) : base(_spaceship)
        {
            ExecuteType = EnumSubsystemExecuteType.FixedUpdate;

            rigidbody = _spaceship.Rigidbody;            
        }


        public override void Execute(float _deltaTime)
        {
            completedCommands.Clear();

            var vectorDelta = Vector3.zero;
            var rotationDelta = Vector3.zero;
            var controllability = data.controllability * _deltaTime;

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

            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotationDelta * _deltaTime));
            rigidbody.MovePosition(rigidbody.position + vectorDelta * _deltaTime);
        }

        public void AddCommand(ManeuverCommandData _data)
        {
            if (commands.ContainsKey(_data.command))
                commands[_data.command].SetTargetDirection(_data.direction);
            else
                commands.Add(_data.command, _data);
        }
    }
}
