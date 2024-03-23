using System;
using OpenTK.Graphics.OpenGL;

namespace HelloWorld
{
    public class IndexBufferObject : IDisposable
    {
        private readonly int id;
        public int Length { get; }
        public IndexBufferObject(uint[] indices)
        {
            Length = indices.Length;
            id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
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


        ~IndexBufferObject()
        {
            ReleaseUnmanagedResources();
        }
    }

}
