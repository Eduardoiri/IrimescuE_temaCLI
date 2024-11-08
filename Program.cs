using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.IO;

namespace tema3
{
    class SimpleWindow : GameWindow
    {
        private float xPosition = 0.0f;
        private float yPosition = 0.0f;
        private float moveSpeed = 0.05f;
        private float rotationAngle = 0.0f;
        private Color triangleColor = Color.Black;  // Initial color of the triangle
        private float transparency = 1.0f;

        private float[] triangleVertices;

        // Constructor
        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            MouseMove += Mouse_MouseMove;
        }

        // Handles key press events for controlling object horizontally and color changes
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Exit();
            }

            if (e.Key == Key.F11)
            {
                this.WindowState = this.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
            }


            // Change color with keys
            if (e.Key == Key.R) // Red component up
            {
                triangleColor = ChangeColor(triangleColor, 'R', 0.1f);
                Console.WriteLine("Red component increased.");
            }
            if (e.Key == Key.G) // Green component up
            {
                triangleColor = ChangeColor(triangleColor, 'G', 0.1f);
                Console.WriteLine("Green component increased.");
            }
            if (e.Key == Key.B) // Blue component up
            {
                triangleColor = ChangeColor(triangleColor, 'B', 0.1f);
                Console.WriteLine("Blue component increased.");
            }
        }

        // Helper function to change color components
        private Color ChangeColor(Color color, char component, float change)
        {
            float r = color.R / 255.0f;
            float g = color.G / 255.0f;
            float b = color.B / 255.0f;

            switch (component)
            {
                case 'R': r = Math.Min(1.0f, r + change); break;
                case 'G': g = Math.Min(1.0f, g + change); break;
                case 'B': b = Math.Min(1.0f, b + change); break;
            }

            return Color.FromArgb((int)(transparency * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        // Handles mouse movement events for rotating the camera
        void Mouse_MouseMove(object sender, MouseMoveEventArgs e)
        {
            // Map the mouse X position to the camera rotation angle
            rotationAngle = 360.0f * e.X / Width;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
            LoadTriangleFromFile("triunghi.txt");  // Load triangle coordinates from file
            ShowInstructions();  // Show instructions in the console
        }


        private void ShowInstructions()
        {
            Console.WriteLine("Welcome to the OpenTK Triangle Renderer!");
            Console.WriteLine("Use the following keys to control the triangle:");
            Console.WriteLine("  - 'R' to increase the red component of the triangle color");
            Console.WriteLine("  - 'G' to increase the green component of the triangle color");
            Console.WriteLine("  - 'B' to increase the blue component of the triangle color");
            Console.WriteLine("  - 'ESC' to exit the program");
            Console.WriteLine("Press 'F11' to toggle fullscreen mode.");
        }


        private void LoadTriangleFromFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {

                    string[] lines = File.ReadAllLines(filename);
                    string[] parts = lines[0].Split(',');

                    if (parts.Length == 6)
                    {
                        triangleVertices = new float[6];
                        for (int i = 0; i < 6; i++)
                        {
                            triangleVertices[i] = float.Parse(parts[i].Trim());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of coordinates in the file. Using default values.");
                        SetDefaultTriangleVertices();
                    }
                }
                else
                {
                    Console.WriteLine("File not found. Using default values.");
                    SetDefaultTriangleVertices();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading triangle data: {ex.Message}. Using default values.");
                SetDefaultTriangleVertices();
            }
        }


        private void SetDefaultTriangleVertices()
        {
            triangleVertices = new float[]
            {
                -0.5f,  0.5f,
                 0.5f,  0.5f,
                 0.0f, -0.5f
            };
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.PushMatrix();
            GL.Rotate(rotationAngle, 0.0f, 0.0f, 1.0f);  // Rotate the scene based on mouse X movement

            // Render the triangle with color and transparency controlled by user input
            GL.Begin(PrimitiveType.Triangles);

            // Apply the current color with transparency
            GL.Color4(triangleColor);

            // Apply the vertices loaded from the file
            GL.Vertex2(triangleVertices[0], triangleVertices[1]);
            GL.Vertex2(triangleVertices[2], triangleVertices[3]);
            GL.Vertex2(triangleVertices[4], triangleVertices[5]);

            GL.End();

            GL.PopMatrix();

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
