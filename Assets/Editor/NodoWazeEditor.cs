
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodoWaze))]
[CanEditMultipleObjects]
public class NodoWazeEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("BuscarVecinos"))
		{
			NodoWaze nodo = (NodoWaze)target;
			nodo.BuscarVecinos();
		}

		//if (GUILayout.Button("Ver Radio para vecinos"))
		//{
		//	NodoWaze.verRadio = !NodoWaze.verRadio;
		//	SceneView.RepaintAll();
		//}
		//if (GUILayout.Button("Ver vecinos"))
		//{
		//	NodoWaze.verVecinos = !NodoWaze.verVecinos;
		//	SceneView.RepaintAll();
		//}
		//if (GUILayout.Button("Ver Radio Ruta"))
		//{
		//	NodoWaze.verRuta = !NodoWaze.verRuta;
		//	SceneView.RepaintAll();
		//}
	}
}
