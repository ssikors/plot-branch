import { Routes, Route } from "react-router-dom";
import PlotFlowListPage from "./pages/PlotFlowListPage";
import PlotEditorPage from "./pages/PlotEditorPage";
import { ReactFlowProvider } from "@xyflow/react";

function App() {
  return (
    <Routes>
      <Route path="/" element={<PlotFlowListPage />} />
      <Route path="/plots/:flowId" element={<ReactFlowProvider><PlotEditorPage /></ReactFlowProvider>} />
    </Routes>
  );
}

export default App;
