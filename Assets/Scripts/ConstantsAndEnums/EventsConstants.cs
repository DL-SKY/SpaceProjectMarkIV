using System;

namespace SpaceProject.Constants
{
    public static class ConstantEventsName
    {
        [Obsolete]
        public static string ON_GAMEPLAY_CAMERA_ENABLE = "ON_GAMEPLAY_CAMERA_ENABLE";               //bool

        //------------------------------------------------------------------------------------------------

        public static string ON_SCENE_LOADED = "ON_SCENE_LOADED";                                   //string sceneName
        public static string ON_RESOLUTION_CHANGE = "ON_RESOLUTION_CHANGE";                         //void
        public static string ON_PLAYER_SPACESHIP_CREATE = "ON_PLAYER_SPACESHIP_CREATE";             //PlayerSpaceship
        public static string ON_PLAYER_SPACESHIP_DESTROY = "ON_PLAYER_SPACESHIP_DESTROY";           //void
    }
}
