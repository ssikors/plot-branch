import type { Node, Edge } from "@xyflow/react";

export type Character = {
  id: string;
  name: string;
};


export interface FlowStore {
  flowId: string;
  nodes: Node[];
  edges: Edge[];
  characters: Character[];

  setNodes: (nodes: Node[]) => void;
  setEdges: (edges: Edge[]) => void;
  
  setCharacters: (characters: Character[]) => void;
  
  setFlowId: (id: string) => void;

  loadCharacters: () => Promise<void>;
  addCharacter: (name: string) => Promise<void>;

  addStoryNode: (position: { x: number; y: number }) => Promise<void>;

  onNodesChange: (changes: any) => Promise<void>;
  onEdgesChange: (changes: any) => Promise<void>;
  onConnect: (connection: any) => Promise<void>;
}
