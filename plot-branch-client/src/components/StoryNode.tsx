
import { Handle, Position, type NodeProps, type Node } from '@xyflow/react';

type StoryNode = Node<{ value: number }, 'value'>;

export const StoryNode = ({ data }: NodeProps<StoryNode>) => {
  return (
    <div className="rounded border bg-white p-3 shadow">
      <div className="font-bold">Story Node</div>
      <div>Value: {data.value}</div>

      <Handle type="target" position={Position.Top} />
      <Handle type="source" position={Position.Bottom} />
    </div>
  );
};


 