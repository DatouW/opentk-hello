using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace HelloWorld
{
    class Cube : GameWindow
    {
        float angle;

        public Cube(int width = 1280, int height = 768, string title = "Televisor 3D")
             : base(
                   GameWindowSettings.Default,
                   new NativeWindowSettings()
                   {
                       Title = title,
                       Size = new Vector2i(width, height),

                   })
        {
            this.CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color.CornflowerBlue);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            angle += 1.0f * (float)e.Time;
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0.0f, 0.0f, -5.0f);
            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            GL.Begin(PrimitiveType.Quads);

            // Front face
            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            // Back face
            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);

            // Top face
            GL.Color3(Color.Green);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);

            // Bottom face
            GL.Color3(Color.Yellow);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            // Right face
            GL.Color3(Color.Magenta);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            // Left face
            GL.Color3(Color.Cyan);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.End();

            SwapBuffers();
        }

        static void Main(string[] args)
        {
            using (Cube program = new Cube())
            {
                program.Run();
            }
        }
    }
}
