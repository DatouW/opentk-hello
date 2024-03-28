using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;

namespace HelloWorld
{
    public class Cube : GameWindow
    {

        private int shaderProgramObject;
        private int vertexArrayObject;
        private IndexBufferObject ibo;
        private VertexBufferObject vbo;
        private Matrix4 model;
        private Matrix4 view;
        private Matrix4 perspective;
        private float width;
        private float height;


        /*float[] vertices = new float[]{
                 0.5f,  0.5f, 0.0f,  // top right
                 0.5f, -0.5f, 0.0f,  // bottom right
                -0.5f, -0.5f, 0.0f,  // bottom left
                -0.5f,  0.5f, 0.0f   // top left 
            };
        uint[] indices = {  // note that we start from 0!
                0, 1, 3,   // first triangle
                1, 2, 3    // second triangle
            };*/
        float[] vertices = new float[]
        {
            -0.8f, -0.5f, -0.1f,
             0.8f, -0.5f, -0.1f,
             0.8f,  0.4f, -0.1f,
            -0.8f,  0.4f, -0.1f,

            -0.8f, -0.5f,  0.1f,
             0.8f, -0.5f,  0.1f,
             0.8f,  0.4f,  0.1f,
            -0.8f,  0.4f,  0.1f,

            //2.
            -0.8f, -0.6f, -0.1f,
            -0.1f, -0.6f, -0.1f,
            -0.1f, -0.5f, -0.1f,
            -0.8f, -0.5f, -0.1f,

            -0.8f, -0.6f,  0.1f,
            -0.1f, -0.6f,  0.1f,
            -0.1f, -0.5f,  0.1f,
            -0.8f, -0.5f,  0.1f,

            //4.
             -0.1f, -0.9f, -0.1f,
             0.1f, -0.9f, -0.1f,
             0.1f, -0.5f, -0.1f,
             -0.1f, -0.5f, -0.1f,

             -0.1f, -0.9f,  0.1f,
             0.1f, -0.9f,  0.1f,
             0.1f, -0.5f,  0.1f,
             -0.1f, -0.5f,  0.1f,

            //3.
             0.1f, -0.6f, -0.1f,
             0.8f, -0.6f, -0.1f,
             0.8f, -0.5f, -0.1f,
             0.1f, -0.5f, -0.1f,

             0.1f, -0.6f,  0.1f,
             0.8f, -0.6f,  0.1f,
             0.8f, -0.5f,  0.1f,
             0.1f, -0.5f,  0.1f,

             

        };
        uint[] indices =
        {
            //front
                0, 7, 3,
                0, 4, 7,
                //back
                1, 2, 6,
                6, 5, 1,
                //left
                0, 2, 1,
                0, 3, 2,
                //right
                4, 5, 6,
                6, 7, 4,
                //top
                2, 3, 6,
                6, 3, 7,
                //bottom
                0, 1, 5,
                0, 5, 4,


                //front
                8, 15, 11,
                8, 12, 15,
            //back
                9, 10, 14,
                14, 13, 9,
            //left
                8, 10, 9,
                8, 11, 10,
            //right
                12, 13, 14,
                14, 15, 12,
            //top
                10, 11, 14,
                14, 11, 15,
            //bottom
                8, 9, 13,
                8, 13, 12,


                //front
                16, 23, 19,
                16, 20, 23,
            //back
                17, 18, 22,
                22, 21, 17,
            //left
                16, 18, 17,
                16, 19, 18,
            //right
                20, 21, 22,
                22, 23, 20,
            //top
                18, 19, 22,
                22, 19, 23,
            //bottom
                16, 17, 21,
                16, 21, 20,

                //front
                24, 31, 27,
                24, 28, 31,
            //back
                25, 26, 30,
                30, 29, 25,
            //left
                24, 26, 25,
                24, 27, 26,
            //right
                28, 29, 30,
                30, 31, 28,
            //top
                26, 27, 30,
                30, 27, 31,
            //bottom
                24, 25, 29,
                24, 29, 28,

        };

        public Cube(int width = 1280, int height = 768, string title = "Televisor 3D")
            : base(
                  GameWindowSettings.Default,
                  new NativeWindowSettings()
                  {
                      Title = title,
                      ClientSize = new Vector2i(width, height),
                  })
        {
            this.CenterWindow();
            this.height = height;
            this.width = width;
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.3f, 0.4f, 0.5f, 1f);



            // Crear y configurar el vao
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);


            // Crear y cargar el búfer de vértices
            vbo = new VertexBufferObject(vertices);
            vbo.Bind();

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

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
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

            ibo = new IndexBufferObject(indices);
            ibo.Bind();

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(vertexArrayObject);

            model = Matrix4.Identity;
            //* Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90));
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
            GL.DrawElements(PrimitiveType.Triangles, ibo.Length, DrawElementsType.UnsignedInt, 0);
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
            GL.UseProgram(0);
            GL.DeleteProgram(shaderProgramObject);
        }
    }
}




