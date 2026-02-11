import {
  Handle,
  Position,
  type NodeProps,
  type Node
} from '@xyflow/react';

import { useState } from 'react';
import useStore from '../../store/store';

export type StoryNodeData = {
  description: string;
  characterIds: string[];
};

type StoryNodeType = Node<StoryNodeData, 'storyNode'>;

export function StoryNode({ id, data }: NodeProps<StoryNodeType>) {
  const characters = useStore(s => s.characters);
  const updateNode = useStore(s => s.updateNodeData);

  const [isEditing, setIsEditing] = useState(false);
  const [value, setValue] = useState(data.description);

  const commitDescription = () => {
    updateNode(id, {
      ...data,
      description: value
    });

    setIsEditing(false);
  };


  const linkedCharacters = characters.filter(c =>
    data.characterIds?.includes(c.id)
  );

  const availableCharacters = characters.filter(
    c => !data.characterIds?.includes(c.id)
  );

  function addCharacter(characterId: string) {
    updateNode(id, {
      ...data,
      characterIds: [...(data.characterIds || []), characterId]
    });
  }

  function removeCharacter(characterId: string) {
    updateNode(id, {
      ...data,
      characterIds: data.characterIds.filter(id => id !== characterId)
    });
  }


  return (
    <div className="rounded border bg-white p-3 shadow min-w-44 text-center">

      <div className="font-bold mb-1">Story Node</div>

      {isEditing ? (
        <textarea
          autoFocus
          className="w-full rounded border px-1 text-sm"
          value={value}
          onChange={(e) => setValue(e.target.value)}
          onBlur={commitDescription}
          onKeyDown={(e) => {
            if (e.key === 'Enter') commitDescription();
            if (e.key === 'Escape') {
              setValue(data.description);
              setIsEditing(false);
            }
          }}
        />
      ) : (
        <div
          className="cursor-pointer hover:text-gray-600"
          onClick={() => setIsEditing(true)}
        >
          {data.description || (
            <span className="italic text-gray-400">
              Click to edit
            </span>
          )}
        </div>
      )}


      <div className="mt-2 text-left">
        <div className="text-xs font-semibold mb-1">Characters</div>

        {linkedCharacters.map(c => (
          <div
            key={c.id}
            className="flex justify-between text-xs bg-gray-100 rounded px-1 py-0.5 mb-1"
          >
            {c.name}
            <button
              onClick={() => removeCharacter(c.id)}
              className="text-red-500"
            >
              ✕
            </button>
          </div>
        ))}
      </div>


      {availableCharacters.length > 0 && (
        <select
          className="mt-2 text-xs border rounded w-full"
          defaultValue=""
          onChange={(e) => {
            if (e.target.value) {
              addCharacter(e.target.value);
              e.target.value = "";
            }
          }}
        >
          <option value="">Add character...</option>

          {availableCharacters.map(c => (
            <option key={c.id} value={c.id}>
              {c.name}
            </option>
          ))}
        </select>
      )}

      <Handle type="target" position={Position.Top} />
      <Handle type="source" position={Position.Bottom} />
    </div>
  );
}
