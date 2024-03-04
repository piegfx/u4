namespace u4.Render;

public class Material
{
    public Texture Albedo;
    public Texture Normal;
    public Texture Metallic;
    public Texture Roughness;
    public Texture Occlusion;
    public Texture Emissive;

    public Material(Texture albedo, Texture normal, Texture metallic, Texture roughness, Texture occlusion, Texture emissive)
    {
        Albedo = albedo;
        Normal = normal;
        Metallic = metallic;
        Roughness = roughness;
        Occlusion = occlusion;
        Emissive = emissive;
    }

    public Material(Texture albedo) : 
        this(albedo, Texture.EmptyNormal, Texture.Black, Texture.White, Texture.White, Texture.Black) { }

    public Material() : this(Texture.White) { }
}