import type { Node, Edge } from "@xyflow/react";

export interface FlowStore {
  flowId: string;
  nodes: Node[];
  edges: Edge[];

  setNodes: (nodes: Node[]) => void;
  setEdges: (edges: Edge[]) => void;


  setFlowId: (id: string) => void;

  addStoryNode: (position: { x: number; y: number }) => Promise<void>;

  onNodesChange: (changes: any) => Promise<void>;
  onEdgesChange: (changes: any) => Promise<void>;
  onConnect: (connection: any) => Promise<void>;
}
