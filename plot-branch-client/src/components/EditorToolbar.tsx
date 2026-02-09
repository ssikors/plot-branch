import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCharacters, createCharacter, type CharacterDto } from "../api/characterApi";

export default function EditorToolbar() {
  const { flowId } = useParams();

  const [expanded, setExpanded] = useState(false);
  const [characters, setCharacters] = useState<CharacterDto[]>([]);
  const [newName, setNewName] = useState("");

  async function loadCharacters() {
    if (!flowId) return;

    const data = await getCharacters(flowId);
    setCharacters(data);
  }

  async function handleAddCharacter() {
    if (!flowId || !newName.trim()) return;

    const newCharacter = await createCharacter(flowId, newName);

    setCharacters(prev => [...prev, newCharacter]);
    setNewName("");
  }

  useEffect(() => {
    loadCharacters();
  }, [flowId]);

  return (
    <div className="absolute top-4 left-1/2 -translate-x-1/2 z-50">
      <div className="bg-white shadow-lg rounded p-2 w-64">

        <button
          onClick={() => setExpanded(!expanded)}
          className="w-full font-bold text-left"
        >
          Characters
        </button>
        

        {expanded && (
          <div className="mt-2 space-y-2">

            <div className="max-h-40 overflow-y-auto border rounded p-2">
              {characters.length === 0 && (
                <div className="text-gray-400 text-sm">
                  No characters yet
                </div>
              )}

              {characters.map(c => (
                <div key={c.id} className="text-sm">
                  {c.name}
                </div>
              ))}
            </div>

            <div className="flex gap-2">
              <input
                value={newName}
                onChange={e => setNewName(e.target.value)}
                placeholder="Character name"
                className="border rounded px-2 py-1 w-full text-sm"
              />

              <button
                onClick={handleAddCharacter}
                className="bg-black text-white px-2 rounded text-sm"
              >
                Add
              </button>
            </div>

          </div>
        )}
      </div>
      
    </div>
  );
}
