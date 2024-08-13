import {
  DeleteMessageReturn,
  ReceiveReturn,
  UpdateMessageReturn,
} from './chat-dto';

export type ChatConfig = {
  onReceiveCallback: (data: ReceiveReturn) => void;
  onUpdateMessageCallback: (data: UpdateMessageReturn) => void;
  onDeleteMessageCallback: (data: DeleteMessageReturn) => void;
};
