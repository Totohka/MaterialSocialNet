/** POST /api/v1/message Body */
export type MessagePostBody = {
  message: string | null;
  dateSend: Date;
  chatRoomId: number;
  userId: number;
  firstName: string | null;
  lastName: string | null;
};

/** GET /api/v1/message/{id} Return */
export type MessageGetReturn = {
  id: number;
  msg: string | null;
  dateSend: Date | string;
  userDTO: {
    id: number;
    firstName: string | null;
    lastName: string | null;
    avatar: string | null;
  };
  typeReactions: string[] | null;
  typeReaction: string | null;
};

/** GET /api/v1/message Query */
export type MessageGetListQuery = {
  chatRoomId: number;
  /** Параметр пагинации, по стандарту равен 0 */
  number: number;
};

/** GET /api/v1/message Return */
export type MessageGetListReturn = {
  countAllMessages: number;
  pageCount: number;
  numberPage: number;
  messages: MessageGetReturn[] | null;
};

/** PUT /api/v1/message Body */
export type MessagePutBody = {
  countAllMessages: number;
  pageCount: string;
};

/** DELETE /api/v1/message/{id} Query */
export type MessageDeleteQuery = {
  chatId: number;
};
