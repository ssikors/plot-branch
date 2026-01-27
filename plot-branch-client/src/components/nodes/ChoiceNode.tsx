
import { Handle, Position, type NodeProps, type Node } from '@xyflow/react';


type ChoiceNode = Node;

export function ChoiceNode({ data }: NodeProps<ChoiceNode>) {
  return (
    <div className="rounded border bg-orange-300 p-4 pt-5 shadow">
      <div className="font-bold mb-1 text-center">Choice</div>
      <Handle type="target" position={Position.Top} />
      <Handle type="source" id="left"  position={Position.Left} />
      <Handle type="source" id="right"  position={Position.Right} />
    </div>
  );
}
