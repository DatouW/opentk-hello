using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;

namespace HelloWorld
{
    public class Game : GameWindow
    {
        private int vertexBufferObject;
        private int shaderProgramObject;
        private int vertexArrayObject;

        public Game(int width = 1280, int height = 768, string title = "Televisor 3D")
            : base(
                  GameWindowSettings.Default,
                  new NativeWindowSettings()
                  {
                      Title = title,
                      ClientSize = new Vector2i(width, height),
                     
                  })
        {
            this.CenterWindow();
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.3f, 0.4f, 0.5f, 1f);

            float[] vertices = new float[]
            {
                -0.5f, -0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                 0.0f,  0.5f, 0.0f
            };

            // Crear y cargar el búfer de vértices
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Crear y configurar el búfer de vértices
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Compilar shaders
            string vertexShaderCode =
                @"#version 330 core
                layout(location = 0) in vec3 aPosition;
                void main()
                {
                    gl_Position = vec4(aPosition, 1.0);
                }";

            string fragmentShaderCode =
                @"#version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0, 1.0, 0.0, 1.0);
                }";

            int vertexShaderObject = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderObject, vertexShaderCode);
            GL.CompileShader(vertexShaderObject);

            int fragmentShaderObject = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderObject, fragmentShaderCode);
            GL.CompileShader(fragmentShaderObject);

            // Enlazar los shaders al programa
            shaderProgramObject = GL.CreateProgram();
            GL.AttachShader(shaderProgramObject, vertexShaderObject);
            GL.AttachShader(shaderProgramObject, fragmentShaderObject);
            GL.LinkProgram(shaderProgramObject);

            // Limpiar y eliminar shaders
            GL.DetachShader(shaderProgramObject, vertexShaderObject);
            GL.DetachShader(shaderProgramObject, fragmentShaderObject);
            GL.DeleteShader(vertexShaderObject);
            GL.DeleteShader(fragmentShaderObject);

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Usar el programa de shaders
            GL.UseProgram(shaderProgramObject);
            GL.BindVertexArray(vertexArrayObject);

            // Dibujar el triángulo
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnUnload()
        {
            // Limpiar los búferes y el programa de shaders
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vertexBufferObject);
            GL.UseProgram(0);
            GL.DeleteProgram(shaderProgramObject);

            base.OnUnload();
        }
    }
}
