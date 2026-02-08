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