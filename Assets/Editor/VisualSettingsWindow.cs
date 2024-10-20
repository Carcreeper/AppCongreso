using UnityEditor;
using UnityEngine;

public class VisualSettingsWindow : EditorWindow
{
    // Booleanos globales guardados en preferencias
    private static readonly string PREF_VER_CIRCULOS = "VisualSettings_VerCirculos";
    private static readonly string PREF_VER_RUTAS = "VisualSettings_VerRutas";
    private static readonly string PREF_VER_VECINOS = "VisualSettings_VerVecinos";

    // Variables para los booleanos
    public static bool verCirculos
    {
        get { return EditorPrefs.GetBool(PREF_VER_CIRCULOS, true); }
        set { EditorPrefs.SetBool(PREF_VER_CIRCULOS, value); }
    }

    public static bool verRutas
    {
        get { return EditorPrefs.GetBool(PREF_VER_RUTAS, true); }
        set { EditorPrefs.SetBool(PREF_VER_RUTAS, value); }
    }

    public static bool verVecinos
    {
        get { return EditorPrefs.GetBool(PREF_VER_VECINOS, true); }
        set { EditorPrefs.SetBool(PREF_VER_VECINOS, value); }
    }

    // Campos para seleccionar los nodos inicial y final
    public NodoWaze nodoInicial;
    public NodoWaze nodoFinal;

    // Crear la ventana del Editor
    [MenuItem("Morion/Navmesh pro UdeM Genial")]
    public static void ShowWindow()
    {
        GetWindow<VisualSettingsWindow>("Morion Navmesh UdeM");
    }

    // Dibujar la GUI en la ventana
    private void OnGUI()
    {
        GUILayout.Label("Configuración de visualización", EditorStyles.boldLabel);

        // Mostrar las opciones de los booleanos
        verCirculos = EditorGUILayout.Toggle("Ver Círculos", verCirculos);
        verRutas = EditorGUILayout.Toggle("Ver Rutas", verRutas);
        verVecinos = EditorGUILayout.Toggle("Ver Vecinos", verVecinos);

        // Botón para buscar vecinos en todos los NodoWaze
        if (GUILayout.Button("Buscar Vecinos en todos los NodoWaze"))
        {
            BuscarVecinosEnTodosLosNodos();
            SceneView.RepaintAll();
        }

        GUILayout.Label("Configuración de la ruta", EditorStyles.boldLabel);
        // Campos para seleccionar los nodos inicial y final
        nodoInicial = (NodoWaze)EditorGUILayout.ObjectField("Nodo Inicial", nodoInicial, typeof(NodoWaze), true);
        nodoFinal = (NodoWaze)EditorGUILayout.ObjectField("Nodo Final", nodoFinal, typeof(NodoWaze), true);


        // Botón para trazar ruta entre nodoInicial y nodoFinal
        if (GUILayout.Button("Trazar Ruta"))
        {
            TrazarRutaEntreNodos();
            SceneView.RepaintAll();
        }
    }

    // Método para buscar todos los NodoWaze y llamar a BuscarVecinos en cada uno
    private void BuscarVecinosEnTodosLosNodos()
    {
        NodoWaze[] nodos = GameObject.FindObjectsOfType<NodoWaze>();
        foreach (NodoWaze nodo in nodos)
        {
            nodo.BuscarVecinos();
        }

        // Refrescar la vista de la escena para visualizar los cambios
        SceneView.RepaintAll();
    }

    // Método para trazar la ruta entre nodoInicial y nodoFinal usando MorionWaze
    private void TrazarRutaEntreNodos()
    {
        if (nodoInicial == null || nodoFinal == null)
        {
            Debug.LogWarning("Debe asignar ambos nodos: Nodo Inicial y Nodo Final.");
            return;
        }

        // Asignar los nodos inicial y final al singleton de MorionWaze
        MorionWaze.singleton.nodoInicial = nodoInicial;
        MorionWaze.singleton.nodoFinal = nodoFinal;

        // Llamar al método TrazarRuta del singleton
        MorionWaze.singleton.TrazarRuta();
    }
}
