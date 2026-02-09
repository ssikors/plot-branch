
import { Handle, Position, useNodeConnections, type NodeProps, type Node } from '@xyflow/react';
import { useState } from 'react';

type StoryNode = Node<{ description: string }, 'value'>;

export function StoryNode({ data }: NodeProps<StoryNode>) {
  const [isEditing, setIsEditing] = useState(false);
  const [value, setValue] = useState(data.description);

  const connections = useNodeConnections({
    handleType: "source",
  });

  const commit = () => {
    data.description = value;
    setIsEditing(false);
  };

  return (
    <div className="rounded border bg-white p-3 shadow min-w-37.5 text-center">
      <div className="font-bold mb-1">Story Node</div>

      {isEditing ? (
        <textarea
          autoFocus
          className="w-full rounded border px-1 text-sm"
          value={value}
          onChange={(e) => setValue(e.target.value)}
          onBlur={commit}
          onKeyDown={(e) => {
            if (e.key === 'Enter') commit();
            if (e.key === 'Escape') {
              setValue(data.description);
              setIsEditing(false);
            }
          }}
        />
      ) : (
        <div
          className="cursor-pointer hover:text-gray-600 wrap-normal max-w-96"
          onClick={() => setIsEditing(true)}
        >
          {data.description || <span className="italic text-gray-400">Click to edit</span>}
        </div>
      )}

      <Handle type="target" position={Position.Top} />
      <Handle type="source" position={Position.Bottom} />
    </div>
  );
}
