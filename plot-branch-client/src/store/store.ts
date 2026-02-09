import { create } from "zustand";
import {
  addEdge,
  applyNodeChanges,
  applyEdgeChanges,
  type EdgeChange,
  type NodeChange,
} from "@xyflow/react";

import { createStoryNode, updateNode, createEdge, updateEdge } from "../api/plotFlowApi";

import type { FlowStore } from "./types";
import { createCharacter, getCharacters } from "../api/characterApi";

const useStore = create<FlowStore>((set, get) => ({
  flowId: "",

  nodes: [],
  edges: [],
  characters: [],

  setFlowId: async (id) => {
    set({ flowId: id });

    await get().loadCharacters();
  },


  setNodes: (nodes) => set({ nodes }),
  setEdges: (edges) => set({ edges }),

  setCharacters: (characters) => set({ characters }),

  loadCharacters: async () => {
    const flowId = get().flowId;
    if (!flowId) return;

    const characters = await getCharacters(flowId);
    set({ characters });
  },

  addCharacter: async (name) => {
    const flowId = get().flowId;
    if (!flowId || !name.trim()) return;

    const newCharacter = await createCharacter(flowId, name);

    set({
      characters: [...get().characters, newCharacter]
    });
  },

  addStoryNode: async (position) => {
    const flowId = get().flowId;

    const newNode = await createStoryNode(flowId, position);

    set({
      nodes: [
        ...get().nodes,
        {
          id: newNode.id,
          type: "storyNode",
          position: {
            x: newNode.positionX,
            y: newNode.positionY
          },
          data: newNode.data
        }
      ]
    });
  },


  onNodesChange: async (changes) => {
    const updatedNodes = applyNodeChanges(changes, get().nodes);

    set({ nodes: updatedNodes });

    for (const change of changes) {
      if (change.type == "position") {
        if (change.dragging) return;
      }

      const node = updatedNodes.find(n => n.id === change.id);
      if (node) await updateNode(node);
    }
  },


  onConnect: async (connection) => {
    const flowId = get().flowId;

    const newEdge = await createEdge(flowId, connection);

    set({
      edges: addEdge(newEdge, get().edges)
    });
  },

  onEdgesChange: async (changes) => {
    const updatedEdges = applyEdgeChanges(changes, get().edges);

    set({ edges: updatedEdges });

    for (const change of changes) {
      const edge = updatedEdges.find(e => e.id === change.id);
      if (edge) await updateEdge(edge);
    }
  }
}));

export default useStore;
