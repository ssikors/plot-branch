import {
  ReactFlow,

  Background,
  Controls,
  Panel,
  type NodeTypes,
  useReactFlow,
} from '@xyflow/react';
import '@xyflow/react/dist/style.css';
import { StoryNode } from '../components/nodes/StoryNode';
import { useParams } from "react-router-dom";
import { useShallow } from 'zustand/shallow';
import { useEffect } from 'react';
import axios from 'axios';
import { sendFlowToApi } from '../api/plotFlowApi';
import useStore from '../store/store';
import EditorToolbar from '../components/EditorToolbar';

const selector = (state: any) => ({
  nodes: state.nodes,
  edges: state.edges,
  onNodesChange: state.onNodesChange,
  onEdgesChange: state.onEdgesChange,
  onConnect: state.onConnect,
});


export default function PlotEditorPage() {
  const { nodes, edges, onNodesChange, onEdgesChange, onConnect } = useStore(
    useShallow(selector),
  );

  const { flowId } = useParams();

  const { addStoryNode, setFlowId, setNodes, setEdges } = useStore();

  useEffect(() => {
  if (!flowId) return;

  const loadFlow = async () => {

    try {
      const nodesResponse = await axios.get('/api/Node', {
        params: { plotFlowId: flowId }
      });

      const edgesResponse = await axios.get('/api/Edge', {
        params: { plotFlowId: flowId }
      });

      const loadedNodes = nodesResponse.data.map((n: any) => ({
        id: n.id.toString(),
        type: n.type,
        position: {
          x: n.positionX,
          y: n.positionY
        },
        data: n.data
      }));

      const loadedEdges = edgesResponse.data.map((e: any) => ({
        id: e.id.toString(),
        source: e.source.toString(),
        target: e.target.toString(),
        sourceHandle: e.sourceHandle
      }));

      setNodes(loadedNodes);
      setEdges(loadedEdges);

    } catch (err) {
      console.error("Failed to load flow", err);
    }
  };

  loadFlow();

}, [flowId]);


  function handleAddStoryNode() {
    const centerX = window.innerWidth / 2;
    const centerY = window.innerHeight / 2;

    const pos = reactFlow.screenToFlowPosition({
      x: centerX,
      y: centerY
    });

    addStoryNode(pos);
  }


  const healthCheck = async () => {

    try {
      var data = await axios.get('/api/Graph/health').then(r => r.data)
      console.log("Health check success, API connected:", data)
    } catch (e) {
      console.error("Health check failed, cant reach API", e)
    }
  }

  useEffect(() => {
    if (flowId) setFlowId(flowId);
  }, [flowId]);


  useEffect(() => {
    healthCheck();
  }, []);

  const reactFlow = useReactFlow();

  const onSave = async () => {
    const flow = reactFlow.toObject();

    console.log(JSON.stringify(flow));

    try {
      await sendFlowToApi(flow);
      console.log("Flow saved successfully");
    } catch (err) {
      console.error("Failed to save flow");
    }
  };



  const nodeTypes: NodeTypes = {
    storyNode: StoryNode,
  }

  return (
    <div className="w-screen h-screen text-black relative">
      
      <EditorToolbar />

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
            onClick={handleAddStoryNode}
            className="rounded bg-black px-3 py-1 text-white"
          >
            Add node
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
