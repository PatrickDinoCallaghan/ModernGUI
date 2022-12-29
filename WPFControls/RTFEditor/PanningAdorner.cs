using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

namespace ModernGUI.WPF.Controls.RTFEditor
{
    public class PanningAdorner : Adorner
    {
        private Thumb gripper;
        public PanningAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            gripper = new Thumb();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Border));
            factory.SetValue(Border.BackgroundProperty, Brushes.Transparent);
            gripper.Template = new ControlTemplate() { VisualTree = factory };
            gripper.DragDelta += (Sender, e) =>
            {
                Canvas.SetLeft(adornedElement, Canvas.GetLeft(adornedElement) + e.HorizontalChange);
                Canvas.SetTop(adornedElement, Canvas.GetTop(adornedElement) + e.VerticalChange);
            };
            base.AddVisualChild(gripper);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            gripper.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            gripper.Arrange(new Rect(finalSize));
            return base.ArrangeOverride(finalSize);
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return gripper != null ? 1 : 0;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return gripper;
        }
    }
}
