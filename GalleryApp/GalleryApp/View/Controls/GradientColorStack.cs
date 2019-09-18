using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace gradient
{
  
   
    public class GradientColorStack : StackLayout
    {
        public static readonly BindableProperty StarColorProperity = BindableProperty.Create("StarColor",
                                                                                               typeof(Color),
                                                                                                typeof(GradientColorStack),
                                                                                                 Color.Default,
                                                                                                     BindingMode.TwoWay,
                                                                                                     propertyChanged:StarColorProperityChanged);

        public Color StarColor
        {
            get { return (Color)GetValue(StarColorProperity); }
            set { SetValue(StarColorProperity, value); }
        }

        public static void StarColorProperityChanged(BindableObject bindable,object oldValue, object newValue)
        {
            if (newValue != null)
            {
                GradientColorStack stack = (GradientColorStack)bindable;
                stack.StarColor = (Color)newValue;
            }

        }

        public static readonly BindableProperty EndColorProperity = BindableProperty.Create("EndColor",
                                                                                               typeof(Color),
                                                                                                typeof(GradientColorStack),
                                                                                                 Color.Default,
                                                                                                     BindingMode.TwoWay,
                                                                                                     propertyChanged: EndColorProperityChanged);

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperity); }
            set { SetValue(EndColorProperity, value); }
        }

        public static void EndColorProperityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                GradientColorStack stack = (GradientColorStack)bindable;
                stack.EndColor = (Color)newValue;
            }

        }



    }
}
