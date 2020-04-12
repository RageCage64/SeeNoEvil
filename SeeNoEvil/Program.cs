using System;

namespace SeeNoEvil
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SeeNoEvilGame())
                game.Run();
        }
    }
}
