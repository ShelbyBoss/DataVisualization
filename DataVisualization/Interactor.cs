using System.Windows;
using System.Windows.Controls;

namespace DataVisualization
{
    public enum InteractionType { Button, Text }

    public static class Interactor
    {
        public static readonly Thickness DefaultMargin = new Thickness(5, 5, 0, 5);

        public static IInputElement Get(string text, InteractionType type = InteractionType.Button)
        {
            return Get(text, DefaultMargin, type);
        }

        public static IInputElement Get(string text, Thickness margin, InteractionType type = InteractionType.Button)
        {
            switch (type)
            {
                case InteractionType.Button:
                    return new Button() { Content = text, Margin = margin };

                case InteractionType.Text:
                    return new TextBox() { Text = text, Margin = margin };
            }

            return null;
        }
    }
}
