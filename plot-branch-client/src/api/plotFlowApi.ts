import axios from "axios";
import type { Node, ReactFlowJsonObject } from "@xyflow/react";


export const getPlotFlows = async () => {
  const res = await axios.get("/api/Graph");
  return res.data;
};

export const createPlotFlow = async (name: string) => {
  const res = await axios.post("/api/Graph", { name });
  return res.data;
};


export const sendFlowToApi = async (flow : ReactFlowJsonObject) => {
  try {
    const response = await axios.post("/api/Graph/save", flow, {
      headers: {
        "Content-Type": "application/json"
      }
    });

    return response.data;
  } catch (error) {
    console.error("Error sending flow:", error);
    throw error;
  }
};


export const createStoryNode = async (
  flowId: string,
  position: { x: number; y: number }
) => {

  console.log("Sending create node request...")

  const res = await axios.post(`/api/Node`, {
    flowId,
    type: "storyNode",
    positionX: position.x,
    positionY: position.y,
    data: { description: "..." }
  });

  console.log(res)

  return res.data;
};

export const updateNode = async (node: Node) => {
  console.log("Sending update node request...")

  const res = await axios.put(`/api/Node/${node.id}`, {
    id: node.id,
    type: node.type,
    positionX: node.position.x,
    positionY: node.position.y,
    data: { description: node.data?.description }
  });

  console.log(res)
};



export const createEdge = async (flowId: string, connection: any) => {
  const res = await axios.post(`/api/Edge`, {
    flowId,
    ...connection
  });

  return res.data;
};

export const updateEdge = async (edge: any) => {
  await axios.put(`/api/Edge/${edge.id}`, edge);
};