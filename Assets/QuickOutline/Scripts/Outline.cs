// using System;
// using System.Collections.Generic;
// using System.Linq;
// using DefaultNamespace;
// using UnityEngine;
//
// [DisallowMultipleComponent]
// [RequireComponent(typeof(Hightlight))]
// public class Outline : MonoBehaviour
// {
//   private static HashSet<Mesh> registeredMeshes = new HashSet<Mesh>();
//
//   [SerializeField] private ColorOptions selectedColorOption;
//
//   public enum ColorOptions
//   {
//     PreSet_0,
//     PreSet_1
//   }
//
//   [ColorUsage(true, true)] [SerializeField]
//   private Color[] predefinedColors = new Color[]
//   {
//     new Color(0.990566f, 0.8180472f, 0f),
//     new Color(0f, 0.8867924f, 0.8509698f),
//
//   };
//
//   [Serializable]
//   private class ListVector3
//   {
//     public List<Vector3> data;
//   }
//
//   [SerializeField] private Color outlineColor = Color.white;
//
//   [SerializeField, Range(0f, 10f)] private float outlineWidth = 6f;
//   
//   [SerializeField]private bool precomputeOutline;
//
//   [SerializeField, HideInInspector] private List<Mesh> bakeKeys = new List<Mesh>();
//
//   [SerializeField, HideInInspector] private List<ListVector3> bakeValues = new List<ListVector3>();
//
//   private Renderer[] renderers;
//   private Material outlineMaskMaterial;
//   private Material outlineFillMaterial;
//
//   private bool needsUpdate;
//
//   private Color GetColorFromEnum(ColorOptions colorOption)
//   {
//     return predefinedColors[(int)colorOption];
//   }
//
//   void Awake()
//   {
//     outlineColor = GetColorFromEnum(selectedColorOption);
//     renderers = GetComponentsInChildren<Renderer>();
//     outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
//     outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));
//     outlineMaskMaterial.name = "OutlineMask (Instance)";
//     outlineFillMaterial.name = "OutlineFill (Instance)";
//     LoadSmoothNormals();
//     needsUpdate = true;
//   }
//
//   void OnEnable()
//   {
//     foreach (var renderer in renderers)
//     {
//       var materials = renderer.sharedMaterials.ToList();
//       materials.Add(outlineMaskMaterial);
//       materials.Add(outlineFillMaterial);
//       renderer.materials = materials.ToArray();
//     }
//   }
//
//   void OnValidate()
//   {
//     needsUpdate = true;
//
//     if (!precomputeOutline && bakeKeys.Count != 0 || bakeKeys.Count != bakeValues.Count)
//     {
//       bakeKeys.Clear();
//       bakeValues.Clear();
//     }
//     
//     if (precomputeOutline && bakeKeys.Count == 0)
//     {
//       Bake();
//     }
//   }
//
//   void Update()
//   {
//     if (needsUpdate)
//     {
//       needsUpdate = false;
//       UpdateMaterialProperties();
//     }
//   }
//
//   void OnDisable()
//   {
//     foreach (var renderer in renderers)
//     {
//       var materials = renderer.sharedMaterials.ToList();
//       materials.Remove(outlineMaskMaterial);
//       materials.Remove(outlineFillMaterial);
//       renderer.materials = materials.ToArray();
//     }
//   }
//
//   void OnDestroy()
//   {
//     Destroy(outlineMaskMaterial);
//     Destroy(outlineFillMaterial);
//   }
//
//   void Bake()
//   {
//
//     var bakedMeshes = new HashSet<Mesh>();
//
//     foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
//     {
//       if (!bakedMeshes.Add(meshFilter.sharedMesh))
//       {
//         continue;
//       }
//       
//       var smoothNormals = SmoothNormals(meshFilter.sharedMesh);
//       bakeKeys.Add(meshFilter.sharedMesh);
//       bakeValues.Add(new ListVector3() { data = smoothNormals });
//     }
//   }
//
//   void LoadSmoothNormals()
//   {
//
//     foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
//     {
//       if (!registeredMeshes.Add(meshFilter.sharedMesh))
//       {
//         continue;
//       }
//
//       var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
//       var smoothNormals = (index >= 0) ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);
//       meshFilter.sharedMesh.SetUVs(3, smoothNormals);
//       var renderer = meshFilter.GetComponent<Renderer>();
//
//       if (renderer != null)
//       {
//         CombineSubmeshes(meshFilter.sharedMesh, renderer.sharedMaterials);
//       }
//     }
//
//     foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
//     {
//       if (!registeredMeshes.Add(skinnedMeshRenderer.sharedMesh))
//       {
//         continue;
//       }
//
//       skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];
//       CombineSubmeshes(skinnedMeshRenderer.sharedMesh, skinnedMeshRenderer.sharedMaterials);
//     }
//   }
//
//   List<Vector3> SmoothNormals(Mesh mesh)
//   {
//     var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
//       .GroupBy(pair => pair.Key);
//
//     var smoothNormals = new List<Vector3>(mesh.normals);
//     foreach (var group in groups)
//     {
//       if (group.Count() == 1)
//       {
//         continue;
//       }
//
//       var smoothNormal = Vector3.zero;
//       foreach (var pair in group)
//       {
//         smoothNormal += smoothNormals[pair.Value];
//       }
//
//       smoothNormal.Normalize();
//       foreach (var pair in group)
//       {
//         smoothNormals[pair.Value] = smoothNormal;
//       }
//     }
//     return smoothNormals;
//   }
//
//   void CombineSubmeshes(Mesh mesh, Material[] materials)
//   {
//     if (mesh.subMeshCount == 1)
//     {
//       return;
//     }
//
//     if (mesh.subMeshCount > materials.Length)
//     {
//       return;
//     }
//
//     mesh.subMeshCount++;
//     mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
//   }
//
//   void UpdateMaterialProperties()
//   {
//     outlineFillMaterial.SetColor("_OutlineColor", outlineColor);
//     outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
//     outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
//     outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
//   }
// }
//
