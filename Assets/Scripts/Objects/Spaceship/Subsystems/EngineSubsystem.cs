using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship.Subsystems
{
    public class EngineSubsystem : SubsystemBase
    {
        private Rigidbody rigidbody;

        //TODO
        private bool isFlightAssist = true;


        public EngineSubsystem(SpaceshipController _spaceship) : base(_spaceship)
        {
            ExecuteType = EnumSubsystemExecuteType.FixedUpdate;

            rigidbody = _spaceship.Rigidbody;
        }

        public override void Execute()
        { 
        
        }
    }
}
