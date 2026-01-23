import { useState, useCallback } from 'react';
import {
  ReactFlow,
  applyNodeChanges,
  applyEdgeChanges,
  addEdge,
  type Node,
  type Edge,
  type NodeChange,
  type EdgeChange,
  Background,
  Controls,
  Panel,
  type NodeTypes,
  useReactFlow,
} from '@xyflow/react';
import '@xyflow/react/dist/style.css';
import { StoryNode } from './components/StoryNode';

const initialNodes: Node[] = [
  {
    id: 'n1',
    type: 'storyNode',
    position: { x: 200, y: 0 },
    data: { description: "The story begins" },
  },
];

const initialEdges: Edge[] = [];

export default function App() {
  const [nodes, setNodes] = useState<Node[]>(initialNodes);
  const [edges, setEdges] = useState<Edge[]>(initialEdges);

  const reactFlow = useReactFlow();


  const onNodesChange = useCallback(
    (changes: NodeChange[]) =>
      setNodes((nds) => applyNodeChanges(changes, nds)),
    []
  );

  const onEdgesChange = useCallback(
    (changes: EdgeChange[]) =>
      setEdges((eds) => applyEdgeChanges(changes, eds)),
    []
  );

  const onConnect = useCallback(
    (params: any) =>
      setEdges((eds) => addEdge(params, eds)),
    []
  );

  function addStoryNode() {
    const id = `story-${nodes.length + 1}`;

    const centerX = window.innerWidth / 2;
    const centerY = window.innerHeight / 2;

    const pos = reactFlow.screenToFlowPosition({x: centerX, y: centerY})

    setNodes((nds) => [
      ...nds,
      {
        id,
        type: 'storyNode',
        position: pos,
        data: { description: "..." },
      },
    ]);
  };

  const nodeTypes: NodeTypes = {
    storyNode: StoryNode
  }

  return (
    <div className="w-screen h-screen text-black">
        <ReactFlow
          nodes={nodes}
          edges={edges}
          nodeTypes={nodeTypes}
          onNodesChange={onNodesChange}
          onEdgesChange={onEdgesChange}
          onConnect={onConnect}
          fitView
        >
          <Background />
          <Controls />

          <Panel position="bottom-center">
            <button
              onClick={addStoryNode}
              className="rounded bg-black px-3 py-1 text-white"
            >
              Add node
            </button>
          </Panel>
        </ReactFlow>
    </div>
  );
}
