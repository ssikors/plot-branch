
import { Handle, Position, type NodeProps, type Node } from '@xyflow/react';


type StartNode = Node;

export function StartNode({ data }: NodeProps<StartNode>) {
  return (
    <div className="rounded-full border bg-green-300 p-4 pt-5 shadow">
      <div className="font-bold mb-1 text-center">Start</div>
      <Handle type="source" position={Position.Bottom} />
    </div>
  );
}
