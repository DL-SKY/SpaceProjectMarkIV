namespace SpaceProject.Objects.Spaceship.Subsystems
{
    public static class SubsystemCreator
    {
        public static SubsystemBase Create(EnumSubsystems _subsystem, SpaceshipController _spaceship)
        {
            SubsystemBase result = null;

            switch (_subsystem)
            {
                case EnumSubsystems.Maneuver:
                    result = new ManeuverSubsystem(_spaceship);
                    break;

                case EnumSubsystems.Engine:
                    result = new EngineSubsystem(_spaceship);
                    break;
            }

            return result;
        }
    }
}
