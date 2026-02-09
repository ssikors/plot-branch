import { useState } from "react";
import useStore from "../store/store";

export default function EditorToolbar() {
  const characters = useStore(s => s.characters);
  const addCharacter = useStore(s => s.addCharacter);

  const [expanded, setExpanded] = useState(false);
  const [name, setName] = useState("");

  async function handleAddCharacter() {
    if (!name.trim()) return;

    await addCharacter(name);
    setName("");
  }

  return (
    <div className="absolute top-4 left-1/2 -translate-x-1/2 bg-white shadow rounded p-3 z-50">

      <button
        onClick={() => setExpanded(!expanded)}
        className="font-bold"
      >
        Characters
      </button>

      {expanded && (
        <div className="mt-3 w-60">

          <ul className="max-h-40 overflow-y-auto mb-2">
            {characters.map(c => (
              <li key={c.id} className="text-sm">
                {c.name}
              </li>
            ))}
          </ul>

          <div className="flex gap-2">
            <input
              className="border px-2 py-1 flex-1"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Character name"
            />

            <button
              onClick={handleAddCharacter}
              className="bg-black text-white px-2"
            >
              Add
            </button>
          </div>

        </div>
      )}

    </div>
  );
}
