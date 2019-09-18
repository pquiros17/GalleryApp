using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using gradient;
using GalleryApp.Droid.Controls;

[assembly:ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]
namespace GalleryApp.Droid.Controls
{
    public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
    {
        private Xamarin.Forms.Color StarColor {get; set;}
        private Xamarin.Forms.Color EndColor { get; set;}

        protected override void DispatchDraw(Android.Graphics.Canvas canvas)
        {
            var gradients = new Android.Graphics.LinearGradient(0, 0, 0, Height, this.StarColor.ToAndroid(), this.EndColor.ToAndroid(), Android.Graphics.Shader.TileMode.Mirror);
            var paint = new Android.Graphics.Paint()
            {
                Dither = true

            };
            paint.SetShader(gradients);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {

            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                var stack = e.NewElement as GradientColorStack;
                this.StarColor = stack.StarColor;
                this.EndColor = stack.EndColor;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("ERRIR",e.ToString());
            }
        }

    }
}