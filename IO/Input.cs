
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StrategyGame
{
    public class Input
    {
 
        public static MouseState CurrentMouseState { get; private set; }
        public static KeyboardState CurrentKeyboardState { get; private set; }
        public static Point CurrentMousePosition { get; private set; }

        public static MouseState PreviousMouseState { get; private set; }
        public static KeyboardState PreviousKeyboardState { get; private set; }
        public static Point PreviousMousePosition { get; private set; }

        public static ButtonState Button { get; }

        public static void Update()
        {
            PreviousMouseState = CurrentMouseState;
            PreviousKeyboardState = CurrentKeyboardState;
            PreviousMousePosition = CurrentMousePosition;
            CurrentMouseState = Mouse.GetState();
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMousePosition = new Point(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public static bool ButtonIsPressed(ButtonState button)
        {
            return button == ButtonState.Pressed;
        }

        public static bool LeftButtonClicked()
        {
            return ButtonIsPressed(CurrentMouseState.LeftButton) && !ButtonIsPressed(PreviousMouseState.LeftButton);
        }

        public static bool RightButtonClicked()
        {
            return ButtonIsPressed(CurrentMouseState.RightButton) && !ButtonIsPressed(PreviousMouseState.RightButton);
        }

        public static bool KeyIsPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && !PreviousKeyboardState.IsKeyDown(key);
        }

    }
}
