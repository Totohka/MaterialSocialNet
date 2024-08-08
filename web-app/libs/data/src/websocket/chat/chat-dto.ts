export type ReceiveReturn = {
  chatId: number;
  message: string;
  firstName: string;
  lastName: string;
  userId: number;
  dateSend: Date | string;
  messageId: number;
};

export type UpdateMessageReturn = {
  chatId: number;
  message: string;
  firstName: string;
  lastName: string;
  userId: number;
  dateSend: Date | string;
  messageId: number;
};

export type DeleteMessageReturn = {
  id: number;
  chatId: number;
};
