using SpaceProject.Data.Objects.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship.Subsystems
{
    public enum EnumSubsystems
    { 
        Maneuver,
    }

    public enum EnumSubsystemExecuteType
    { 
        NA,

        FixedUpdate,
    }


    abstract public class SubsystemBase
    {
        public EnumSubsystemExecuteType ExecuteType { get; protected set; }

        protected SpaceshipData data;
        

        public SubsystemBase(SpaceshipController _spaceship) 
        {
            data = _spaceship.GetData();
        }

        abstract public void Execute();
    }
}
