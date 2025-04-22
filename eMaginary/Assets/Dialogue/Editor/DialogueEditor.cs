using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.Drawing;
//using static Codice.Client.ChangeTrackerService.Win32Api;
//using Codice.CM.Client.Differences.Graphic;
using NUnit.Framework;
using System.Collections.Generic;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;

        [NonSerialized] GUIStyle nodeStyle;

        [NonSerialized] GUIStyle playerNodeStyle;

        [NonSerialized] DialogueNode draggingNode = null;

        [NonSerialized] Vector2 dragOffset;

        [NonSerialized] DialogueNode creatingNode = null;

        [NonSerialized] DialogueNode deletingNode = null;

        [NonSerialized] DialogueNode linkingParentNode = null;

        Vector2 scrollPosition;

        [NonSerialized] bool draggingCanvas = false;

        [NonSerialized] Vector2 draggingCanvasOffset;

        const float canvasSize = 6000;
        const float backgroundSize = 50;

        [UnityEditor.MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null) 
            {
                ShowEditorWindow();
                return true;
            }           
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;// Adding the function to the list of events, not calling it!
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.padding = new RectOffset(10, 10, 10, 10);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            nodeStyle.normal.textColor = UnityEngine.Color.white;

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerNodeStyle.padding = new RectOffset(10, 10, 10, 10);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
            playerNodeStyle.normal.textColor = UnityEngine.Color.white;

        }
        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null) {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No dialogue selected");
            }
            else
            {
                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);

                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect textCoords = new Rect(0, 0, canvasSize/50, canvasSize / 50);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, textCoords);


                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);                    
                }
                if(creatingNode !=  null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }

                EditorGUILayout.EndScrollView();
                if (deletingNode != null)
                {
                    Undo.RecordObject(selectedDialogue, "Deleted Dialogue Node");
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }               
            }  
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingNode != null)
                {
                    dragOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode!= null)
            {
                Undo.RecordObject(selectedDialogue, "Undo Move Node");
                draggingNode.SetPosition(Event.current.mousePosition + dragOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }

            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }

            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                GUI.changed = false;
            }
        }        

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.IsPlayerSpeaking())
            {
                style = playerNodeStyle;
            }
            GUILayout.BeginArea(node.GetRect(), style);

            node.SetDialoguePart(GUILayout.TextArea(node.GetDialoguePart(), 256, GUILayout.MaxHeight(50)));

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }
            DrawLinkButtons(node);
            if (GUILayout.Button("-"))
            {
                deletingNode = node;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    linkingParentNode = node;
                }
            }
            else
            {
                if (GUILayout.Button("child"))
                {             
                    //Selbstreferenz ausschließen
                    if (linkingParentNode.name != node.name)
                    {
                        //Ausschließen, dass Knoten bereits als Kindsknoten angelegt ist
                        if (!linkingParentNode.GetChildNodes().Contains(node.name))
                        {
                           linkingParentNode.AddChildNode(node.name);
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("Aktion nicht ausgeführt", "Dieser Knoten ist bereits als Kindsknoten angelegt.", "Schließen");
                        }
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Aktion nicht ausgeführt", "Eine Selbstreferenz ist nicht möglich", "Schließen");
                        GUILayout.Button("link");
                    }
                    linkingParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            int dirNode = 1;
           
            float startOffsetX = (node.GetRect().xMax - node.GetRect().xMin) / 2;
            float startOffsetY = (node.GetRect().yMax - node.GetRect().yMin) / 2;

            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                if (childNode.GetRect().center.x < node.GetRect().center.x)
                {
                    dirNode = -1;
                }
                Vector3 startPosition = node.GetRect().center + dirNode * new Vector2(startOffsetX, 0);
                Vector3 endPosition = childNode.GetRect().center - dirNode * new Vector2(startOffsetY, 0);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition, 
                    startPosition + controlPointOffset, endPosition - controlPointOffset, 
                    UnityEngine.Color.white, null, 4f);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                 if (node.GetRect().Contains(point))
                {
                    foundNode = node;
                }               
            }
            return foundNode;
        }
    }

}
