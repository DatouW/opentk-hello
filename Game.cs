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
        private Matrix4 model;
        private Matrix4 view;
        private Matrix4 perspective;
        private float width;
        private float height;

        float[] vertices = {
                -0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f,  0.5f, -0.5f,
                 0.5f,  0.5f, -0.5f,
                -0.5f,  0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,

                -0.5f, -0.5f,  0.5f,
                 0.5f, -0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,
                -0.5f, -0.5f,  0.5f,

                -0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,

                 0.5f,  0.5f,  0.5f,
                 0.5f,  0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,

                -0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f,  0.5f,
                 0.5f, -0.5f,  0.5f,
                -0.5f, -0.5f,  0.5f,
                -0.5f, -0.5f, -0.5f,

                -0.5f,  0.5f, -0.5f,
                 0.5f,  0.5f, -0.5f,
                 0.5f,  0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f, -0.5f
            };

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
            this.width = width;
            this.height = height;
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.3f, 0.4f, 0.5f, 1f);


            /*float[] vertices = new float[]
            {
                -0.5f, -0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                 0.0f,  0.5f, 0.0f,

                 -1.0f, 0.2f, 0.0f,
                 -0.7f, 0.2f, 0.0f,
                 -0.85f,  0.5f, 0.0f,
            };*/

            

            // Crear y configurar el vao
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);


            // Crear y cargar el búfer de vértices
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Compilar shaders
            string vertexShaderCode =
                @"#version 330 core
                layout(location = 0) in vec3 aPosition;

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 perspective;

                void main()
                {
                    gl_Position = vec4(aPosition, 1.0) * model * view * perspective;
                }";

            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float, false, 3*sizeof(float),0);
            GL.EnableVertexAttribArray(0);

            int vertexShaderObject = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderObject, vertexShaderCode);
            GL.CompileShader(vertexShaderObject);


            string fragmentShaderCode =
                @"#version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0, 1.0, 0.0, 1.0);
                }";

           
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
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(vertexArrayObject);

            model = Matrix4.Identity * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30));
            view = Matrix4.LookAt(new Vector3(0, 0, -3), Vector3.Zero, Vector3.UnitY);
            perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), width / height, 0.1f, 1000f);


            // 
            int modelUniformLocation = GL.GetUniformLocation(shaderProgramObject, "model");
            int viewUniformLocation = GL.GetUniformLocation(shaderProgramObject, "view");
            int perspectiveUniformLocation = GL.GetUniformLocation(shaderProgramObject, "perspective");



            GL.UniformMatrix4(modelUniformLocation, true, ref model);
            GL.UniformMatrix4(viewUniformLocation, true, ref view);
            GL.UniformMatrix4(perspectiveUniformLocation, true, ref perspective);

            // Usar el programa de shaders
            GL.UseProgram(shaderProgramObject);

            


            // Dibujar el triángulo
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            //GL.DrawArrays(PrimitiveType.Triangles, 3, 3);

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            height = e.Height;
            width = e.Width;
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
