using System.Diagnostics.CodeAnalysis;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D_PRIMITIVE_TOPOLOGY;
using static TerraFX.Interop.DirectX.D3D11_INPUT_CLASSIFICATION;
using static TerraFX.Interop.DirectX.DXGI_FORMAT;

namespace u4.Render.Backend.D3D11;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal static class D3D11Utils
{
    public static D3D_PRIMITIVE_TOPOLOGY ToPrimitiveTopology(this PrimitiveType type)
    {
        return type switch
        {
            PrimitiveType.TriangleList => D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public static D3D11_INPUT_CLASSIFICATION ToInputClassification(this InputType type)
    {
        return type switch
        {
            InputType.PerVertex => D3D11_INPUT_PER_VERTEX_DATA,
            InputType.PerInstance => D3D11_INPUT_PER_INSTANCE_DATA,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public static DXGI_FORMAT ToDxgiFormat(this Format format)
    {
        return format switch
        {
            Format.R1UNorm => DXGI_FORMAT_R1_UNORM,
            Format.A8UNorm => DXGI_FORMAT_A8_UNORM,
            Format.R8UNorm => DXGI_FORMAT_R8_UNORM,
            Format.R8UInt => DXGI_FORMAT_R8_UINT,
            Format.R8SNorm => DXGI_FORMAT_R8_SNORM,
            Format.R8SInt => DXGI_FORMAT_R8_SINT,
            Format.R8G8UNorm => DXGI_FORMAT_R8G8_UNORM,
            Format.R8G8UInt => DXGI_FORMAT_R8G8_UINT,
            Format.R8G8SNorm => DXGI_FORMAT_R8G8_SNORM,
            Format.R8G8SInt => DXGI_FORMAT_R8G8_SINT,
            Format.R8G8B8A8UNorm => DXGI_FORMAT_R8G8B8A8_UNORM,
            Format.R8G8B8A8UNormSRGB => DXGI_FORMAT_R8G8B8A8_UNORM_SRGB,
            Format.R8G8B8A8UInt => DXGI_FORMAT_R8G8B8A8_UINT,
            Format.R8G8B8A8SNorm => DXGI_FORMAT_R8G8B8A8_SNORM,
            Format.R8G8B8A8SInt => DXGI_FORMAT_R8G8B8A8_SINT,
            Format.B8G8R8A8UNorm => DXGI_FORMAT_B8G8R8A8_UNORM,
            Format.B8G8R8A8UNormSRGB => DXGI_FORMAT_B8G8R8A8_UNORM_SRGB,
            Format.R16Float => DXGI_FORMAT_R16_FLOAT,
            Format.R16UNorm => DXGI_FORMAT_R16_UNORM,
            Format.R16UInt => DXGI_FORMAT_R16_UINT,
            Format.R16SNorm => DXGI_FORMAT_R16_SNORM,
            Format.R16SInt => DXGI_FORMAT_R16_SINT,
            Format.R16G16Float => DXGI_FORMAT_R16G16_FLOAT,
            Format.R16G16UNorm => DXGI_FORMAT_R16G16_UNORM,
            Format.R16G16UInt => DXGI_FORMAT_R16G16_UINT,
            Format.R16G16SNorm => DXGI_FORMAT_R16G16_SNORM,
            Format.R16G16SInt => DXGI_FORMAT_R16G16_SINT,
            Format.R16G16B16A16Float => DXGI_FORMAT_R16G16B16A16_FLOAT,
            Format.R16G16B16A16UNorm => DXGI_FORMAT_R16G16B16A16_UINT,
            Format.R16G16B16A16UInt => DXGI_FORMAT_R16G16B16A16_UINT,
            Format.R16G16B16A16SNorm => DXGI_FORMAT_R16G16B16A16_SNORM,
            Format.R16G16B16A16SInt => DXGI_FORMAT_R16G16B16A16_SINT,
            Format.R32Float => DXGI_FORMAT_R32_FLOAT,
            Format.R32UInt => DXGI_FORMAT_R32_UINT,
            Format.R32SInt => DXGI_FORMAT_R32_SINT,
            Format.R32G32Float => DXGI_FORMAT_R32G32_FLOAT,
            Format.R32G32UInt => DXGI_FORMAT_R32G32_UINT,
            Format.R32G32SInt => DXGI_FORMAT_R32G32_SINT,
            Format.R32G32B32A32Float => DXGI_FORMAT_R32G32B32A32_FLOAT,
            Format.R32G32B32A32UInt => DXGI_FORMAT_R32G32B32A32_UINT,
            Format.R32G32B32A32SInt => DXGI_FORMAT_R32G32B32A32_SINT,
            Format.R32G32B32Float => DXGI_FORMAT_R32G32B32_FLOAT,
            Format.R32G32B32UInt => DXGI_FORMAT_R32G32B32_UINT,
            Format.R32G32B32SInt => DXGI_FORMAT_R32G32B32_SINT,
            Format.R10G10B10A2UNorm => DXGI_FORMAT_R10G10B10A2_UNORM,
            Format.R10G10B10A2UInt => DXGI_FORMAT_R10G10B10A2_UINT,
            Format.R11G11B10Float => DXGI_FORMAT_R11G11B10_FLOAT,
            Format.D16UNorm => DXGI_FORMAT_D16_UNORM,
            Format.D24UNormS8UInt => DXGI_FORMAT_D24_UNORM_S8_UINT,
            Format.D32Float => DXGI_FORMAT_D32_FLOAT,
            Format.BC1UNorm => DXGI_FORMAT_BC1_UNORM,
            Format.BC1UNormSRGB => DXGI_FORMAT_BC1_UNORM_SRGB,
            Format.BC2UNorm => DXGI_FORMAT_BC2_UNORM,
            Format.BC2UNormSRGB => DXGI_FORMAT_BC2_UNORM_SRGB,
            Format.BC3UNorm => DXGI_FORMAT_BC3_UNORM,
            Format.BC3UNormSRGB => DXGI_FORMAT_BC3_UNORM_SRGB,
            Format.BC4UNorm => DXGI_FORMAT_BC4_UNORM,
            Format.BC4SNorm => DXGI_FORMAT_BC4_SNORM,
            Format.BC5UNorm => DXGI_FORMAT_BC5_UNORM,
            Format.BC5SNorm => DXGI_FORMAT_BC5_SNORM,
            Format.BC6HUF16 => DXGI_FORMAT_BC6H_UF16,
            Format.BC6HSF16 => DXGI_FORMAT_BC6H_SF16,
            Format.BC7UNorm => DXGI_FORMAT_BC7_UNORM,
            Format.BC7UNormSRGB => DXGI_FORMAT_BC7_UNORM_SRGB,
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }
}