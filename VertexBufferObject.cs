using System;
using OpenTK.Graphics.OpenGL;

namespace HelloWorld
{
    public class VertexBufferObject : IDisposable
    {
        private readonly int id;
        public int Length { get; }
        public VertexBufferObject(float[] vertices)
        {
            Length = vertices.Length;
            id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }


        private void ReleaseUnmanagedResources()
        {
            GL.DeleteBuffer(id);
        }


        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }


        ~VertexBufferObject()
        {
            ReleaseUnmanagedResources();
        }
    }

}

