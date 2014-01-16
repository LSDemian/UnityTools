using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor class MoveCollider for MoveCollider component
/// </summary>
[CustomEditor(typeof(MoveCollider))]
public class MoveColliderEditor : Editor
{
    void OnSceneGUI()
    {
        MoveCollider target = this.target as MoveCollider;
        if (target.collider2D) {
            Handles.color = Color.white;
            if (target.collider2D is PolygonCollider2D) {
                //if objects contains PolygonCollider
                PolygonCollider2D collider = target.collider2D as PolygonCollider2D;
                //iterate through all vertices arrays in polygon collider
                for (int i = 0; i < collider.pathCount; i++) {
                    Vector2[] vertices = collider.GetPath(i);
                    if (vertices.Length > 0) {
                        //place handle to first vertex position
                        Vector2 move = vertices[0] - VectorEx.Vec3ToVec2( 
                            Handles.FreeMoveHandle(vertices[0] + VectorEx.Vec3ToVec2(target.transform.position), 
                            Quaternion.identity, 
                            0.1f, 
                            Vector3.zero, 
                            Handles.SphereCap) );
                        for (int j = 0; j < vertices.Length; j++) {
                            vertices[j] -= move;
                        }
                        collider.SetPath(i, vertices);
                    }
                    
                }

            } else if (target.collider2D is CircleCollider2D ) {
                //if objects contains CircleCollider
                CircleCollider2D collider = target.collider2D as CircleCollider2D;
                //Draw handle and assing it's value to collider center
                Vector2 move = VectorEx.Vec3ToVec2(
                            Handles.FreeMoveHandle(collider.center + VectorEx.Vec3ToVec2(target.transform.position),
                            Quaternion.identity,
                            0.1f,
                            Vector3.zero,
                            Handles.SphereCap));
                if (move != collider.center + VectorEx.Vec3ToVec2(target.transform.position)) {
                    collider.center = move - VectorEx.Vec3ToVec2(target.transform.position);
                }
            } else if (target.collider2D is BoxCollider2D) {
                //if objects contains BoxCollider
                BoxCollider2D collider = target.collider2D as BoxCollider2D;
                //Draw handle and assing it's value to collider center
                Vector2 move = VectorEx.Vec3ToVec2(
                            Handles.FreeMoveHandle(collider.center + VectorEx.Vec3ToVec2(target.transform.position),
                            Quaternion.identity,
                            0.1f,
                            Vector3.zero,
                            Handles.SphereCap));
                if (move != collider.center + VectorEx.Vec3ToVec2(target.transform.position) ) {
                    collider.center = move - VectorEx.Vec3ToVec2(target.transform.position);
                }
            }
        }
    }
}
