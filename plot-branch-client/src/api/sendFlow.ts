import type { ReactFlowJsonObject } from "@xyflow/react";
import axios from "axios";

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
