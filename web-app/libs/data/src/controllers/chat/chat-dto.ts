/** POST /api/chat Body */
export type ChatPostBody = {
  name: string;
  description: string;
  userId: number;
  userIdsByChat: number[];
};

/** GET /api/chat/{id} Return */
export type ChatGetReturn = {
  id: number;
  name: string | null;
  description: string | null;
  lastMessage: string | null;
};

/** GET /api/chat Query */
export type ChatGetListQuery = {
  search: string;
  number: number;
};

/** GET /api/chat Return */
export type ChatGetListReturn = {
  countAllChats: number;
  pageCount: number;
  numberPage: number;
  chatRoomDTOs: ChatGetReturn[];
};

/** PUT /api/chat Body */
export type ChatPutBody = {
  id: number;
  name: string;
  description: string;
};
