using System.Runtime.InteropServices;

namespace u4.Math;

[StructLayout(LayoutKind.Sequential)]
public struct Color
{
    public float R;

    public float G;

    public float B;

    public float A;

    public Color(float r, float g, float b, float a = 1.0f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(byte r, byte g, byte b, byte a = byte.MaxValue)
    {
        const float byteToFloat = 1.0f / byte.MaxValue;

        R = r * byteToFloat;
        G = g * byteToFloat;
        B = b * byteToFloat;
        A = a * byteToFloat;
    }

    public Color(uint packedValue)
    {
        const float byteToFloat = 1.0f / byte.MaxValue;

        R = (packedValue >> 24) * byteToFloat;
        G = ((packedValue >> 16) & 0xFF) * byteToFloat;
        B = ((packedValue >> 8) & 0xFF) * byteToFloat;
        A = (packedValue & 0xFF) * byteToFloat;
    }
    
    public byte Rb
    {
        get => (byte) (R * byte.MaxValue);
        set => R = value * (1.0f / byte.MaxValue);
    }
    
    public byte Gb
    {
        get => (byte) (G * byte.MaxValue);
        set => G = value * (1.0f / byte.MaxValue);
    }
    
    public byte Bb
    {
        get => (byte) (B * byte.MaxValue);
        set => B = value * (1.0f / byte.MaxValue);
    }
    
    public byte Ab
    {
        get => (byte) (A * byte.MaxValue);
        set => A = value * (1.0f / byte.MaxValue);
    }
    
    /// <summary>
    /// AliceBlue has an RGBA value of 240, 248, 255, 255 (#F0F8FFFF).
    /// </summary>
    public static Color AliceBlue => new Color(0.9411764705882353f, 0.9725490196078431f, 1.0f);

    /// <summary>
    /// AntiqueWhite has an RGBA value of 250, 235, 215, 255 (#FAEBD7FF).
    /// </summary>
    public static Color AntiqueWhite => new Color(0.9803921568627451f, 0.9215686274509803f, 0.8431372549019608f);

    /// <summary>
    /// Aqua has an RGBA value of 0, 255, 255, 255 (#00FFFFFF).
    /// </summary>
    public static Color Aqua => new Color(0.0f, 1.0f, 1.0f);

    /// <summary>
    /// Aquamarine has an RGBA value of 127, 255, 212, 255 (#7FFFD4FF).
    /// </summary>
    public static Color Aquamarine => new Color(0.4980392156862745f, 1.0f, 0.8313725490196079f);

    /// <summary>
    /// Azure has an RGBA value of 240, 255, 255, 255 (#F0FFFFFF).
    /// </summary>
    public static Color Azure => new Color(0.9411764705882353f, 1.0f, 1.0f);

    /// <summary>
    /// Beige has an RGBA value of 245, 245, 220, 255 (#F5F5DCFF).
    /// </summary>
    public static Color Beige => new Color(0.9607843137254902f, 0.9607843137254902f, 0.8627450980392157f);

    /// <summary>
    /// Bisque has an RGBA value of 255, 228, 196, 255 (#FFE4C4FF).
    /// </summary>
    public static Color Bisque => new Color(1.0f, 0.8941176470588236f, 0.7686274509803922f);

    /// <summary>
    /// Black has an RGBA value of 0, 0, 0, 255 (#000000FF).
    /// </summary>
    public static Color Black => new Color(0.0f, 0.0f, 0.0f);

    /// <summary>
    /// BlanchedAlmond has an RGBA value of 255, 235, 205, 255 (#FFEBCDFF).
    /// </summary>
    public static Color BlanchedAlmond => new Color(1.0f, 0.9215686274509803f, 0.803921568627451f);

    /// <summary>
    /// Blue has an RGBA value of 0, 0, 255, 255 (#0000FFFF).
    /// </summary>
    public static Color Blue => new Color(0.0f, 0.0f, 1.0f);

    /// <summary>
    /// BlueViolet has an RGBA value of 138, 43, 226, 255 (#8A2BE2FF).
    /// </summary>
    public static Color BlueViolet => new Color(0.5411764705882353f, 0.16862745098039217f, 0.8862745098039215f);

    /// <summary>
    /// Brown has an RGBA value of 165, 42, 42, 255 (#A52A2AFF).
    /// </summary>
    public static Color Brown => new Color(0.6470588235294118f, 0.16470588235294117f, 0.16470588235294117f);

    /// <summary>
    /// BurlyWood has an RGBA value of 222, 184, 135, 255 (#DEB887FF).
    /// </summary>
    public static Color BurlyWood => new Color(0.8705882352941177f, 0.7215686274509804f, 0.5294117647058824f);

    /// <summary>
    /// CadetBlue has an RGBA value of 95, 158, 160, 255 (#5F9EA0FF).
    /// </summary>
    public static Color CadetBlue => new Color(0.37254901960784315f, 0.6196078431372549f, 0.6274509803921569f);

    /// <summary>
    /// Chartreuse has an RGBA value of 127, 255, 0, 255 (#7FFF00FF).
    /// </summary>
    public static Color Chartreuse => new Color(0.4980392156862745f, 1.0f, 0.0f);

    /// <summary>
    /// Chocolate has an RGBA value of 210, 105, 30, 255 (#D2691EFF).
    /// </summary>
    public static Color Chocolate => new Color(0.8235294117647058f, 0.4117647058823529f, 0.11764705882352941f);

    /// <summary>
    /// Coral has an RGBA value of 255, 127, 80, 255 (#FF7F50FF).
    /// </summary>
    public static Color Coral => new Color(1.0f, 0.4980392156862745f, 0.3137254901960784f);

    /// <summary>
    /// CornflowerBlue has an RGBA value of 100, 149, 237, 255 (#6495EDFF).
    /// </summary>
    public static Color CornflowerBlue => new Color(0.39215686274509803f, 0.5843137254901961f, 0.9294117647058824f);

    /// <summary>
    /// Cornsilk has an RGBA value of 255, 248, 220, 255 (#FFF8DCFF).
    /// </summary>
    public static Color Cornsilk => new Color(1.0f, 0.9725490196078431f, 0.8627450980392157f);

    /// <summary>
    /// Crimson has an RGBA value of 220, 20, 60, 255 (#DC143CFF).
    /// </summary>
    public static Color Crimson => new Color(0.8627450980392157f, 0.0784313725490196f, 0.23529411764705882f);

    /// <summary>
    /// Cyan has an RGBA value of 0, 255, 255, 255 (#00FFFFFF).
    /// </summary>
    public static Color Cyan => new Color(0.0f, 1.0f, 1.0f);

    /// <summary>
    /// DarkBlue has an RGBA value of 0, 0, 139, 255 (#00008BFF).
    /// </summary>
    public static Color DarkBlue => new Color(0.0f, 0.0f, 0.5450980392156862f);

    /// <summary>
    /// DarkCyan has an RGBA value of 0, 139, 139, 255 (#008B8BFF).
    /// </summary>
    public static Color DarkCyan => new Color(0.0f, 0.5450980392156862f, 0.5450980392156862f);

    /// <summary>
    /// DarkGoldenRod has an RGBA value of 184, 134, 11, 255 (#B8860BFF).
    /// </summary>
    public static Color DarkGoldenRod => new Color(0.7215686274509804f, 0.5254901960784314f, 0.043137254901960784f);

    /// <summary>
    /// DarkGray has an RGBA value of 169, 169, 169, 255 (#A9A9A9FF).
    /// </summary>
    public static Color DarkGray => new Color(0.6627450980392157f, 0.6627450980392157f, 0.6627450980392157f);

    /// <summary>
    /// DarkGrey has an RGBA value of 169, 169, 169, 255 (#A9A9A9FF).
    /// </summary>
    public static Color DarkGrey => new Color(0.6627450980392157f, 0.6627450980392157f, 0.6627450980392157f);

    /// <summary>
    /// DarkGreen has an RGBA value of 0, 100, 0, 255 (#006400FF).
    /// </summary>
    public static Color DarkGreen => new Color(0.0f, 0.39215686274509803f, 0.0f);

    /// <summary>
    /// DarkKhaki has an RGBA value of 189, 183, 107, 255 (#BDB76BFF).
    /// </summary>
    public static Color DarkKhaki => new Color(0.7411764705882353f, 0.7176470588235294f, 0.4196078431372549f);

    /// <summary>
    /// DarkMagenta has an RGBA value of 139, 0, 139, 255 (#8B008BFF).
    /// </summary>
    public static Color DarkMagenta => new Color(0.5450980392156862f, 0.0f, 0.5450980392156862f);

    /// <summary>
    /// DarkOliveGreen has an RGBA value of 85, 107, 47, 255 (#556B2FFF).
    /// </summary>
    public static Color DarkOliveGreen => new Color(0.3333333333333333f, 0.4196078431372549f, 0.1843137254901961f);

    /// <summary>
    /// DarkOrange has an RGBA value of 255, 140, 0, 255 (#FF8C00FF).
    /// </summary>
    public static Color DarkOrange => new Color(1.0f, 0.5490196078431373f, 0.0f);

    /// <summary>
    /// DarkOrchid has an RGBA value of 153, 50, 204, 255 (#9932CCFF).
    /// </summary>
    public static Color DarkOrchid => new Color(0.6f, 0.19607843137254902f, 0.8f);

    /// <summary>
    /// DarkRed has an RGBA value of 139, 0, 0, 255 (#8B0000FF).
    /// </summary>
    public static Color DarkRed => new Color(0.5450980392156862f, 0.0f, 0.0f);

    /// <summary>
    /// DarkSalmon has an RGBA value of 233, 150, 122, 255 (#E9967AFF).
    /// </summary>
    public static Color DarkSalmon => new Color(0.9137254901960784f, 0.5882352941176471f, 0.47843137254901963f);

    /// <summary>
    /// DarkSeaGreen has an RGBA value of 143, 188, 143, 255 (#8FBC8FFF).
    /// </summary>
    public static Color DarkSeaGreen => new Color(0.5607843137254902f, 0.7372549019607844f, 0.5607843137254902f);

    /// <summary>
    /// DarkSlateBlue has an RGBA value of 72, 61, 139, 255 (#483D8BFF).
    /// </summary>
    public static Color DarkSlateBlue => new Color(0.2823529411764706f, 0.23921568627450981f, 0.5450980392156862f);

    /// <summary>
    /// DarkSlateGray has an RGBA value of 47, 79, 79, 255 (#2F4F4FFF).
    /// </summary>
    public static Color DarkSlateGray => new Color(0.1843137254901961f, 0.30980392156862746f, 0.30980392156862746f);

    /// <summary>
    /// DarkSlateGrey has an RGBA value of 47, 79, 79, 255 (#2F4F4FFF).
    /// </summary>
    public static Color DarkSlateGrey => new Color(0.1843137254901961f, 0.30980392156862746f, 0.30980392156862746f);

    /// <summary>
    /// DarkTurquoise has an RGBA value of 0, 206, 209, 255 (#00CED1FF).
    /// </summary>
    public static Color DarkTurquoise => new Color(0.0f, 0.807843137254902f, 0.8196078431372549f);

    /// <summary>
    /// DarkViolet has an RGBA value of 148, 0, 211, 255 (#9400D3FF).
    /// </summary>
    public static Color DarkViolet => new Color(0.5803921568627451f, 0.0f, 0.8274509803921568f);

    /// <summary>
    /// DeepPink has an RGBA value of 255, 20, 147, 255 (#FF1493FF).
    /// </summary>
    public static Color DeepPink => new Color(1.0f, 0.0784313725490196f, 0.5764705882352941f);

    /// <summary>
    /// DeepSkyBlue has an RGBA value of 0, 191, 255, 255 (#00BFFFFF).
    /// </summary>
    public static Color DeepSkyBlue => new Color(0.0f, 0.7490196078431373f, 1.0f);

    /// <summary>
    /// DimGray has an RGBA value of 105, 105, 105, 255 (#696969FF).
    /// </summary>
    public static Color DimGray => new Color(0.4117647058823529f, 0.4117647058823529f, 0.4117647058823529f);

    /// <summary>
    /// DimGrey has an RGBA value of 105, 105, 105, 255 (#696969FF).
    /// </summary>
    public static Color DimGrey => new Color(0.4117647058823529f, 0.4117647058823529f, 0.4117647058823529f);

    /// <summary>
    /// DodgerBlue has an RGBA value of 30, 144, 255, 255 (#1E90FFFF).
    /// </summary>
    public static Color DodgerBlue => new Color(0.11764705882352941f, 0.5647058823529412f, 1.0f);

    /// <summary>
    /// FireBrick has an RGBA value of 178, 34, 34, 255 (#B22222FF).
    /// </summary>
    public static Color FireBrick => new Color(0.6980392156862745f, 0.13333333333333333f, 0.13333333333333333f);

    /// <summary>
    /// FloralWhite has an RGBA value of 255, 250, 240, 255 (#FFFAF0FF).
    /// </summary>
    public static Color FloralWhite => new Color(1.0f, 0.9803921568627451f, 0.9411764705882353f);

    /// <summary>
    /// ForestGreen has an RGBA value of 34, 139, 34, 255 (#228B22FF).
    /// </summary>
    public static Color ForestGreen => new Color(0.13333333333333333f, 0.5450980392156862f, 0.13333333333333333f);

    /// <summary>
    /// Fuchsia has an RGBA value of 255, 0, 255, 255 (#FF00FFFF).
    /// </summary>
    public static Color Fuchsia => new Color(1.0f, 0.0f, 1.0f);

    /// <summary>
    /// Gainsboro has an RGBA value of 220, 220, 220, 255 (#DCDCDCFF).
    /// </summary>
    public static Color Gainsboro => new Color(0.8627450980392157f, 0.8627450980392157f, 0.8627450980392157f);

    /// <summary>
    /// GhostWhite has an RGBA value of 248, 248, 255, 255 (#F8F8FFFF).
    /// </summary>
    public static Color GhostWhite => new Color(0.9725490196078431f, 0.9725490196078431f, 1.0f);

    /// <summary>
    /// Gold has an RGBA value of 255, 215, 0, 255 (#FFD700FF).
    /// </summary>
    public static Color Gold => new Color(1.0f, 0.8431372549019608f, 0.0f);

    /// <summary>
    /// GoldenRod has an RGBA value of 218, 165, 32, 255 (#DAA520FF).
    /// </summary>
    public static Color GoldenRod => new Color(0.8549019607843137f, 0.6470588235294118f, 0.12549019607843137f);

    /// <summary>
    /// Gray has an RGBA value of 128, 128, 128, 255 (#808080FF).
    /// </summary>
    public static Color Gray => new Color(0.5019607843137255f, 0.5019607843137255f, 0.5019607843137255f);

    /// <summary>
    /// Grey has an RGBA value of 128, 128, 128, 255 (#808080FF).
    /// </summary>
    public static Color Grey => new Color(0.5019607843137255f, 0.5019607843137255f, 0.5019607843137255f);

    /// <summary>
    /// Green has an RGBA value of 0, 128, 0, 255 (#008000FF).
    /// </summary>
    public static Color Green => new Color(0.0f, 0.5019607843137255f, 0.0f);

    /// <summary>
    /// GreenYellow has an RGBA value of 173, 255, 47, 255 (#ADFF2FFF).
    /// </summary>
    public static Color GreenYellow => new Color(0.6784313725490196f, 1.0f, 0.1843137254901961f);

    /// <summary>
    /// HoneyDew has an RGBA value of 240, 255, 240, 255 (#F0FFF0FF).
    /// </summary>
    public static Color HoneyDew => new Color(0.9411764705882353f, 1.0f, 0.9411764705882353f);

    /// <summary>
    /// HotPink has an RGBA value of 255, 105, 180, 255 (#FF69B4FF).
    /// </summary>
    public static Color HotPink => new Color(1.0f, 0.4117647058823529f, 0.7058823529411765f);

    /// <summary>
    /// IndianRed has an RGBA value of 205, 92, 92, 255 (#CD5C5CFF).
    /// </summary>
    public static Color IndianRed => new Color(0.803921568627451f, 0.3607843137254902f, 0.3607843137254902f);

    /// <summary>
    /// Indigo has an RGBA value of 75, 0, 130, 255 (#4B0082FF).
    /// </summary>
    public static Color Indigo => new Color(0.29411764705882354f, 0.0f, 0.5098039215686274f);

    /// <summary>
    /// Ivory has an RGBA value of 255, 255, 240, 255 (#FFFFF0FF).
    /// </summary>
    public static Color Ivory => new Color(1.0f, 1.0f, 0.9411764705882353f);

    /// <summary>
    /// Khaki has an RGBA value of 240, 230, 140, 255 (#F0E68CFF).
    /// </summary>
    public static Color Khaki => new Color(0.9411764705882353f, 0.9019607843137255f, 0.5490196078431373f);

    /// <summary>
    /// Lavender has an RGBA value of 230, 230, 250, 255 (#E6E6FAFF).
    /// </summary>
    public static Color Lavender => new Color(0.9019607843137255f, 0.9019607843137255f, 0.9803921568627451f);

    /// <summary>
    /// LavenderBlush has an RGBA value of 255, 240, 245, 255 (#FFF0F5FF).
    /// </summary>
    public static Color LavenderBlush => new Color(1.0f, 0.9411764705882353f, 0.9607843137254902f);

    /// <summary>
    /// LawnGreen has an RGBA value of 124, 252, 0, 255 (#7CFC00FF).
    /// </summary>
    public static Color LawnGreen => new Color(0.48627450980392156f, 0.9882352941176471f, 0.0f);

    /// <summary>
    /// LemonChiffon has an RGBA value of 255, 250, 205, 255 (#FFFACDFF).
    /// </summary>
    public static Color LemonChiffon => new Color(1.0f, 0.9803921568627451f, 0.803921568627451f);

    /// <summary>
    /// LightBlue has an RGBA value of 173, 216, 230, 255 (#ADD8E6FF).
    /// </summary>
    public static Color LightBlue => new Color(0.6784313725490196f, 0.8470588235294118f, 0.9019607843137255f);

    /// <summary>
    /// LightCoral has an RGBA value of 240, 128, 128, 255 (#F08080FF).
    /// </summary>
    public static Color LightCoral => new Color(0.9411764705882353f, 0.5019607843137255f, 0.5019607843137255f);

    /// <summary>
    /// LightCyan has an RGBA value of 224, 255, 255, 255 (#E0FFFFFF).
    /// </summary>
    public static Color LightCyan => new Color(0.8784313725490196f, 1.0f, 1.0f);

    /// <summary>
    /// LightGoldenRodYellow has an RGBA value of 250, 250, 210, 255 (#FAFAD2FF).
    /// </summary>
    public static Color LightGoldenRodYellow => new Color(0.9803921568627451f, 0.9803921568627451f, 0.8235294117647058f);

    /// <summary>
    /// LightGray has an RGBA value of 211, 211, 211, 255 (#D3D3D3FF).
    /// </summary>
    public static Color LightGray => new Color(0.8274509803921568f, 0.8274509803921568f, 0.8274509803921568f);

    /// <summary>
    /// LightGrey has an RGBA value of 211, 211, 211, 255 (#D3D3D3FF).
    /// </summary>
    public static Color LightGrey => new Color(0.8274509803921568f, 0.8274509803921568f, 0.8274509803921568f);

    /// <summary>
    /// LightGreen has an RGBA value of 144, 238, 144, 255 (#90EE90FF).
    /// </summary>
    public static Color LightGreen => new Color(0.5647058823529412f, 0.9333333333333333f, 0.5647058823529412f);

    /// <summary>
    /// LightPink has an RGBA value of 255, 182, 193, 255 (#FFB6C1FF).
    /// </summary>
    public static Color LightPink => new Color(1.0f, 0.7137254901960784f, 0.7568627450980392f);

    /// <summary>
    /// LightSalmon has an RGBA value of 255, 160, 122, 255 (#FFA07AFF).
    /// </summary>
    public static Color LightSalmon => new Color(1.0f, 0.6274509803921569f, 0.47843137254901963f);

    /// <summary>
    /// LightSeaGreen has an RGBA value of 32, 178, 170, 255 (#20B2AAFF).
    /// </summary>
    public static Color LightSeaGreen => new Color(0.12549019607843137f, 0.6980392156862745f, 0.6666666666666666f);

    /// <summary>
    /// LightSkyBlue has an RGBA value of 135, 206, 250, 255 (#87CEFAFF).
    /// </summary>
    public static Color LightSkyBlue => new Color(0.5294117647058824f, 0.807843137254902f, 0.9803921568627451f);

    /// <summary>
    /// LightSlateGray has an RGBA value of 119, 136, 153, 255 (#778899FF).
    /// </summary>
    public static Color LightSlateGray => new Color(0.4666666666666667f, 0.5333333333333333f, 0.6f);

    /// <summary>
    /// LightSlateGrey has an RGBA value of 119, 136, 153, 255 (#778899FF).
    /// </summary>
    public static Color LightSlateGrey => new Color(0.4666666666666667f, 0.5333333333333333f, 0.6f);

    /// <summary>
    /// LightSteelBlue has an RGBA value of 176, 196, 222, 255 (#B0C4DEFF).
    /// </summary>
    public static Color LightSteelBlue => new Color(0.6901960784313725f, 0.7686274509803922f, 0.8705882352941177f);

    /// <summary>
    /// LightYellow has an RGBA value of 255, 255, 224, 255 (#FFFFE0FF).
    /// </summary>
    public static Color LightYellow => new Color(1.0f, 1.0f, 0.8784313725490196f);

    /// <summary>
    /// Lime has an RGBA value of 0, 255, 0, 255 (#00FF00FF).
    /// </summary>
    public static Color Lime => new Color(0.0f, 1.0f, 0.0f);

    /// <summary>
    /// LimeGreen has an RGBA value of 50, 205, 50, 255 (#32CD32FF).
    /// </summary>
    public static Color LimeGreen => new Color(0.19607843137254902f, 0.803921568627451f, 0.19607843137254902f);

    /// <summary>
    /// Linen has an RGBA value of 250, 240, 230, 255 (#FAF0E6FF).
    /// </summary>
    public static Color Linen => new Color(0.9803921568627451f, 0.9411764705882353f, 0.9019607843137255f);

    /// <summary>
    /// Magenta has an RGBA value of 255, 0, 255, 255 (#FF00FFFF).
    /// </summary>
    public static Color Magenta => new Color(1.0f, 0.0f, 1.0f);

    /// <summary>
    /// Maroon has an RGBA value of 128, 0, 0, 255 (#800000FF).
    /// </summary>
    public static Color Maroon => new Color(0.5019607843137255f, 0.0f, 0.0f);

    /// <summary>
    /// MediumAquaMarine has an RGBA value of 102, 205, 170, 255 (#66CDAAFF).
    /// </summary>
    public static Color MediumAquaMarine => new Color(0.4f, 0.803921568627451f, 0.6666666666666666f);

    /// <summary>
    /// MediumBlue has an RGBA value of 0, 0, 205, 255 (#0000CDFF).
    /// </summary>
    public static Color MediumBlue => new Color(0.0f, 0.0f, 0.803921568627451f);

    /// <summary>
    /// MediumOrchid has an RGBA value of 186, 85, 211, 255 (#BA55D3FF).
    /// </summary>
    public static Color MediumOrchid => new Color(0.7294117647058823f, 0.3333333333333333f, 0.8274509803921568f);

    /// <summary>
    /// MediumPurple has an RGBA value of 147, 112, 219, 255 (#9370DBFF).
    /// </summary>
    public static Color MediumPurple => new Color(0.5764705882352941f, 0.4392156862745098f, 0.8588235294117647f);

    /// <summary>
    /// MediumSeaGreen has an RGBA value of 60, 179, 113, 255 (#3CB371FF).
    /// </summary>
    public static Color MediumSeaGreen => new Color(0.23529411764705882f, 0.7019607843137254f, 0.44313725490196076f);

    /// <summary>
    /// MediumSlateBlue has an RGBA value of 123, 104, 238, 255 (#7B68EEFF).
    /// </summary>
    public static Color MediumSlateBlue => new Color(0.4823529411764706f, 0.40784313725490196f, 0.9333333333333333f);

    /// <summary>
    /// MediumSpringGreen has an RGBA value of 0, 250, 154, 255 (#00FA9AFF).
    /// </summary>
    public static Color MediumSpringGreen => new Color(0.0f, 0.9803921568627451f, 0.6039215686274509f);

    /// <summary>
    /// MediumTurquoise has an RGBA value of 72, 209, 204, 255 (#48D1CCFF).
    /// </summary>
    public static Color MediumTurquoise => new Color(0.2823529411764706f, 0.8196078431372549f, 0.8f);

    /// <summary>
    /// MediumVioletRed has an RGBA value of 199, 21, 133, 255 (#C71585FF).
    /// </summary>
    public static Color MediumVioletRed => new Color(0.7803921568627451f, 0.08235294117647059f, 0.5215686274509804f);

    /// <summary>
    /// MidnightBlue has an RGBA value of 25, 25, 112, 255 (#191970FF).
    /// </summary>
    public static Color MidnightBlue => new Color(0.09803921568627451f, 0.09803921568627451f, 0.4392156862745098f);

    /// <summary>
    /// MintCream has an RGBA value of 245, 255, 250, 255 (#F5FFFAFF).
    /// </summary>
    public static Color MintCream => new Color(0.9607843137254902f, 1.0f, 0.9803921568627451f);

    /// <summary>
    /// MistyRose has an RGBA value of 255, 228, 225, 255 (#FFE4E1FF).
    /// </summary>
    public static Color MistyRose => new Color(1.0f, 0.8941176470588236f, 0.8823529411764706f);

    /// <summary>
    /// Moccasin has an RGBA value of 255, 228, 181, 255 (#FFE4B5FF).
    /// </summary>
    public static Color Moccasin => new Color(1.0f, 0.8941176470588236f, 0.7098039215686275f);

    /// <summary>
    /// NavajoWhite has an RGBA value of 255, 222, 173, 255 (#FFDEADFF).
    /// </summary>
    public static Color NavajoWhite => new Color(1.0f, 0.8705882352941177f, 0.6784313725490196f);

    /// <summary>
    /// Navy has an RGBA value of 0, 0, 128, 255 (#000080FF).
    /// </summary>
    public static Color Navy => new Color(0.0f, 0.0f, 0.5019607843137255f);

    /// <summary>
    /// OldLace has an RGBA value of 253, 245, 230, 255 (#FDF5E6FF).
    /// </summary>
    public static Color OldLace => new Color(0.9921568627450981f, 0.9607843137254902f, 0.9019607843137255f);

    /// <summary>
    /// Olive has an RGBA value of 128, 128, 0, 255 (#808000FF).
    /// </summary>
    public static Color Olive => new Color(0.5019607843137255f, 0.5019607843137255f, 0.0f);

    /// <summary>
    /// OliveDrab has an RGBA value of 107, 142, 35, 255 (#6B8E23FF).
    /// </summary>
    public static Color OliveDrab => new Color(0.4196078431372549f, 0.5568627450980392f, 0.13725490196078433f);

    /// <summary>
    /// Orange has an RGBA value of 255, 165, 0, 255 (#FFA500FF).
    /// </summary>
    public static Color Orange => new Color(1.0f, 0.6470588235294118f, 0.0f);

    /// <summary>
    /// OrangeRed has an RGBA value of 255, 69, 0, 255 (#FF4500FF).
    /// </summary>
    public static Color OrangeRed => new Color(1.0f, 0.27058823529411763f, 0.0f);

    /// <summary>
    /// Orchid has an RGBA value of 218, 112, 214, 255 (#DA70D6FF).
    /// </summary>
    public static Color Orchid => new Color(0.8549019607843137f, 0.4392156862745098f, 0.8392156862745098f);

    /// <summary>
    /// PaleGoldenRod has an RGBA value of 238, 232, 170, 255 (#EEE8AAFF).
    /// </summary>
    public static Color PaleGoldenRod => new Color(0.9333333333333333f, 0.9098039215686274f, 0.6666666666666666f);

    /// <summary>
    /// PaleGreen has an RGBA value of 152, 251, 152, 255 (#98FB98FF).
    /// </summary>
    public static Color PaleGreen => new Color(0.596078431372549f, 0.984313725490196f, 0.596078431372549f);

    /// <summary>
    /// PaleTurquoise has an RGBA value of 175, 238, 238, 255 (#AFEEEEFF).
    /// </summary>
    public static Color PaleTurquoise => new Color(0.6862745098039216f, 0.9333333333333333f, 0.9333333333333333f);

    /// <summary>
    /// PaleVioletRed has an RGBA value of 219, 112, 147, 255 (#DB7093FF).
    /// </summary>
    public static Color PaleVioletRed => new Color(0.8588235294117647f, 0.4392156862745098f, 0.5764705882352941f);

    /// <summary>
    /// PapayaWhip has an RGBA value of 255, 239, 213, 255 (#FFEFD5FF).
    /// </summary>
    public static Color PapayaWhip => new Color(1.0f, 0.9372549019607843f, 0.8352941176470589f);

    /// <summary>
    /// PeachPuff has an RGBA value of 255, 218, 185, 255 (#FFDAB9FF).
    /// </summary>
    public static Color PeachPuff => new Color(1.0f, 0.8549019607843137f, 0.7254901960784313f);

    /// <summary>
    /// Peru has an RGBA value of 205, 133, 63, 255 (#CD853FFF).
    /// </summary>
    public static Color Peru => new Color(0.803921568627451f, 0.5215686274509804f, 0.24705882352941178f);

    /// <summary>
    /// Pink has an RGBA value of 255, 192, 203, 255 (#FFC0CBFF).
    /// </summary>
    public static Color Pink => new Color(1.0f, 0.7529411764705882f, 0.796078431372549f);

    /// <summary>
    /// Plum has an RGBA value of 221, 160, 221, 255 (#DDA0DDFF).
    /// </summary>
    public static Color Plum => new Color(0.8666666666666667f, 0.6274509803921569f, 0.8666666666666667f);

    /// <summary>
    /// PowderBlue has an RGBA value of 176, 224, 230, 255 (#B0E0E6FF).
    /// </summary>
    public static Color PowderBlue => new Color(0.6901960784313725f, 0.8784313725490196f, 0.9019607843137255f);

    /// <summary>
    /// Purple has an RGBA value of 128, 0, 128, 255 (#800080FF).
    /// </summary>
    public static Color Purple => new Color(0.5019607843137255f, 0.0f, 0.5019607843137255f);

    /// <summary>
    /// RebeccaPurple has an RGBA value of 102, 51, 153, 255 (#663399FF).
    /// </summary>
    public static Color RebeccaPurple => new Color(0.4f, 0.2f, 0.6f);

    /// <summary>
    /// Red has an RGBA value of 255, 0, 0, 255 (#FF0000FF).
    /// </summary>
    public static Color Red => new Color(1.0f, 0.0f, 0.0f);

    /// <summary>
    /// RosyBrown has an RGBA value of 188, 143, 143, 255 (#BC8F8FFF).
    /// </summary>
    public static Color RosyBrown => new Color(0.7372549019607844f, 0.5607843137254902f, 0.5607843137254902f);

    /// <summary>
    /// RoyalBlue has an RGBA value of 65, 105, 225, 255 (#4169E1FF).
    /// </summary>
    public static Color RoyalBlue => new Color(0.2549019607843137f, 0.4117647058823529f, 0.8823529411764706f);

    /// <summary>
    /// SaddleBrown has an RGBA value of 139, 69, 19, 255 (#8B4513FF).
    /// </summary>
    public static Color SaddleBrown => new Color(0.5450980392156862f, 0.27058823529411763f, 0.07450980392156863f);

    /// <summary>
    /// Salmon has an RGBA value of 250, 128, 114, 255 (#FA8072FF).
    /// </summary>
    public static Color Salmon => new Color(0.9803921568627451f, 0.5019607843137255f, 0.4470588235294118f);

    /// <summary>
    /// SandyBrown has an RGBA value of 244, 164, 96, 255 (#F4A460FF).
    /// </summary>
    public static Color SandyBrown => new Color(0.9568627450980393f, 0.6431372549019608f, 0.3764705882352941f);

    /// <summary>
    /// SeaGreen has an RGBA value of 46, 139, 87, 255 (#2E8B57FF).
    /// </summary>
    public static Color SeaGreen => new Color(0.1803921568627451f, 0.5450980392156862f, 0.3411764705882353f);

    /// <summary>
    /// SeaShell has an RGBA value of 255, 245, 238, 255 (#FFF5EEFF).
    /// </summary>
    public static Color SeaShell => new Color(1.0f, 0.9607843137254902f, 0.9333333333333333f);

    /// <summary>
    /// Sienna has an RGBA value of 160, 82, 45, 255 (#A0522DFF).
    /// </summary>
    public static Color Sienna => new Color(0.6274509803921569f, 0.3215686274509804f, 0.17647058823529413f);

    /// <summary>
    /// Silver has an RGBA value of 192, 192, 192, 255 (#C0C0C0FF).
    /// </summary>
    public static Color Silver => new Color(0.7529411764705882f, 0.7529411764705882f, 0.7529411764705882f);

    /// <summary>
    /// SkyBlue has an RGBA value of 135, 206, 235, 255 (#87CEEBFF).
    /// </summary>
    public static Color SkyBlue => new Color(0.5294117647058824f, 0.807843137254902f, 0.9215686274509803f);

    /// <summary>
    /// SlateBlue has an RGBA value of 106, 90, 205, 255 (#6A5ACDFF).
    /// </summary>
    public static Color SlateBlue => new Color(0.41568627450980394f, 0.35294117647058826f, 0.803921568627451f);

    /// <summary>
    /// SlateGray has an RGBA value of 112, 128, 144, 255 (#708090FF).
    /// </summary>
    public static Color SlateGray => new Color(0.4392156862745098f, 0.5019607843137255f, 0.5647058823529412f);

    /// <summary>
    /// SlateGrey has an RGBA value of 112, 128, 144, 255 (#708090FF).
    /// </summary>
    public static Color SlateGrey => new Color(0.4392156862745098f, 0.5019607843137255f, 0.5647058823529412f);

    /// <summary>
    /// Snow has an RGBA value of 255, 250, 250, 255 (#FFFAFAFF).
    /// </summary>
    public static Color Snow => new Color(1.0f, 0.9803921568627451f, 0.9803921568627451f);

    /// <summary>
    /// SpringGreen has an RGBA value of 0, 255, 127, 255 (#00FF7FFF).
    /// </summary>
    public static Color SpringGreen => new Color(0.0f, 1.0f, 0.4980392156862745f);

    /// <summary>
    /// SteelBlue has an RGBA value of 70, 130, 180, 255 (#4682B4FF).
    /// </summary>
    public static Color SteelBlue => new Color(0.27450980392156865f, 0.5098039215686274f, 0.7058823529411765f);

    /// <summary>
    /// Tan has an RGBA value of 210, 180, 140, 255 (#D2B48CFF).
    /// </summary>
    public static Color Tan => new Color(0.8235294117647058f, 0.7058823529411765f, 0.5490196078431373f);

    /// <summary>
    /// Teal has an RGBA value of 0, 128, 128, 255 (#008080FF).
    /// </summary>
    public static Color Teal => new Color(0.0f, 0.5019607843137255f, 0.5019607843137255f);

    /// <summary>
    /// Thistle has an RGBA value of 216, 191, 216, 255 (#D8BFD8FF).
    /// </summary>
    public static Color Thistle => new Color(0.8470588235294118f, 0.7490196078431373f, 0.8470588235294118f);

    /// <summary>
    /// Tomato has an RGBA value of 255, 99, 71, 255 (#FF6347FF).
    /// </summary>
    public static Color Tomato => new Color(1.0f, 0.38823529411764707f, 0.2784313725490196f);

    /// <summary>
    /// Turquoise has an RGBA value of 64, 224, 208, 255 (#40E0D0FF).
    /// </summary>
    public static Color Turquoise => new Color(0.25098039215686274f, 0.8784313725490196f, 0.8156862745098039f);

    /// <summary>
    /// Violet has an RGBA value of 238, 130, 238, 255 (#EE82EEFF).
    /// </summary>
    public static Color Violet => new Color(0.9333333333333333f, 0.5098039215686274f, 0.9333333333333333f);

    /// <summary>
    /// Wheat has an RGBA value of 245, 222, 179, 255 (#F5DEB3FF).
    /// </summary>
    public static Color Wheat => new Color(0.9607843137254902f, 0.8705882352941177f, 0.7019607843137254f);

    /// <summary>
    /// White has an RGBA value of 255, 255, 255, 255 (#FFFFFFFF).
    /// </summary>
    public static Color White => new Color(1.0f, 1.0f, 1.0f);

    /// <summary>
    /// WhiteSmoke has an RGBA value of 245, 245, 245, 255 (#F5F5F5FF).
    /// </summary>
    public static Color WhiteSmoke => new Color(0.9607843137254902f, 0.9607843137254902f, 0.9607843137254902f);

    /// <summary>
    /// Yellow has an RGBA value of 255, 255, 0, 255 (#FFFF00FF).
    /// </summary>
    public static Color Yellow => new Color(1.0f, 1.0f, 0.0f);

    /// <summary>
    /// YellowGreen has an RGBA value of 154, 205, 50, 255 (#9ACD32FF).
    /// </summary>
    public static Color YellowGreen => new Color(0.6039215686274509f, 0.803921568627451f, 0.19607843137254902f);

    /// <summary>
    /// Transparent has an RGBA value of 0, 0, 0, 0 (#00000000).
    /// </summary>
    public static Color Transparent => new Color(0.0f, 0.0f, 0.0f, 0.0f);
}