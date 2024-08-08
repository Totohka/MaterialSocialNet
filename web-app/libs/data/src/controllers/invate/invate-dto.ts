/** POST /api/v1/invate Body */
export type InvatePostBody = {
  chatRoomId: number;
  userId: number;
};

/** GET /api/v1/invate Query */
export type InvateGetListQuery = {
  chatId: number;
};

/** GET /api/v1/invate Return */
export type InvateGetListReturn = {
  id: number;
  firstName: string | null;
  lastName: string | null;
  avatar: string | null;
};

/** DELETE /api/v1/invate Query */
export type InvateDeleteQuery = {
  chatRoomId: number;
  userId: number;
};
