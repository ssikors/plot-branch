import axios from "axios";
import type { ReactFlowJsonObject } from "@xyflow/react";

const baseUrl = "/api/Graph";

export const getPlotFlows = async () => {
  const res = await axios.get(baseUrl);
  return res.data;
};

export const createPlotFlow = async (name: string) => {
  const res = await axios.post(baseUrl, { name });
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
  const res = await axios.post(`/api/Node`, {
    flowId,
    type: "storyNode",
    positionX: position.x,
    positionY: position.y,
    data: { description: "..." }
  });

  return res.data;
};

export const updateNode = async (node: any) => {
  await axios.put(`/api/Node/${node.id}`, node);
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