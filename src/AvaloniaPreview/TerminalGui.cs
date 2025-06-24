namespace Main;
using Avalonia.Remote.Protocol.Input;
using Terminal.Gui;

//this is only used for tracking mouse events
public class TerminalGui(BsonProtocol _protocol)
{
    public void Start()
    {
        Application.Init();
        Application.RootMouseEvent += async (MouseEvent args) =>
        {
            if (_protocol.ImageSize is null) return;
            switch (args.Flags)
            {
                //movement
                case MouseFlags.ReportMousePosition:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseMove(args.X, args.Y);
                    break;
                //button presses
                case MouseFlags.Button1Pressed:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseDown(args.X, args.Y, MouseButton.Left);
                    break;
                case MouseFlags.Button2Pressed:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseDown(args.X, args.Y, MouseButton.Right);
                    break;
                case MouseFlags.Button3Pressed:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseDown(args.X, args.Y, MouseButton.Middle);
                    break;
                //button releases
                case MouseFlags.Button1Released:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseUp(args.X, args.Y, MouseButton.Left);
                    break;
                case MouseFlags.Button2Released:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseUp(args.X, args.Y, MouseButton.Right);
                    break;
                case MouseFlags.Button3Released:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseUp(args.X, args.Y, MouseButton.Middle);
                    break;
                //scroll
                case MouseFlags.WheeledDown:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseScroll(ScrollDirection.Up);
                    break;
                case MouseFlags.WheeledUp:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseScroll(ScrollDirection.Down);
                    break;
                case MouseFlags.WheeledLeft:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseScroll(ScrollDirection.Right);
                    break;
                case MouseFlags.WheeledRight:
                    if (InImageBounds(args.X, args.Y)) await _protocol.MouseScroll(ScrollDirection.Left);
                    break;
            }
        };

        Application.Top.KeyPress += (View.KeyEventEventArgs args) =>
       {
           if (args.KeyEvent.Key == Terminal.Gui.Key.a)
           {
               Application.Shutdown();
           }
       };
        Application.Run();
    }
    //This method checks if a mouse event happened within the area occupied by our image
    private bool InImageBounds(int X, int Y)
    {
        if (_protocol.ImageSize is null) return false;
        return _protocol.ImageSize.height >= Y && _protocol.ImageSize.width >= X;
    }
}
public enum ScrollDirection {
    Left,
    Right,
    Up,
    Down,
}
