
import {
  ReactFlow,

  Background,
  Controls,
  Panel,
  type NodeTypes,
  useReactFlow,
} from '@xyflow/react';
import '@xyflow/react/dist/style.css';
import { StoryNode } from './components/nodes/StoryNode';

import useStore from './store/store';
import { useShallow } from 'zustand/shallow';
import { useEffect } from 'react';
import axios from 'axios';
import { StartNode } from './components/nodes/StartNode';
import { ChoiceNode } from './components/nodes/ChoiceNode';

const selector = (state: any) => ({
  nodes: state.nodes,
  edges: state.edges,
  onNodesChange: state.onNodesChange,
  onEdgesChange: state.onEdgesChange,
  onConnect: state.onConnect,
  setNodes: state.setNodes
});


export default function App() {
  const { nodes, edges, onNodesChange, onEdgesChange, onConnect, setNodes } = useStore(
    useShallow(selector),
  );

  const healthCheck = async () =>  {
    
    try {
      var data = await axios.get('/api/Graph/health').then(r => r.data)
      console.log("Health check success, API connected:", data)
    } catch (e) {
      console.error("Health check failed, cant reach API", e)
    }
  }


  useEffect(() => {
    healthCheck();
  }, []);

  const reactFlow = useReactFlow();

  const onSave = () => {
    const flow = reactFlow.toObject()
    console.log(JSON.stringify(flow))
  }

  function addChoiceNode() {
    const id = `choice-${nodes.length + 1}`;

    const centerX = window.innerWidth / 2;
    const centerY = window.innerHeight / 2;

    const pos = reactFlow.screenToFlowPosition({ x: centerX, y: centerY })

    setNodes([...nodes,
    {
      id,
      type: 'choiceNode',
      position: pos,
      data: {},
    }]
    );
  };

  function addStoryNode() {
    const id = `story-${nodes.length + 1}`;

    const centerX = window.innerWidth / 2;
    const centerY = window.innerHeight / 2;

    const pos = reactFlow.screenToFlowPosition({ x: centerX, y: centerY })

    setNodes([...nodes,
    {
      id,
      type: 'storyNode',
      position: pos,
      data: { description: "..." },
    }]
    );
  };
  const nodeTypes: NodeTypes = {
    storyNode: StoryNode,
    startNode: StartNode,
    choiceNode: ChoiceNode
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
          <button
            onClick={addChoiceNode}
            className="rounded bg-black px-3 py-1 text-white"
          >
            Add choice
          </button>
          <button
            onClick={onSave}
            className="rounded bg-black px-3 py-1 text-white"
          >
            Save flow
          </button>
        </Panel>
      </ReactFlow>
    </div>
  );
}
