using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

public class BasicGameWindow : GameWindow
{
    private float trianglePositionX = 0.0f; 
    private float triangleRotation = 0.0f; 
    private bool isMousePressed = false; 

    public BasicGameWindow(int width, int height, string title)
        : base(width, height, OpenTK.Graphics.GraphicsMode.Default, title)
    {
 
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Width, Height);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        float fov = MathHelper.DegreesToRadians(45.0f); 
        float aspectRatio = Width / (float)Height;
        float near = 0.1f;
        float far = 100.0f;
        Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(fov, aspectRatio, near, far);
        GL.LoadMatrix(ref perspective);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();
        GL.Translate(0.0f, 0.0f, -3.0f);  
        GL.Translate(trianglePositionX, 0.0f, 0.0f);
        GL.Rotate(triangleRotation, 0.0f, 0.0f, 1.0f);
        GL.Begin(PrimitiveType.Triangles);
        GL.Color3(1.0f, 0.0f, 0.0f);
        GL.Vertex3(-0.5f, -0.5f, 0.0f);
        GL.Color3(0.0f, 1.0f, 0.0f);
        GL.Vertex3(0.5f, -0.5f, 0.0f);
        GL.Color3(0.0f, 0.0f, 1.0f);
        GL.Vertex3(0.0f, 0.5f, 0.0f);
        GL.End();

        SwapBuffers();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        if (e.Key == Key.Left)
        {
            trianglePositionX -= 0.1f;
        }
        else if (e.Key == Key.Right)
        {
            trianglePositionX += 0.1f;
        }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        if (isMousePressed)
        {
            triangleRotation = e.X * 0.1f;  
            Console.WriteLine($"Mouse moved: {e.X}, {e.Y}");  
        }
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        if (e.Button == MouseButton.Left)
        {
            isMousePressed = true;  
            Console.WriteLine("Mouse button down.");  
        }
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        if (e.Button == MouseButton.Left)
        {
            isMousePressed = false; 
            Console.WriteLine("Mouse button up.");  
        }
    }

    public static void Main(string[] args)
    {
        using (var game = new BasicGameWindow(800, 600, "OpenTK Demo"))
        {
            game.Run(60.0);
        }
    }
}
