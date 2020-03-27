using GalaSoft.MvvmLight;
using Humanizer;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lets_Emoji_UWP.ExportPNG
{
    class ExportViewModel : ViewModelBase
    {
        public CanvasTypography GetEffectiveTypography()
        {
            CanvasTypography typo = new CanvasTypography();
            if (SelectedTypography != null && SelectedTypography.Feature != CanvasTypographyFeatureName.None)
            {
                typo.AddFeature(SelectedTypography.Feature, 1u);
            }
            return typo;
        }

        private TypographyFeatureInfo _selectedTypography;
        public TypographyFeatureInfo SelectedTypography
        {
            get => _selectedTypography;
            set => Set(ref _selectedTypography, value);
        }

        private void Set(ref TypographyFeatureInfo selectedTypography, TypographyFeatureInfo value)
        {
            throw new NotImplementedException();
        }

        public class TypographyFeatureInfo
        {
            public CanvasTypographyFeatureName Feature { get; }

            public string DisplayName { get; }

            public TypographyFeatureInfo(CanvasTypographyFeatureName n)
            {
                Feature = n;

                if (IsNamedFeature(Feature))
                {
                    DisplayName = Feature.Humanize().Transform(To.TitleCase);
                }
                else
                {
                    //
                    // For custom font features, we can produce the OpenType feature tag
                    // using the feature name.
                    //
                    uint id = (uint)(Feature);
                    DisplayName = ("Custom: ") +
                        ((char)((id >> 24) & 0xFF)) +
                        ((char)((id >> 16) & 0xFF)) +
                        ((char)((id >> 8) & 0xFF)) +
                        ((char)((id >> 0) & 0xFF));
                }
            }


            public override string ToString()
            {
                return DisplayName;
            }

            public override bool Equals(object obj)
            {
                if (obj is TypographyFeatureInfo other)
                    return Feature == other.Feature;
                return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            bool IsNamedFeature(CanvasTypographyFeatureName name)
            {
                //
                // DWrite and Win2D support a pre-defined list of typographic features.
                // However, fonts are free to expose features outside of that list.
                // In fact, many built-in fonts have such custom features. 
                // 
                // These custom features are also accessible through Win2D, and 
                // are reported by GetSupportedTypographicFeatureNames.
                //

                return _allValues.Contains(name);
            }

            private static HashSet<CanvasTypographyFeatureName> _allValues { get; } = new HashSet<CanvasTypographyFeatureName>(
                Enum.GetValues(typeof(CanvasTypographyFeatureName)).Cast<CanvasTypographyFeatureName>());
        }
    }
}
