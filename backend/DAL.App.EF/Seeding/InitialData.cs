namespace DAL.App.EF.Seeding
{
    public class InitialData
    {
        public static readonly string[] Roles =
        {
            "Admin"
        };

        public static readonly (string email, string name, string password, string[] roles)[] Users =
        {
            ("siimliinat@gmail.com", "siim@liinat.com", "Admin1234_", new []{"admin"}),
            ("s@s.s", "NotSiim", "User1234_", System.Array.Empty<string>())
        };
        
        public static readonly string[] Companies =
        {
            "Microsoft", "Sony", "Nintendo", "Epic Games"
        };
        
        public static readonly (string type, string description, int rating)[] CompatibilityTypes =
        {
            ("Nothing", "Programs that don't initialize properly, not loading at all and/or crashing the emulator.", 0),
            ("Loadable", "Programs that display a black screen with a framerate on the window's title.", 1),
            ("Intro", "Programs that display image but don't make it past the menus.", 2),
            ("Ingame", "Programs that either can't be finished, have serious glitches or have insufficient performance.", 3),
            ("Playable", "Programs that can be completed with playable performance and no game breaking glitches.", 4)
        };
        
        public static readonly string[] Genres =
        {
            "Adventure", "Action", "RPG", "Simulation", "Sports"
        };
        
        public static readonly (string company, string platformType, string name, string code)[] Platforms =
        {
            ("Sony", "Game Console", "Playstation 2", "PS2"),
            ("Microsoft", "Game Console", "Xbox 360", "X360")
        };
        
        public static readonly string[] PlatformTypes =
        {
            "Game Console", "Handheld", "Operating System"
        };
    }
}