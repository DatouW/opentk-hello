using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;



namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }

    }
}









