using System;

namespace FlappyBird.Core
{
    /* To change the icon:
     * Convert .ico file to a .bmp file
     * Add both to root dir
     * Set the .ico file in the project property menu
     * Add them in the .csproj file
     */
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
