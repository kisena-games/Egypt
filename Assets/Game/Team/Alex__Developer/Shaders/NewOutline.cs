using UnityEditor;
using UnityEngine;

public class NewOutline : MonoBehaviour
{
    [SerializeField] private Material _material;
    public bool Enabled = false;

    [Header("Shader properties")]
    public Texture2D _Texture2D;
    public bool _USE_TEXTURE = true;
    public Color _Color=Color.yellow;
    [Range(0f, 20f)]
    public float _TextureVolume=10f;
    [Range(0f, 6f)]
    public float _Angle = 2f;
    [Range(0f, 10f)]
    public float _speed = 1f;
    [Range(0f, 100f)]
    public float _Emission = 20f;
    [Range(0f, 2f)]
    public float _Period =0.5f;

    private Material outlineMaterial;
    private Material originalMaterial;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component missing!");
            enabled = false;
            return;
        }

        
        outlineMaterial = _material;

        if (outlineMaterial == null)
        {
            Debug.LogError("Material not found in Assets/Game/Team/Alex_Developer/Outline");
            enabled = false;
            return;
        }

        // —охран€ем оригинальный материал дл€ восстановлени€
        if (meshRenderer.materials.Length > 0)
        {
            originalMaterial = meshRenderer.materials[0];
        }
    }

    void Update()
    {
        if (Enabled)
        {
            ApplyOutlineMaterial();
            UpdateShaderProperties();
        }
        else
        {
            RemoveOutlineMaterial();
        }
    }

    void ApplyOutlineMaterial()
    {
        // ѕровер€ем, есть ли уже материал outline у renderer
        var materials = meshRenderer.materials;

        bool hasOutline = false;
        foreach (var mat in materials)
        {
            if (mat.name.Contains(outlineMaterial.name))
            {
                hasOutline = true;
                break;
            }
        }

        if (!hasOutline)
        {
            var newMats = new Material[materials.Length + 1];
            materials.CopyTo(newMats, 0);
            newMats[newMats.Length - 1] = new Material(outlineMaterial);
            meshRenderer.materials = newMats;
        }
    }

    void RemoveOutlineMaterial()
    {
        var materials = meshRenderer.materials;
        int length = materials.Length;
        if (length == 0) return;

        // —оздаем новый массив без outline материала
        var newMatsList = new System.Collections.Generic.List<Material>();
        foreach (var mat in materials)
        {
            if (!mat.name.Contains(outlineMaterial.name))
            {
                newMatsList.Add(mat);
            }
        }

        if (newMatsList.Count != length)
        {
            meshRenderer.materials = newMatsList.ToArray();
        }
    }

    void UpdateShaderProperties()
    {
        var materials = meshRenderer.materials;
        foreach (var mat in materials)
        {
            if (mat.name.Contains(outlineMaterial.name))
            {
                mat.SetTexture("_Texture2D", _Texture2D);
                mat.SetFloat("_USE_TEXTURE?", _USE_TEXTURE ? 1f : 0f);
                mat.SetColor("_Color", _Color);
                mat.SetFloat("_TextureVolume", _TextureVolume);
                mat.SetFloat("_Angle", _Angle);
                mat.SetFloat("_speed", _speed);
                mat.SetFloat("_Emission", _Emission);
                mat.SetFloat("_Period", _Period);
            }
        }
    }
}
