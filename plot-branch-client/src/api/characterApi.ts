import axios from "axios";

export interface CharacterDto {
  id: string;
  name: string;
}

export const getCharacters = async (flowId: string) => {
  const res = await axios.get(`/api/Character?graphId=${flowId}`);
  return res.data as CharacterDto[];
};

export const createCharacter = async (
  flowId: string,
  name: string
) => {
  const res = await axios.post(`/api/Character?graphId=${flowId}`, {
    name
  });

  return res.data as CharacterDto;
};

export const addCharacterToNode = async (
  nodeId: string,
  characterId: string
) => {
  const res = await axios.post(`/api/Character/Node?nodeId=${nodeId}`, {
    characterId
  });

  return res.status;
};