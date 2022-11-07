namespace ModernGUI
{
    public interface IControl
    {
        int Depth { get; set; }
        SkinManager SkinManager { get; }
        MouseState MouseState { get; set; }

    }

    public enum MouseState
    {
        HOVER,
        DOWN,
        OUT
    }
}
