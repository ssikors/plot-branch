import { useEffect, useState } from "react";
import { getPlotFlows, createPlotFlow } from "../api/plotFlowApi";
import { useNavigate } from "react-router-dom";
import type { PlotFlowListItem } from "../types/PlotFlow";

export default function PlotFlowListPage() {
  const [flows, setFlows] = useState<PlotFlowListItem[]>([]);
  const [newName, setNewName] = useState("");

  const navigate = useNavigate();

  const loadFlows = async () => {
    const data = await getPlotFlows();
    setFlows(data);
  };

  useEffect(() => {
    loadFlows();
  }, []);

  const handleCreate = async () => {
    if (!newName.trim()) return;

    await createPlotFlow(newName);
    setNewName("");

    await loadFlows();
  };

  const openFlow = (flowId: string) => {
    navigate(`/plots/${flowId}`);
  };

  return (
    <div style={{ padding: 20 }}>
      <h2>Plot Flows</h2>

      <div style={{ marginBottom: 20 }}>
        <input
          placeholder="Flow name"
          value={newName}
          onChange={(e) => setNewName(e.target.value)}
        />

        <button onClick={handleCreate}>
          Create Flow
        </button>
      </div>

      <ul>
        {flows.map((flow) => (
          <li key={flow.id}>
            <button onClick={() => openFlow(flow.id)}>
              {flow.name}
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
