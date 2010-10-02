using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Mediatek.Helpers;

namespace Mediatek.CustomEffects
{
    /// <summary>
    /// Greyscale effect.
    /// From http://windowsclient.net/wpf/wpf35/wpf-35sp1-more-effects.aspx
    /// </summary>
    public class GreyscaleEffect : ShaderEffect
    {
        /// <summary>
        /// Dependency property for Input.
        /// </summary>
        public static readonly DependencyProperty InputProperty =
            RegisterPixelShaderSamplerProperty("Input", typeof(GreyscaleEffect), 0 /* assigned to sampler register S0 */);

        /// <summary>
        /// PixelShader for this effect.
        /// </summary>
        private static readonly PixelShader _pixelShader;

        /// <summary>
        /// Static constructor - Create a PixelShader for all GreyscaleEffect instances. 
        /// </summary>
        static GreyscaleEffect()
        {
            _pixelShader = new PixelShader();
            _pixelShader.UriSource = PackUri.MakePackUri("CustomEffects/Bytecode/Greyscale.ps");
            _pixelShader.Freeze();
        }

        /// <summary>
        /// Constructor - Assign the PixelShader property and set the shader parameters to default values.
        /// </summary>
        public GreyscaleEffect()
        {
            this.PixelShader = _pixelShader;
            UpdateShaderValue(InputProperty);
        }

        /// <summary>
        /// Gets or sets Input properties. 
        /// </summary>
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
    }

}
